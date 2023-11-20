using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BigCommerceNET.Misc;
using BigCommerceNET.Models;
using BigCommerceNET.Models.Command;
using BigCommerceNET.Models.Configuration;
using BigCommerceNET.Models.Throttling;
using Newtonsoft.Json;

namespace BigCommerceNET.Services
{
    internal class WebRequestServices
    {
        private const int RequestTimeoutMs = 30 * 60 * 1000;

        private readonly BigCommerceConfig _config;
        private readonly string _host;
        private readonly APIVersion _apiVersion;

        public WebRequestServices(BigCommerceConfig config, string marker)
        {
            _config = config;
            _apiVersion = config.GetAPIVersion();
            _host = _apiVersion == APIVersion.V2 ? ResolveHost(config, marker) : config.NativeHost!;
        }

        public async Task<BigCommerceResponse<T>> GetResponseByRelativeUrlAsync<T>(
            string url, string commandParams, string marker, [CallerMemberName] string? callerMethodName = null)
            where T : class
        {
            var requestUrl = GetUrl(url, commandParams);
            return await GetResponseAsync<T>(requestUrl, marker, callerMethodName!);
        }

        public async Task<BigCommerceResponse<T>> GetResponseByRelativeUrlAsync<T>(
            BigCommerceCommand command, string commandParams, string marker, [CallerMemberName] string? callerMethodName = null)
            where T : class
        {
            var requestUrl = GetUrl(command, commandParams);
            return await GetResponseAsync<T>(requestUrl, marker, callerMethodName!);
        }

        public IBigCommerceRateLimits PutData(BigCommerceCommand command, string endpoint, string jsonContent, string marker, [CallerMemberName] string? callerMethodName = null)
        {
            var url = GetUrl(command, endpoint);
            LogCallStarted(url, marker, callerMethodName!, HttpMethodEnum.Put, jsonContent);
            var responseStatusCode = string.Empty;

            try
            {
                var httpClient = CreateHttpClient();
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = httpClient.PutAsync(url, content).Result; // Blocking call to perform the PUT request

                responseStatusCode = response.StatusCode.ToString();
                var currentLimits = ParseLimits(response);
                LogCallEnded(url, marker, callerMethodName!, responseStatusCode, null, currentLimits.CallsRemaining.ToString(), null);
                return currentLimits;
            }
            catch (Exception ex)
            {
                throw HandleExceptionAndLog(url, marker, callerMethodName!, responseStatusCode, ex);
            }
        }


        public async Task<IBigCommerceRateLimits> PutDataAsync(
            BigCommerceCommand command, string endpoint, string jsonContent, string marker, [CallerMemberName] string? callerMethodName = null)
        {
            var url = GetUrl(command, endpoint);
            return await PutDataAsync(url, jsonContent, marker, callerMethodName);
        }

        public async Task<IBigCommerceRateLimits> PutDataAsync(
            string url, string jsonContent, string marker, [CallerMemberName] string? callerMethodName = null)
        {
            LogCallStarted(url, marker, callerMethodName!, HttpMethodEnum.Put, jsonContent);
            var responseStatusCode = string.Empty;

            try
            {
                var httpClient = CreateHttpClient();
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(url, content);
                responseStatusCode = response.StatusCode.ToString();

                var currentLimits = ParseLimits(response);
                LogCallEnded(url, marker, callerMethodName!, responseStatusCode, null, currentLimits.CallsRemaining.ToString(), null);
                return currentLimits;
            }
            catch (Exception ex)
            {
                throw HandleExceptionAndLog(url, marker, callerMethodName!, responseStatusCode, ex);
            }
        }

        public async Task<BigCommerceResponse<T>> GetResponseAsync<T>(string url, string marker, [CallerMemberName] string? callerMethodName = null) where T : class
        {
            this.LogCallStarted(url, marker, callerMethodName!);
            var responseStatusCode = HttpStatusCode.OK;

            try
            {
                BigCommerceResponse<T> result;

                using (var client = CreateHttpClient())
                {                   
                    var timeoutToken = this.GetTimeoutToken(RequestTimeoutMs);
                    using (timeoutToken.Register(() => client.CancelPendingRequests()))
                    using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, timeoutToken))
                    {
                        responseStatusCode = response.StatusCode;

                        if (!response.IsSuccessStatusCode)
                        {
                            // Handle non-success status codes if needed
                        }

                        timeoutToken.ThrowIfCancellationRequested();
                        result = await this.ParseResponseAsync<T>(response, marker, url, callerMethodName!);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw this.HandleExceptionAndLog(url, marker, callerMethodName!, responseStatusCode.ToString(), ex);
            }
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();

            if (_apiVersion == APIVersion.V2)
            {
                var base64Auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_config.UserName}:{_config.ApiKey}"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Auth);
            }
            else
            {
                httpClient.DefaultRequestHeaders.Add("X-Auth-Client", _config.ClientId);
                httpClient.DefaultRequestHeaders.Add("X-Auth-Token", _config.Token);
            }

            httpClient.Timeout = TimeSpan.FromMilliseconds(RequestTimeoutMs);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "BigCommerceNET");

            return httpClient;
        }

        private string GetUrl(string url, string commandParams)
        {
            return string.Concat(url, commandParams);
        }

        private string GetUrl(BigCommerceCommand command, string commandParams)
        {
            return string.Concat(_host, command.Command, commandParams);
        }

        private IBigCommerceRateLimits ParseLimits(HttpResponseMessage response)
        {
            // Get X-BC-ApiLimit-Remaining header
            if (response.Headers.TryGetValues("X-BC-ApiLimit-Remaining", out var remainingLimitHeader))
            {
                var remainingLimit = remainingLimitHeader?.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(remainingLimit) && int.TryParse(remainingLimit, out var callsRemaining))
                {
                    // Get X-Rate-Limit-Requests-Left header
                    if (response.Headers.TryGetValues("X-Rate-Limit-Requests-Left", out var limitRequestsLeftHeader))
                    {
                        var limitRequestsLeft = limitRequestsLeftHeader?.FirstOrDefault();
                        if (!string.IsNullOrWhiteSpace(limitRequestsLeft) && int.TryParse(limitRequestsLeft, out var limitRequestsLeftValue))
                        {
                            // Get X-Rate-Limit-Time-Reset-Ms header
                            if (response.Headers.TryGetValues("X-Rate-Limit-Time-Reset-Ms", out var limitTimeResetMsHeader))
                            {
                                var limitTimeResetMs = limitTimeResetMsHeader?.FirstOrDefault();
                                if (!string.IsNullOrWhiteSpace(limitTimeResetMs) && int.TryParse(limitTimeResetMs, out var limitTimeResetMsValue))
                                {
                                    return new BigCommerceLimits(callsRemaining, limitRequestsLeftValue, limitTimeResetMsValue);
                                }
                            }
                        }
                    }
                }
            }

            // Headers not found or parsing failed
            return new BigCommerceLimits(-1, -1, -1);
        }


        private string? GetRemainingLimit(HttpResponseMessage response)
        {
            if (response.Headers.TryGetValues("X-BC-ApiLimit-Remaining", out var remainingLimitHeader))
            {
                var remainingLimit = remainingLimitHeader?.FirstOrDefault();
                if (!string.IsNullOrEmpty(remainingLimit))
                {
                    return remainingLimit;
                }
            }

            if (response.Headers.TryGetValues("X-Rate-Limit-Requests-Left", out var limitRequestsLeftHeader))
            {
                return limitRequestsLeftHeader?.FirstOrDefault();
            }

            return null; // Header not found
        }

        private string CreateAuthenticationHeader()
        {
            var authInfo = string.Concat(_config.UserName, ":", _config.ApiKey);
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

            return string.Concat("Basic ", authInfo);
        }

        private string ResolveHost(BigCommerceConfig config, string marker, [CallerMemberName] string? callerMethodName = null)
        {
            try
            {
                var url = string.Concat(config.NativeHost, BigCommerceCommand.GetOrdersCountV2.Command);
                _ = GetResponseAsync<BigCommerceItemsCount>(url, marker, callerMethodName);
                return config.NativeHost!;
            }
            catch (Exception)
            {
                try
                {
                    var url = string.Concat(config.CustomHost, BigCommerceCommand.GetOrdersCountV2.Command);
                    _ = GetResponseAsync<BigCommerceItemsCount>(url, marker, callerMethodName);
                    return config.CustomHost!;
                }
                catch (Exception)
                {
                    var clippedHost = config.CustomHost!.Replace("www.", string.Empty);
                    var url = string.Concat(clippedHost, BigCommerceCommand.GetOrdersCountV2.Command);
                    _ = GetResponseAsync<BigCommerceItemsCount>(url, marker, callerMethodName);
                    return clippedHost;
                }
            }
        }

        private CancellationToken GetTimeoutToken(int timeout)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(timeout);
            return cancellationTokenSource.Token;
        }

        private void LogCallStarted(string url, string marker, string callerMethodName, HttpMethodEnum httpMethod = HttpMethodEnum.Get, string? body = null)
        {
            BigCommerceLogger.TraceLog(new RequestInfo()
            {
                Mark = marker,
                Url = url,
                LibMethodName = callerMethodName,
                Category = MessageCategoryEnum.Information,
                HttpMethod = httpMethod,
                Body = body,
                TenantId = _config.TenantId,
                ChannelAccountId = _config.ChannelAccountId
            });
        }

        private void LogCallEnded(string url, string marker, string callerMethodName, string statusCode, string? response, string remainingCalls, string? systemVersion)
        {
            BigCommerceLogger.TraceLog(new ResponseInfo()
            {
                Mark = marker,
                Url = url,
                LibMethodName = callerMethodName,
                Category = MessageCategoryEnum.Information,
                Response = response,
                StatusCode = statusCode,
                RemainingCalls = remainingCalls,
                SystemVersion = systemVersion,
                TenantId = _config.TenantId,
                ChannelAccountId = _config.ChannelAccountId
            });
        }

        private Exception HandleExceptionAndLog(string url, string marker, string callerMethodName, string statusCode, Exception ex)
        {
            BigCommerceLogger.LogTraceException(new ResponseInfo()
            {
                Mark = marker,
                Url = url,
                LibMethodName = callerMethodName,
                StatusCode = statusCode,
                Category = MessageCategoryEnum.Critical,
                TenantId = _config.TenantId,
                ChannelAccountId = _config.ChannelAccountId
            }, ex);

            return new Exception(string.Format("Marker: '{0}'. Call to url '{1}' failed", marker, url), ex);
        }

        public BigCommerceResponse<T> GetResponseByRelativeUrl<T>(string url, string commandParams, string marker, [CallerMemberName] string? callerMethodName = null) where T : class
        {
            var requestUrl = this.GetUrl(url, commandParams);
            var result = this.GetResponse<T>(requestUrl, marker, callerMethodName!);
            return result;
        }

        public BigCommerceResponse<T> GetResponseByRelativeUrl<T>(BigCommerceCommand command, string commandParams, string marker, [CallerMemberName] string? callerMethodName = null) where T : class
        {
            var requestUrl = this.GetUrl(command, commandParams);
            var result = this.GetResponse<T>(requestUrl, marker, callerMethodName!);
            return result;
        }

        public BigCommerceResponse<T> GetResponse<T>(string url, string marker, [CallerMemberName] string? callerMethodName = null) where T : class
        {
            LogCallStarted(url, marker, callerMethodName!);
            var responseStatusCode = HttpStatusCode.OK;

            try
            {

                using (var httpClient = CreateHttpClient())
                {
                    using (var response = httpClient.GetAsync(url).Result) // Blocking call to get the response synchronously
                    {
                        responseStatusCode = response.StatusCode;
                        
                        return ParseResponse<T>(response, marker, url, callerMethodName!, responseStatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                throw HandleExceptionAndLog(url, marker, callerMethodName!, responseStatusCode.ToString(), ex);
            }
        }

        private BigCommerceResponse<T> ParseResponse<T>(HttpResponseMessage response, string marker, string url, string callerMethodName, HttpStatusCode statusCode) where T : class
        {
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            var remainingLimit = GetRemainingLimit(response);

            // Check if the header is present before accessing its value
            var versionHeader = response.Headers.TryGetValues("X-BC-Store-Version", out var versionHeaderValues)
                ? versionHeaderValues.FirstOrDefault()
                : null;

            var version = versionHeader ?? "Header Not Found";

            LogCallEnded(url, marker, callerMethodName, statusCode.ToString(), jsonResponse, remainingLimit!, version);

            var limits = ParseLimits(response);

            if (string.IsNullOrEmpty(jsonResponse))
            {
                return new BigCommerceResponse<T>(null!, limits);
            }

            var result = JsonConvert.DeserializeObject<T>(jsonResponse);
            return new BigCommerceResponse<T>(result!, limits);
        }

        private async Task<BigCommerceResponse<T>> ParseResponseAsync<T>(HttpResponseMessage response, string marker, string url, string callerMethodName) where T : class
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var remainingLimit = this.GetRemainingLimit(response);
            var versionHeader = response.Headers.TryGetValues("X-BC-Store-Version", out var versionHeaderValues)
                ? versionHeaderValues.FirstOrDefault()
                : null;

            var version = versionHeader ?? "Header Not Found";

            this.LogCallEnded(url, marker, callerMethodName, response.StatusCode.ToString(), jsonResponse, remainingLimit!, version);

            var limits = this.ParseLimits(response);

            if (string.IsNullOrEmpty(jsonResponse))
            {
                return new BigCommerceResponse<T>(null!, limits);
            }

            var result = JsonConvert.DeserializeObject<T>(jsonResponse);
            return new BigCommerceResponse<T>(result!, limits);
        }



    }
}

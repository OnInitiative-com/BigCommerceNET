using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BigCommerceNET.Misc;
using BigCommerceNET.Models.Address;
using BigCommerceNET.Models.Command;
using BigCommerceNET.Models.Configuration;
using BigCommerceNET.Models.Order;
using BigCommerceNET.Services;
using Netco.Extensions;

namespace BigCommerceNET
{
    /// <summary>
    /// The big commerce orders service.
    /// </summary>
    public class BigCommerceOrdersService : BigCommerceServiceBase, IBigCommerceOrdersService
    {
        /// <summary>
        /// The web request services.
        /// </summary>
        private readonly WebRequestServices _webRequestServices;
        /// <summary>
        /// The api version.
        /// </summary>
        private readonly APIVersion _apiVersion;

        /// <summary>
        /// Initializes a new instance of the <see cref="BigCommerceOrdersService"/> class.
        /// </summary>
        /// <param name="config">The config.</param>
        public BigCommerceOrdersService(BigCommerceConfig config)
        {           
            this._webRequestServices = new WebRequestServices(config, this.GetMarker());
            this._apiVersion = config.GetAPIVersion();
        }

        #region Orders
        /// <summary>
        /// Gets the orders.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <returns><![CDATA[A List<BigCommerceOrder>.]]></returns>
        public List<BigCommerceOrder> GetOrders(DateTime dateFrom, DateTime dateTo)
        {
            return this._apiVersion == APIVersion.V2 ? this.GetOrdersForV2(dateFrom, dateTo) : this.GetOrdersForV3(dateFrom, dateTo);
        }

        /// <summary>
        /// Gets the orders asynchronously.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="token">The token.</param>
        /// <returns><![CDATA[A Task<List<BigCommerceOrder>>.]]></returns>
        public Task<List<BigCommerceOrder>> GetOrdersAsync(DateTime dateFrom, DateTime dateTo, CancellationToken token)
        {
            return this._apiVersion == APIVersion.V2 ? this.GetOrdersForV2Async(dateFrom, dateTo, token) : this.GetOrdersForV3Async(dateFrom, dateTo, token);
        }

        /// <summary>
        /// Gets the orders for v2.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <returns><![CDATA[A List<BigCommerceOrder>.]]></returns>
        public List<BigCommerceOrder> GetOrdersForV2(DateTime dateFrom, DateTime dateTo)
        {
            var mainEndpoint = ParamsBuilder.CreateOrdersParams(dateFrom, dateTo);
            var orders = new List<BigCommerceOrder>();
            var marker = this.GetMarker();

            for (var i = 1; i < int.MaxValue; i++)
            {
                var compositeEndpoint = mainEndpoint.ConcatParams(ParamsBuilder.CreateGetNextPageParams(new BigCommerceCommandConfig(i, RequestMaxLimit)));
                var ordersWithinPage = ActionPolicies.Get(marker, compositeEndpoint).Get(() =>
                    this._webRequestServices.GetResponseByRelativeUrl<List<BigCommerceOrder>>(BigCommerceCommand.GetOrdersV2, compositeEndpoint, marker));
                this.CreateApiDelay(ordersWithinPage.Limits).Wait(); //API requirement

                if (ordersWithinPage.Response == null)
                    break;

                this.GetOrdersProducts(ordersWithinPage.Response, marker);
                this.GetOrdersCoupons(ordersWithinPage.Response, marker);
                this.GetOrdersShippingAddresses(ordersWithinPage.Response, marker);
                orders.AddRange(ordersWithinPage.Response);
                if (ordersWithinPage.Response.Count < RequestMaxLimit)
                    break;
            }

            return orders;
        }

        /// <summary>
        /// Gets the orders for v3.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <returns><![CDATA[A List<BigCommerceOrder>.]]></returns>
        public List<BigCommerceOrder> GetOrdersForV3(DateTime dateFrom, DateTime dateTo)
        {
            var mainEndpoint = ParamsBuilder.CreateOrdersParams(dateFrom, dateTo);
            var orders = new List<BigCommerceOrder>();
            var marker = this.GetMarker();

            for (var i = 1; i < int.MaxValue; i++)
            {
                var compositeEndpoint = mainEndpoint.ConcatParams(ParamsBuilder.CreateGetNextPageParams(new BigCommerceCommandConfig(i, RequestMaxLimit)));
                var ordersWithinPage = ActionPolicies.Get(marker, compositeEndpoint).Get(() =>
                    this._webRequestServices.GetResponseByRelativeUrl<List<BigCommerceOrder>>(BigCommerceCommand.GetOrdersV2_OAuth, compositeEndpoint, marker));
                this.CreateApiDelay(ordersWithinPage.Limits).Wait(); //API requirement

                if (ordersWithinPage.Response == null)
                    break;

                this.GetOrdersProducts(ordersWithinPage.Response, marker);
                this.GetOrdersCoupons(ordersWithinPage.Response, marker);
                this.GetOrdersShippingAddresses(ordersWithinPage.Response, marker);
                orders.AddRange(ordersWithinPage.Response);
                if (ordersWithinPage.Response.Count < RequestMaxLimit)
                    break;
            }

            return orders;
        }

        /// <summary>
        /// Gets the orders for v2 asynchronously.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="token">The token.</param>
        /// <returns><![CDATA[A Task<List<BigCommerceOrder>>.]]></returns>
        private async Task<List<BigCommerceOrder>> GetOrdersForV2Async(DateTime dateFrom, DateTime dateTo, CancellationToken token)
        {
            var mainEndpoint = ParamsBuilder.CreateOrdersParams(dateFrom, dateTo);
            var orders = new List<BigCommerceOrder>();
            var marker = this.GetMarker();

            for (var i = 1; i < int.MaxValue; i++)
            {
                var compositeEndpoint = mainEndpoint.ConcatParams(ParamsBuilder.CreateGetNextPageParams(new BigCommerceCommandConfig(i, RequestMaxLimit)));
                var ordersWithinPage = await ActionPolicies.GetAsync(marker, compositeEndpoint).Get(async () =>
                    await this._webRequestServices.GetResponseByRelativeUrlAsync<List<BigCommerceOrder>>(BigCommerceCommand.GetOrdersV2, compositeEndpoint, marker));
                await this.CreateApiDelay(ordersWithinPage.Limits, token); //API requirement

                if (ordersWithinPage.Response == null)
                    break;

                await this.GetOrdersProductsAsync(ordersWithinPage.Response, ordersWithinPage.Limits.IsUnlimitedCallsCount, token, marker);
                await this.GetOrdersCouponsAsync(ordersWithinPage.Response, ordersWithinPage.Limits.IsUnlimitedCallsCount, token, marker);
                await this.GetOrdersShippingAddressesAsync(ordersWithinPage.Response, ordersWithinPage.Limits.IsUnlimitedCallsCount, token, marker);
                orders.AddRange(ordersWithinPage.Response);
                if (ordersWithinPage.Response.Count < RequestMaxLimit)
                    break;
            }

            return orders;
        }

        /// <summary>
        /// Gets the orders for v3 asynchronously.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="token">The token.</param>
        /// <returns><![CDATA[A Task<List<BigCommerceOrder>>.]]></returns>
        private async Task<List<BigCommerceOrder>> GetOrdersForV3Async(DateTime dateFrom, DateTime dateTo, CancellationToken token)
        {
            var mainEndpoint = ParamsBuilder.CreateOrdersParams(dateFrom, dateTo);
            var orders = new List<BigCommerceOrder>();
            var marker = this.GetMarker();

            for (var i = 1; i < int.MaxValue; i++)
            {
                var compositeEndpoint = mainEndpoint.ConcatParams(ParamsBuilder.CreateGetNextPageParams(new BigCommerceCommandConfig(i, RequestMaxLimit)));
                var ordersWithinPage = await ActionPolicies.GetAsync(marker, compositeEndpoint).Get(async () =>
                    await this._webRequestServices.GetResponseByRelativeUrlAsync<List<BigCommerceOrder>>(BigCommerceCommand.GetOrdersV2_OAuth, compositeEndpoint, marker));
                await this.CreateApiDelay(ordersWithinPage.Limits, token); //API requirement

                if (ordersWithinPage.Response == null)
                    break;

                await this.GetOrdersProductsAsync(ordersWithinPage.Response, ordersWithinPage.Limits.IsUnlimitedCallsCount, token, marker);
                await this.GetOrdersCouponsAsync(ordersWithinPage.Response, ordersWithinPage.Limits.IsUnlimitedCallsCount, token, marker);
                await this.GetOrdersShippingAddressesAsync(ordersWithinPage.Response, ordersWithinPage.Limits.IsUnlimitedCallsCount, token, marker);
                orders.AddRange(ordersWithinPage.Response);
                if (ordersWithinPage.Response.Count < RequestMaxLimit)
                    break;
            }

            return orders;
        }

        #endregion

        #region Order products
        /// <summary>
        /// Gets the orders products.
        /// </summary>
        /// <param name="orders">The orders.</param>
        /// <param name="marker">The marker.</param>
        private void GetOrdersProducts(IEnumerable<BigCommerceOrder> orders, string marker)
        {
            foreach (var order in orders)
            {
                for (var i = 1; i < int.MaxValue; i++)
                {
                    if (string.IsNullOrWhiteSpace(order.ProductsReference?.Url))
                        break;

                    var endpoint = ParamsBuilder.CreateGetNextPageParams(new BigCommerceCommandConfig(i, RequestMaxLimit));
                    var products = ActionPolicies.Get(marker, endpoint).Get(() =>
                        this._webRequestServices.GetResponseByRelativeUrl<List<BigCommerceOrderProduct>>(order.ProductsReference.Url, endpoint, marker));
                    this.CreateApiDelay(products.Limits).Wait(); //API requirement

                    if (products.Response == null)
                        break;
                    order.Products.AddRange(products.Response);
                    if (products.Response.Count < RequestMaxLimit)
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the orders products asynchronously.
        /// </summary>
        /// <param name="orders">The orders.</param>
        /// <param name="isUnlimit">If true, is unlimit.</param>
        /// <param name="token">The token.</param>
        /// <param name="marker">The marker.</param>
        /// <returns>A Task.</returns>
        private async Task GetOrdersProductsAsync(IEnumerable<BigCommerceOrder> orders, bool isUnlimit, CancellationToken token, string marker)
        {
            var threadCount = isUnlimit ? MaxThreadsCount : 1;
            await orders.DoInBatchAsync(threadCount, async order =>
            {
                for (var i = 1; i < int.MaxValue; i++)
                {
                    if (string.IsNullOrWhiteSpace(order.ProductsReference?.Url))
                        break;

                    var endpoint = ParamsBuilder.CreateGetNextPageParams(new BigCommerceCommandConfig(i, RequestMaxLimit));
                    var products = await ActionPolicies.GetAsync(marker, endpoint).Get(async () =>
                        await this._webRequestServices.GetResponseByRelativeUrlAsync<List<BigCommerceOrderProduct>>(order.ProductsReference.Url, endpoint, marker));
                    await this.CreateApiDelay(products.Limits, token); //API requirement

                    if (products.Response == null)
                        break;
                    order.Products.AddRange(products.Response);
                    if (products.Response.Count < RequestMaxLimit)
                        break;
                }
            });
        }
        #endregion

        #region Order Coupons
        /// <summary>
        /// Gets the orders coupons.
        /// </summary>
        /// <param name="orders">The orders.</param>
        /// <param name="marker">The marker.</param>
        private void GetOrdersCoupons(IEnumerable<BigCommerceOrder> orders, string marker)
        {
            foreach (var order in orders)
            {
                if (string.IsNullOrWhiteSpace(order.CouponsReference?.Url))
                    continue;

                for (var i = 1; i < int.MaxValue; i++)
                {
                    var endpoint = ParamsBuilder.CreateGetNextPageParams(new BigCommerceCommandConfig(i, RequestMaxLimit));
                    var coupons = ActionPolicies.Get(marker, endpoint).Get(() =>
                        this._webRequestServices.GetResponseByRelativeUrl<List<BigCommerceOrderCoupon>>(order.CouponsReference.Url, endpoint, marker));
                    this.CreateApiDelay(coupons.Limits).Wait(); //API requirement

                    if (coupons.Response == null)
                        break;
                    order.Coupons!.AddRange(coupons.Response);
                    if (coupons.Response.Count < RequestMaxLimit)
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the orders coupons asynchronously.
        /// </summary>
        /// <param name="orders">The orders.</param>
        /// <param name="isUnlimit">If true, is unlimit.</param>
        /// <param name="token">The token.</param>
        /// <param name="marker">The marker.</param>
        /// <returns>A Task.</returns>
        private async Task GetOrdersCouponsAsync(IEnumerable<BigCommerceOrder> orders, bool isUnlimit, CancellationToken token, string marker)
        {
            var threadCount = isUnlimit ? MaxThreadsCount : 1;
            await orders.DoInBatchAsync(threadCount, async order =>
            {
                if (string.IsNullOrWhiteSpace(order.CouponsReference?.Url))
                    return;

                for (var i = 1; i < int.MaxValue; i++)
                {
                    var endpoint = ParamsBuilder.CreateGetNextPageParams(new BigCommerceCommandConfig(i, RequestMaxLimit));
                    var coupons = await ActionPolicies.GetAsync(marker, endpoint).Get(async () =>
                        await this._webRequestServices.GetResponseByRelativeUrlAsync<List<BigCommerceOrderCoupon>>(order.CouponsReference.Url, endpoint, marker));
                    await this.CreateApiDelay(coupons.Limits, token); //API requirement

                    if (coupons.Response == null)
                        break;
                    order.Coupons!.AddRange(coupons.Response);
                    if (coupons.Response.Count < RequestMaxLimit)
                        break;
                }
            });
        }
        #endregion

        #region ShippingAddress
        /// <summary>
        /// Gets the orders shipping addresses.
        /// </summary>
        /// <param name="orders">The orders.</param>
        /// <param name="marker">The marker.</param>
        private void GetOrdersShippingAddresses(IEnumerable<BigCommerceOrder> orders, string marker)
        {
            foreach (var order in orders)
            {
                if (string.IsNullOrWhiteSpace(order.ShippingAddressesReference?.Url))
                    continue;

                var addresses = ActionPolicies.Get(marker, order.ShippingAddressesReference.Url).Get(() =>
                    this._webRequestServices.GetResponse<List<BigCommerceShippingAddress>>(order.ShippingAddressesReference.Url, marker));
                order.ShippingAddresses = addresses.Response;
                this.CreateApiDelay(addresses.Limits).Wait(); //API requirement
            }
        }

        /// <summary>
        /// Gets the orders shipping addresses asynchronously.
        /// </summary>
        /// <param name="orders">The orders.</param>
        /// <param name="isUnlimit">If true, is unlimit.</param>
        /// <param name="token">The token.</param>
        /// <param name="marker">The marker.</param>
        /// <returns>A Task.</returns>
        private async Task GetOrdersShippingAddressesAsync(IEnumerable<BigCommerceOrder> orders, bool isUnlimit, CancellationToken token, string marker)
        {
            var threadCount = isUnlimit ? MaxThreadsCount : 1;
            await orders.DoInBatchAsync(threadCount, async order =>
            {
                if (string.IsNullOrWhiteSpace(order.ShippingAddressesReference?.Url))
                    return;

                var addresses = await ActionPolicies.GetAsync(marker, order.ShippingAddressesReference.Url).Get(async () =>
                    await this._webRequestServices.GetResponseAsync<List<BigCommerceShippingAddress>>(order.ShippingAddressesReference.Url, marker));
                order.ShippingAddresses = addresses.Response;
                await this.CreateApiDelay(addresses.Limits, token); //API requirement
            });
        }
        #endregion
    }
}
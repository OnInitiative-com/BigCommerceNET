namespace BigCommerceNET.Misc
{
    /// <summary>
    /// The http method enum.
    /// </summary>
    public enum HttpMethodEnum { Get, Post, Put }
    /// <summary>
    /// The message category enum.
    /// </summary>
    public enum MessageCategoryEnum { Information, Warning, Critical }

    /// <summary>
    /// The call info.
    /// </summary>
    public abstract class CallInfo
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="CallInfo"/> class.
        /// </summary>
        protected CallInfo()
		{
			this.Mark = "Unknown";
		}

        /// <summary>
        /// Gets or Sets the mark.
        /// </summary>
        public string? Mark { get; set; }
        /// <summary>
        /// Gets or Sets the lib method name.
        /// </summary>
        public string? LibMethodName { get; set; }
        /// <summary>
        /// Gets or Sets the url.
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// Gets or Sets the category.
        /// </summary>
        public MessageCategoryEnum Category { get; set; }

        /// <summary>
        /// Gets or Sets the tenant id.
        /// </summary>
        public long? TenantId { get; set; }
        /// <summary>
        /// Gets or Sets the channel account id.
        /// </summary>
        public long? ChannelAccountId { get; set; }
	}

    /// <summary>
    /// The request info.
    /// </summary>
    public sealed class RequestInfo : CallInfo
	{
        /// <summary>
        /// Gets or Sets the http method.
        /// </summary>
        public HttpMethodEnum HttpMethod { get; set; }
        /// <summary>
        /// Gets or Sets the body.
        /// </summary>
        public object? Body { get; set; }
	}

    /// <summary>
    /// The response info.
    /// </summary>
    public sealed class ResponseInfo : CallInfo
	{
        /// <summary>
        /// Gets or Sets the response.
        /// </summary>
        public object? Response { get; set; }
        /// <summary>
        /// Gets or Sets the remaining calls.
        /// </summary>
        public string? RemainingCalls { get; set; }
        /// <summary>
        /// Gets or Sets the system version.
        /// </summary>
        public string? SystemVersion { get; set; }
        /// <summary>
        /// Gets or Sets the status code.
        /// </summary>
        public string? StatusCode { get; set; }
	}

    /// <summary>
    /// The retry info.
    /// </summary>
    public sealed class RetryInfo : CallInfo
	{
        /// <summary>
        /// Gets or Sets the total retries attempts.
        /// </summary>
        public int TotalRetriesAttempts { get; set; }
        /// <summary>
        /// Gets or Sets the current retry attempt.
        /// </summary>
        public int CurrentRetryAttempt { get; set; }
        /// <summary>
        /// Gets or Sets the delay in seconds.
        /// </summary>
        public double DelayInSeconds { get; set; }
	}
}
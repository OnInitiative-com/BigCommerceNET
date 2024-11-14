namespace BigCommerceNET.Models.Throttling
{
    /// <summary>
    /// The big commerce rate limits interface.
    /// </summary>
    public interface IBigCommerceRateLimits
	{
        /// <summary>
        /// Gets the calls remaining.
        /// </summary>
        int CallsRemaining{ get; }

        /// <summary>
        /// Gets the limit requests left.
        /// </summary>
        int LimitRequestsLeft{ get; }
        /// <summary>
        /// Gets the limit time reset ms.
        /// </summary>
        int LimitTimeResetMs{ get; }

        /// <summary>
        /// Gets a value indicating whether unlimited calls is count.
        /// </summary>
        bool IsUnlimitedCallsCount{ get; }
	}
}

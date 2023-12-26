namespace BigCommerceNET.Models.Throttling
{
    /// <summary>
    /// The big commerce limits.
    /// </summary>
    internal class BigCommerceLimits: IBigCommerceRateLimits
	{
        // 2018-10-11: https://support.bigcommerce.com/articles/Public/Platform-Limits
        // Trial Stores, Standard and Plus plans : 20000 per hour
        // Pro plans : 60000 per hour
        // Enterprise : Unlimited
        /// <summary>
        /// The unlimit cnt.
        /// </summary>
        private const int UnlimitCnt = 60001;
        /// <summary>
        /// Gets the calls remaining.
        /// </summary>
        public int CallsRemaining{ get; private set; }

        /// <summary>
        /// Gets the limit requests left.
        /// </summary>
        public int LimitRequestsLeft{ get; private set; }
        /// <summary>
        /// Gets the limit time reset ms.
        /// </summary>
        public int LimitTimeResetMs{ get; private set; }

        /// <summary>
        /// Gets a value indicating whether unlimited calls is count.
        /// </summary>
        public bool IsUnlimitedCallsCount
		{
			get
			{
				if( this.CallsRemaining != -1 && this.CallsRemaining > UnlimitCnt )
					return true; // because plan of client is Enterprise and he shouldn't have any delays

				if( this.LimitRequestsLeft != -1 ) // it means that client use OAuth and we should check LimitRequestsLeft
					return this.LimitRequestsLeft > 20;

				return this.CallsRemaining > 100; // it means that client use BasicAuth and CallsRemaining contains number of API requests remaining for the current period
			}
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="BigCommerceLimits"/> class.
        /// </summary>
        /// <param name="callsRemaining">The calls remaining.</param>
        /// <param name="limitRequestsLeft">The limit requests left.</param>
        /// <param name="limitTimeResetMs">The limit time reset ms.</param>
        public BigCommerceLimits( int callsRemaining = -1, int limitRequestsLeft = -1, int limitTimeResetMs = -1 )
		{
			this.CallsRemaining = callsRemaining;
			this.LimitRequestsLeft = limitRequestsLeft;
			this.LimitTimeResetMs = limitTimeResetMs;
		}
	}
}

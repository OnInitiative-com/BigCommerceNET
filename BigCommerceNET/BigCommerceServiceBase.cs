using System;
using System.Threading;
using System.Threading.Tasks;
using BigCommerceNET.Models.Throttling;

namespace BigCommerceNET
{
    /// <summary>
    /// The big commerce service base.
    /// </summary>
    public abstract class BigCommerceServiceBase
	{
        //since we have 20000 api calls per hour:
        //we need to grant not more than 5 api calls per second
        //to have 18000 per hour and 2000 calls for the retry needs
        /// <summary>
        /// The default api delay.
        /// </summary>
        private readonly TimeSpan DefaultApiDelay = TimeSpan.FromMilliseconds( 200 );
        /// <summary>
        /// Request max limit.
        /// </summary>
        protected int RequestMaxLimit = 250;
        /// <summary>
        /// Request min limit.
        /// </summary>
        protected  int RequestMinLimit = 50;
        /// <summary>
        /// The max threads count.
        /// </summary>
        protected const int MaxThreadsCount = 5;

        /// <summary>
        /// Creates the api delay.
        /// </summary>
        /// <param name="limits">The limits.</param>
        /// <returns>A Task.</returns>
        protected Task CreateApiDelay( IBigCommerceRateLimits limits )
		{
			return this.CreateApiDelay( limits, CancellationToken.None );
		}

        /// <summary>
        /// Creates the api delay.
        /// </summary>
        /// <param name="limits">The limits.</param>
        /// <param name="token">The token.</param>
        /// <returns>A Task.</returns>
        protected Task CreateApiDelay( IBigCommerceRateLimits limits, CancellationToken token )
		{
			return limits.IsUnlimitedCallsCount ? Task.FromResult( 0 ) : Task.Delay( limits.LimitTimeResetMs != -1 ? TimeSpan.FromMilliseconds( limits.LimitTimeResetMs ) : this.DefaultApiDelay, token );
		}

        /// <summary>
        /// Calculate pages count.
        /// </summary>
        /// <param name="itemsCount">The items count.</param>
        /// <returns>An integer.</returns>
        protected int CalculatePagesCount( int itemsCount )
		{
			var result = ( int )Math.Ceiling( ( double )itemsCount / RequestMaxLimit );
			return result;
		}

        /// <summary>
        /// Gets the marker.
        /// </summary>
        /// <returns>A string.</returns>
        protected string GetMarker()
		{
			var marker = Guid.NewGuid().ToString();
			return marker;
		}
	}
}
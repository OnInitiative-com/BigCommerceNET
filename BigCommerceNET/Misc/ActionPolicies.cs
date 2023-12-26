using Netco.ActionPolicyServices;
using Netco.Utils;

namespace BigCommerceNET.Misc
{
    /// <summary>
    /// The action policies.
    /// </summary>
    public static class ActionPolicies
	{
#if DEBUG
        /// <summary>
        /// The retry count.
        /// </summary>
        public const int RetryCount = 1;
#else
		public const int RetryCount = 10;
#endif

        /// <summary>
        /// Submits a <see cref="ActionPolicy"/>.
        /// </summary>
        /// <param name="marker">The marker.</param>
        /// <param name="url">The url.</param>
        /// <returns>An ActionPolicy.</returns>
        public static ActionPolicy Submit( string marker, string url )
		{
			return ActionPolicy.Handle< Exception >().Retry( RetryCount, ( ex, i ) =>
			{
				var delay = TimeSpan.FromSeconds( 5 + 20 * i );
				BigCommerceLogger.LogTraceException( new RetryInfo()
				{
					Mark = marker,
					Url = url,
					CurrentRetryAttempt = i,
					TotalRetriesAttempts = RetryCount,
					DelayInSeconds = delay.TotalSeconds,
					Category = MessageCategoryEnum.Warning
				}, ex );
				SystemUtil.Sleep( delay );
			} );
		}

        /// <summary>
        /// Submits a <see cref="ActionPolicyAsync"/>. asynchronously.
        /// </summary>
        /// <param name="marker">The marker.</param>
        /// <param name="url">The url.</param>
        /// <returns>An ActionPolicyAsync.</returns>
        public static ActionPolicyAsync SubmitAsync( string marker, string url )
		{
			return ActionPolicyAsync.Handle< Exception >().RetryAsync( RetryCount, async ( ex, i ) =>
			{
				var delay = TimeSpan.FromSeconds( 5 + 20 * i );
				BigCommerceLogger.LogTraceException( new RetryInfo()
				{
					Mark = marker,
					Url = url,
					CurrentRetryAttempt = i,
					TotalRetriesAttempts = RetryCount,
					DelayInSeconds = delay.TotalSeconds,
					Category = MessageCategoryEnum.Warning
				}, ex );
				await Task.Delay( delay );
			} );
		}

        /// <summary>
        /// Gets a <see cref="ActionPolicy"/>.
        /// </summary>
        /// <param name="marker">The marker.</param>
        /// <param name="url">The url.</param>
        /// <returns>An ActionPolicy.</returns>
        public static ActionPolicy Get( string marker, string url )
		{
			return ActionPolicy.Handle< Exception >().Retry( RetryCount, ( ex, retryAttempt ) => { 
				var delay = TimeSpan.FromSeconds( 5 + 20 * retryAttempt );
				BigCommerceLogger.LogTraceException( new RetryInfo()
				{
					Mark = marker,
					Url = url,
					CurrentRetryAttempt = retryAttempt,
					TotalRetriesAttempts = RetryCount,
					DelayInSeconds = delay.TotalSeconds,
					Category = MessageCategoryEnum.Warning
				}, ex );
				SystemUtil.Sleep( delay );
			} );
		}

        /// <summary>
        /// Gets a <see cref="ActionPolicyAsync"/>. asynchronously.
        /// </summary>
        /// <param name="marker">The marker.</param>
        /// <param name="url">The url.</param>
        /// <returns>An ActionPolicyAsync.</returns>
        public static ActionPolicyAsync GetAsync( string marker, string url )
		{
			return ActionPolicyAsync.Handle< Exception >().RetryAsync( RetryCount, async ( ex, retryAttempt ) => { 
				var delay = TimeSpan.FromSeconds( 5 + 20 * retryAttempt );
				BigCommerceLogger.LogTraceException( new RetryInfo()
				{
					Mark = marker,
					Url = url,
					CurrentRetryAttempt = retryAttempt,
					TotalRetriesAttempts = RetryCount,
					DelayInSeconds = delay.TotalSeconds,
					Category = MessageCategoryEnum.Warning
				}, ex );
				await Task.Delay( delay );
			} );
		}

        /// <summary>
        /// Logs the retry and wait.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="marker">The marker.</param>
        /// <param name="url">The url.</param>
        /// <param name="retryAttempt">The retry attempt.</param>
        public static void LogRetryAndWait( Exception ex, string marker, string url, int retryAttempt )
		{
			var delay = TimeSpan.FromSeconds( 5 + 20 * retryAttempt );
			BigCommerceLogger.LogTraceException( new RetryInfo()
			{
				Mark = marker,
				Url = url,
				CurrentRetryAttempt = retryAttempt,
				TotalRetriesAttempts = RetryCount,
				DelayInSeconds = delay.TotalSeconds,
				Category = MessageCategoryEnum.Warning
			}, ex );
			SystemUtil.Sleep( delay );
		}

        /// <summary>
        /// Logs the retry and wait asynchronously.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="marker">The marker.</param>
        /// <param name="url">The url.</param>
        /// <param name="retryAttempt">The retry attempt.</param>
        /// <returns>A Task.</returns>
        public static async Task LogRetryAndWaitAsync( Exception ex, string marker, string url, int retryAttempt )
		{
			var delay = TimeSpan.FromSeconds( 5 + 20 * retryAttempt );
			BigCommerceLogger.LogTraceException( new RetryInfo()
			{
				Mark = marker,
				Url = url,
				CurrentRetryAttempt = retryAttempt,
				TotalRetriesAttempts = RetryCount,
				DelayInSeconds = delay.TotalSeconds,
				Category = MessageCategoryEnum.Warning
			}, ex );
			await Task.Delay( delay );
		}
	}
}
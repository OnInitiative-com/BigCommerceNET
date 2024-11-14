using BigCommerceNET.Models.Throttling;

namespace BigCommerceNET.Models
{
    /// <summary>
    /// The big commerce response.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class BigCommerceResponse< T > where T : class
	{
        /// <summary>
        /// Gets the response.
        /// </summary>
        public T Response{ get; private set; }
        /// <summary>
        /// Gets the limits.
        /// </summary>
        public IBigCommerceRateLimits Limits{ get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BigCommerceResponse"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="limits">The limits.</param>
        public BigCommerceResponse( T response, IBigCommerceRateLimits limits )
		{
			this.Response = response;
			this.Limits = limits;
		}
	}
}

using BigCommerceNET.Services;

namespace BigCommerceNET
{
    /// <summary>
    /// The big commerce base categories service.
    /// </summary>
    abstract class BigCommerceBaseCategoriesService : BigCommerceServiceBase
	{
        /// <summary>
        /// The web request services.
        /// </summary>
        protected readonly WebRequestServices? _webRequestServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="BigCommerceBaseCategoriesService"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        public BigCommerceBaseCategoriesService( WebRequestServices services )
		{
			if (services is not null)
			{
				this._webRequestServices = services;
			}
			
		}
	
	}
}

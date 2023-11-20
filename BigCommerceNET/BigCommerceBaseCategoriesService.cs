using BigCommerceNET.Services;

namespace BigCommerceNET
{
    abstract class BigCommerceBaseCategoriesService : BigCommerceServiceBase
	{
		protected readonly WebRequestServices? _webRequestServices;

		public BigCommerceBaseCategoriesService( WebRequestServices services )
		{
			if (services is not null)
			{
				this._webRequestServices = services;
			}
			
		}
	
	}
}

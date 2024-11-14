using BigCommerceNET.Models.Configuration;
using BigCommerceNET.Services;
using System;

namespace BigCommerceNET
{
    /// <summary>
    /// The big commerce factory interface.
    /// </summary>
    public interface IBigCommerceFactory
	{
        /// <summary>
        /// Creates the orders service.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <returns>An IBigCommerceOrdersService.</returns>
        IBigCommerceOrdersService CreateOrdersService( BigCommerceConfig config );
        /// <summary>
        /// Creates the products service.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <returns>An IBigCommerceProductsService.</returns>
        IBigCommerceProductsService CreateProductsService( BigCommerceConfig config );
        /// <summary>
        /// Creates the categories service.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <returns>An IBigCommerceCategoriesService.</returns>
        IBigCommerceCategoriesService CreateCategoriesService(BigCommerceConfig config);
	}

    /// <summary>
    /// The big commerce factory.
    /// </summary>
    public sealed class BigCommerceFactory : IBigCommerceFactory
	{
        /// <summary>
        /// Creates the orders service.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <returns>An IBigCommerceOrdersService.</returns>
        public IBigCommerceOrdersService CreateOrdersService( BigCommerceConfig config )
		{
			return new BigCommerceOrdersService( config );
		}

        /// <summary>
        /// Creates the categories service.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <returns>An IBigCommerceCategoriesService.</returns>
        public IBigCommerceCategoriesService CreateCategoriesService(BigCommerceConfig config)
		{
			var apiVersion = config.GetAPIVersion();
			var marker = Guid.NewGuid().ToString();
			var services = new WebRequestServices(config, marker);

			return new BigCommerceCategoriesServiceV3(services);
		}

        /// <summary>
        /// Creates the products service.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <returns>An IBigCommerceProductsService.</returns>
        public IBigCommerceProductsService CreateProductsService( BigCommerceConfig config )
		{
			var apiVersion = config.GetAPIVersion();
			var marker = Guid.NewGuid().ToString();
			var services = new WebRequestServices( config, marker );

			if ( apiVersion == APIVersion.V2 )
			{
				return new BigCommerceProductsServiceV2( services );
			}

			return new BigCommerceProductsServiceV3( services );
		}
	}
}
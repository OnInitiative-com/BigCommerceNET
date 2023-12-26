using System;
using System.Text;
using BigCommerceNET.Models.Command;
using BigCommerceNET.Models.Configuration;

namespace BigCommerceNET.Services
{
    /// <summary>
    /// The params builder.
    /// </summary>
    internal static class ParamsBuilder
	{
        /// <summary>
        /// Empty params.
        /// </summary>
        public static readonly string EmptyParams = string.Empty;

        /// <summary>
        /// Creates the orders params.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>A string.</returns>
        public static string CreateOrdersParams( DateTime startDate, DateTime endDate )
		{
			var endpoint = string.Format( "?{0}={1}&{2}={3}",
				BigCommerceParam.OrdersModifiedDateFrom.Name, DateTime.SpecifyKind( startDate, DateTimeKind.Utc ).ToString( "o" ),
				BigCommerceParam.OrdersModifiedDateTo.Name, DateTime.SpecifyKind( endDate, DateTimeKind.Utc ).ToString( "o" ) );
			return endpoint;
		}

        /// <summary>
        /// Creates the product update endpoint.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns>A string.</returns>
        public static string CreateProductUpdateEndpoint( long productId )
		{
			var endpoint = string.Format( "{0}.json", productId );
			return endpoint;
		}

        /// <summary>
        /// Creates the product option update endpoint.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <param name="optionId">The option id.</param>
        /// <returns>A string.</returns>
        public static string CreateProductOptionUpdateEndpoint( long productId, long optionId )
		{
			return string.Format( "{0}/skus/{1}.json", productId, optionId );
		}

        /// <summary>
        /// Creates the get single page params.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <returns>A string.</returns>
        public static string CreateGetSinglePageParams( BigCommerceCommandConfig config )
		{
			var endpoint = string.Format( "?{0}={1}", BigCommerceParam.Limit.Name, config.Limit );
			return endpoint;
		}

        /// <summary>
        /// Creates the get next page params.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <returns>A string.</returns>
        public static string CreateGetNextPageParams( BigCommerceCommandConfig config )
		{
			var endpoint = string.Format( "?{0}={1}&{2}={3}",
				BigCommerceParam.Limit.Name, config.Limit,
				BigCommerceParam.Page.Name, config.Page );
			return endpoint;
		}

        /// <summary>
        /// Gets the fields for product sync.
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetFieldsForProductSync()
		{
			return "&include=id,inventory_level,sku,inventory_tracking,skus,upc,name,description,price,sale_price,retail_price,cost_price,weight,brand_id,primary_image";
		}

        /// <summary>
        /// Gets the fields for inventory sync.
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetFieldsForInventorySync()
		{
			return "&include=id,inventory_level,sku,upc,inventory_tracking,skus";
		}

        /// <summary>
        /// Concats the params.
        /// </summary>
        /// <param name="mainEndpoint">The main endpoint.</param>
        /// <param name="endpoints">The endpoints.</param>
        /// <returns>A string.</returns>
        public static string ConcatParams( this string mainEndpoint, params string[] endpoints )
		{
			var result = new StringBuilder( mainEndpoint );

			foreach( var endpoint in endpoints )
				result.Append( endpoint.Replace( "?", "&" ) );

			return result.ToString();
		}
	}
}
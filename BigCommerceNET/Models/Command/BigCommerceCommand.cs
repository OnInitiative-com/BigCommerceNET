namespace BigCommerceNET.Models.Command
{
    /// <summary>
    /// The big commerce command.
    /// </summary>
    public class BigCommerceCommand
	{
        /// <summary>
        /// The unknown.
        /// </summary>
        public static readonly BigCommerceCommand Unknown = new BigCommerceCommand( string.Empty );

        /// <summary>
        /// The get products v2.
        /// </summary>
        public static readonly BigCommerceCommand GetProductsV2 = new BigCommerceCommand( "/api/v2/products.json" );
        /// <summary>
        /// The get orders v2.
        /// </summary>
        public static readonly BigCommerceCommand GetOrdersV2 = new BigCommerceCommand( "/api/v2/orders.json" );
        /// <summary>
        /// The get orders count v2.
        /// </summary>
        public static readonly BigCommerceCommand GetOrdersCountV2 = new BigCommerceCommand( "/api/v2/orders/count.json" );
        /// <summary>
        /// The get products count v2.
        /// </summary>
        public static readonly BigCommerceCommand GetProductsCountV2 = new BigCommerceCommand( "/api/v2/products/count.json" );
        /// <summary>
        /// The update product v2.
        /// </summary>
        public static readonly BigCommerceCommand UpdateProductV2 = new BigCommerceCommand( "/api/v2/products/" );
        /// <summary>
        /// The get store v2.
        /// </summary>
        public static readonly BigCommerceCommand GetStoreV2 = new BigCommerceCommand( "/api/v2/store.json" );
        /// <summary>
        /// The get brands v2.
        /// </summary>
        public static readonly BigCommerceCommand GetBrandsV2 = new BigCommerceCommand( "/api/v2/brands.json" );

        /// <summary>
        /// The get products v2 o auth.
        /// </summary>
        public static readonly BigCommerceCommand GetProductsV2_OAuth = new BigCommerceCommand( "/v2/products" );
        /// <summary>
        /// The get orders v2 o auth.
        /// </summary>
        public static readonly BigCommerceCommand GetOrdersV2_OAuth = new BigCommerceCommand( "/v2/orders" );
        /// <summary>
        /// The get orders count v2 o auth.
        /// </summary>
        public static readonly BigCommerceCommand GetOrdersCountV2_OAuth = new BigCommerceCommand( "/v2/orders/count" );
        /// <summary>
        /// The get products count v2 o auth.
        /// </summary>
        public static readonly BigCommerceCommand GetProductsCountV2_OAuth = new BigCommerceCommand( "/v2/products/count" );
        /// <summary>
        /// The update product v2 o auth.
        /// </summary>
        public static readonly BigCommerceCommand UpdateProductV2_OAuth = new BigCommerceCommand( "/v2/products/" );
        /// <summary>
        /// The get store v2 o auth.
        /// </summary>
        public static readonly BigCommerceCommand GetStoreV2_OAuth = new BigCommerceCommand( "/v2/store" );
        /// <summary>
        /// The get brands v2 o auth.
        /// </summary>
        public static readonly BigCommerceCommand GetBrandsV2_OAuth = new BigCommerceCommand( "/v2/brands" );

        /// <summary>
        /// The get products v3.
        /// </summary>
        public static readonly BigCommerceCommand GetProductsV3 = new BigCommerceCommand( "/v3/catalog/products" );
        /// <summary>
        /// The update products v3.
        /// </summary>
        public static readonly BigCommerceCommand UpdateProductsV3 = new BigCommerceCommand( "/v3/catalog/products" );

        /// <summary>
        /// The get categories v3.
        /// </summary>
        public static readonly BigCommerceCommand GetCategoriesV3 = new BigCommerceCommand("/v3/catalog/categories");

        /// <summary>
        /// Prevents a default instance of the <see cref="BigCommerceCommand"/> class from being created.
        /// </summary>
        /// <param name="command">The command.</param>
        private BigCommerceCommand( string command )
		{
			this.Command = command;
		}

        /// <summary>
        /// Gets the command.
        /// </summary>
        public string Command{ get; private set; }
	}
}
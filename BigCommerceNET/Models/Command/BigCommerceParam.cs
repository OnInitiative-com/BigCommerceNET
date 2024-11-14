namespace BigCommerceNET.Models.Command
{
    /// <summary>
    /// The big commerce param.
    /// </summary>
    internal class BigCommerceParam
	{
        /// <summary>
        /// The unknown.
        /// </summary>
        public static readonly BigCommerceParam Unknown = new BigCommerceParam( string.Empty );
        /// <summary>
        /// The orders modified date from.
        /// </summary>
        public static readonly BigCommerceParam OrdersModifiedDateFrom = new BigCommerceParam( "min_date_modified" );
        /// <summary>
        /// The orders modified date to.
        /// </summary>
        public static readonly BigCommerceParam OrdersModifiedDateTo = new BigCommerceParam( "max_date_modified" );
        /// <summary>
        /// The limit.
        /// </summary>
        public static readonly BigCommerceParam Limit = new BigCommerceParam( "limit" );
        /// <summary>
        /// The page.
        /// </summary>
        public static readonly BigCommerceParam Page = new BigCommerceParam( "page" );

        /// <summary>
        /// Prevents a default instance of the <see cref="BigCommerceParam"/> class from being created.
        /// </summary>
        /// <param name="name">The name.</param>
        private BigCommerceParam( string name )
		{
			this.Name = name;
		}

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }
	}
}
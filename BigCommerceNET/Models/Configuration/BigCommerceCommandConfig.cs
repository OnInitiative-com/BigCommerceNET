
namespace BigCommerceNET.Models.Configuration
{
    /// <summary>
    /// The big commerce command config.
    /// </summary>
    internal class BigCommerceCommandConfig
	{
        /// <summary>
        /// Gets the page.
        /// </summary>
        public int Page { get; private set; }
        /// <summary>
        /// Gets the limit.
        /// </summary>
        public int Limit { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BigCommerceCommandConfig"/> class.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="limit">The limit.</param>
        public BigCommerceCommandConfig( int page, int limit )
			: this( limit )
		{
			if (page > 0 && limit > 0)
				this.Page = page;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="BigCommerceCommandConfig"/> class.
        /// </summary>
        /// <param name="limit">The limit.</param>
        public BigCommerceCommandConfig( int limit )
		{
			if (limit > 0 )
				this.Limit = limit;
		}
	}
}
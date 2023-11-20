
namespace BigCommerceNET.Models.Configuration
{
	internal class BigCommerceCommandConfig
	{
		public int Page { get; private set; }
		public int Limit { get; private set; }

		public BigCommerceCommandConfig( int page, int limit )
			: this( limit )
		{
			if (page > 0 && limit > 0)
				this.Page = page;
		}

		public BigCommerceCommandConfig( int limit )
		{
			if (limit > 0 )
				this.Limit = limit;
		}
	}
}
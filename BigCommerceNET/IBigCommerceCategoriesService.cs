using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BigCommerceNET.Models.Category;

namespace BigCommerceNET
{
    /// <summary>
    /// The big commerce categories service interface.
    /// </summary>
    public interface IBigCommerceCategoriesService
	{
        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns><![CDATA[A List<BigCommerceCategory>.]]></returns>
        List<BigCommerceCategory> GetCategories();
        /// <summary>
        /// Gets the categories asynchronously.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns><![CDATA[A Task< List<BigCommerceCategory> >.]]></returns>
        Task< List<BigCommerceCategory> > GetCategoriesAsync( CancellationToken token);
		
	}
}
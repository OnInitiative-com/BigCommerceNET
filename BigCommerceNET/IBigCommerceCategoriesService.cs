using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BigCommerceNET.Models.Category;

namespace BigCommerceNET
{
	public interface IBigCommerceCategoriesService
	{
        List<BigCommerceCategory> GetCategories();
        Task< List<BigCommerceCategory> > GetCategoriesAsync( CancellationToken token);
		
	}
}
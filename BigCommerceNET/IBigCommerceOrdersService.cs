using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BigCommerceNET.Models.Order;

namespace BigCommerceNET
{
	public interface IBigCommerceOrdersService
	{
        List<BigCommerceOrder> GetOrders(DateTime dateFrom, DateTime dateTo);
        Task< List< BigCommerceOrder > > GetOrdersAsync( DateTime dateFrom, DateTime dateTo, CancellationToken token );
	}
}
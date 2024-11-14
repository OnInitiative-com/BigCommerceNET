using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BigCommerceNET.Models.Order;

namespace BigCommerceNET
{
    /// <summary>
    /// The big commerce orders service interface.
    /// </summary>
    public interface IBigCommerceOrdersService
	{
        /// <summary>
        /// Gets the orders.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <returns><![CDATA[A List<BigCommerceOrder>.]]></returns>
        List<BigCommerceOrder> GetOrders(DateTime dateFrom, DateTime dateTo);
        /// <summary>
        /// Gets the orders asynchronously.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="token">The token.</param>
        /// <returns><![CDATA[A Task< List< BigCommerceOrder > >.]]></returns>
        Task< List< BigCommerceOrder > > GetOrdersAsync( DateTime dateFrom, DateTime dateTo, CancellationToken token );
	}
}
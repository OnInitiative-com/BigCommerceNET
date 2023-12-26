using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BigCommerceNET.Models.Product;

namespace BigCommerceNET
{
    /// <summary>
    /// The big commerce products service interface.
    /// </summary>
    public interface IBigCommerceProductsService
	{
        /// <summary>
        /// Get the store name.
        /// </summary>
        /// <returns>A string.</returns>
        string GetStoreName();
        /// <summary>
        /// Get the store domain.
        /// </summary>
        /// <returns>A string.</returns>
        string GetStoreDomain();
        /// <summary>
        /// Get the store safe URL.
        /// </summary>
        /// <returns>A string.</returns>
        string GetStoreSafeURL();
        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <param name="includeExtendInfo">If true, include extend info.</param>
        /// <returns><![CDATA[A List<BigCommerceProduct>.]]></returns>
        List<BigCommerceProduct> GetProducts(bool includeExtendInfo = false);
        /// <summary>
        /// Gets the products asynchronously.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="includeExtendInfo">If true, include extend info.</param>
        /// <returns><![CDATA[A Task<List<BigCommerceProduct>>.]]></returns>
        Task<List<BigCommerceProduct>> GetProductsAsync(CancellationToken token, bool includeExtendInfo = false);
        /// <summary>
        /// Updates the products.
        /// </summary>
        /// <param name="products">The products.</param>
        void UpdateProducts(List<BigCommerceProduct> products);
        /// <summary>
        /// Updates the products asynchronously.
        /// </summary>
        /// <param name="products">The products.</param>
        /// <param name="token">The token.</param>
        /// <returns>A Task.</returns>
        Task UpdateProductsAsync(List<BigCommerceProduct> products, CancellationToken token);
        /// <summary>
        /// Updates the product options.
        /// </summary>
        /// <param name="productOptions">The product options.</param>
        void UpdateProductOptions(List<BigCommerceProductOption> productOptions);
        /// <summary>
        /// Updates the product options asynchronously.
        /// </summary>
        /// <param name="productOptions">The product options.</param>
        /// <param name="token">The token.</param>
        /// <returns>A Task.</returns>
        Task UpdateProductOptionsAsync(List<BigCommerceProductOption> productOptions, CancellationToken token);
    }
}
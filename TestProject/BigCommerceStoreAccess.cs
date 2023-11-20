using BigCommerceNET.Models.Configuration;
using BigCommerceNET;
using BigCommerceNET.Models.Category;
using BigCommerceNET.Models.Product;

namespace TestProject
{
    public class BigCommerceStoreAccess
    {
        private readonly IBigCommerceFactory BigCommerceFactory = new BigCommerceFactory();
        private BigCommerceConfig ConfigV3;

        private const string ShortShopName = "your-shop-name";
        private const string ClientId = "your-client-id";
        private const string ClientSecret = "your-client-secret";
        private const string AccessToken = "your-access-token";

        public BigCommerceStoreAccess()
        {
            this.ConfigV3 = new BigCommerceConfig(ShortShopName, ClientId, ClientSecret, AccessToken);
        }

        /// <summary>
        /// Gets the list of Categories from the BigCommerce store.
        /// </summary>
        public List<BigCommerceCategory> GetCategories()
        {
            try
            {
                var service = this.BigCommerceFactory.CreateCategoriesService(this.ConfigV3);

                List<BigCommerceCategory> categories = service.GetCategories();

                return categories;
            }
            catch (System.Net.WebException webEx)
            {
                if (webEx.Message.Contains("Cloudflare"))
                    throw new Exception("Could not access the web server. Check Cloudflare IP whitelist rules.");
                else
                    throw new Exception("Could not access the web server. Check Firewall rules.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the list of Products from the BigCommerce store.
        /// </summary>
        public async Task<List<BigCommerceProduct>> GetProducts()
        {
            try
            {
                var service = this.BigCommerceFactory.CreateProductsService(this.ConfigV3);

                var products = await service.GetProductsAsync(CancellationToken.None, true);

                return products;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

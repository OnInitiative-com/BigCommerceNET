using BigCommerceNET.Misc;
using BigCommerceNET.Models.Command;
using BigCommerceNET.Models.Configuration;
using BigCommerceNET.Models.Product;
using BigCommerceNET.Services;
using Netco.Extensions;

namespace BigCommerceNET
{
    /// <summary>
    /// The big commerce base products service.
    /// </summary>
    abstract class BigCommerceBaseProductsService : BigCommerceServiceBase
	{
        /// <summary>
        /// The web request services.
        /// </summary>
        protected readonly WebRequestServices _webRequestServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="BigCommerceBaseProductsService"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        public BigCommerceBaseProductsService(WebRequestServices services)
		{
			if (services is not null)
			{
                this._webRequestServices = services;
            }
			else
                throw new ArgumentException("The 'services' parameter is missing or empty.");
        }

        /// <summary>
        /// Fill weight unit.
        /// </summary>
        /// <param name="products">The products.</param>
        /// <param name="marker">The marker.</param>
        protected virtual void FillWeightUnit(IEnumerable<BigCommerceProduct> products, string marker)
        {
            var command = BigCommerceCommand.GetStoreV2_OAuth;
            var store = ActionPolicies.Get(marker, command.Command).Get(() =>
             this._webRequestServices.GetResponseByRelativeUrl<BigCommerceStore>(command, string.Empty, marker));
            this.CreateApiDelay(store.Limits).Wait(); //API requirement

            foreach (var product in products)
            {
                product.WeightUnit = store.Response.WeightUnits!;
            }
        }


        /// <summary>
        /// Fill weight unit asynchronously.
        /// </summary>
        /// <param name="products">The products.</param>
        /// <param name="token">The token.</param>
        /// <param name="marker">The marker.</param>
        /// <returns>A Task.</returns>
        protected virtual async Task FillWeightUnitAsync(IEnumerable<BigCommerceProduct> products, CancellationToken token, string marker)
		{
			var command = BigCommerceCommand.GetStoreV2_OAuth;
			var store = await ActionPolicies.GetAsync(marker, command.Command).Get(async () =>
			 await this._webRequestServices.GetResponseByRelativeUrlAsync<BigCommerceStore>(command, string.Empty, marker));
			await this.CreateApiDelay(store.Limits, token); //API requirement

			foreach (var product in products)
			{
				product.WeightUnit = store.Response.WeightUnits!;
			}
		}

        /// <summary>
        /// Fill brands.
        /// </summary>
        /// <param name="products">The products.</param>
        /// <param name="marker">The marker.</param>
        protected virtual void FillBrands(IEnumerable<BigCommerceProduct> products, string marker)
        {
            var brands = new List<BigCommerceBrand>();
            for (var i = 1; i < int.MaxValue; i++)
            {
                var endpoint = ParamsBuilder.CreateGetNextPageParams(new BigCommerceCommandConfig(i, RequestMaxLimit));
                var brandsWithinPage = ActionPolicies.Get(marker, endpoint).Get(() =>
                 this._webRequestServices.GetResponseByRelativeUrl<List<BigCommerceBrand>>(BigCommerceCommand.GetBrandsV2_OAuth, endpoint, marker));
                this.CreateApiDelay(brandsWithinPage.Limits).Wait(); //API requirement

                if (brandsWithinPage.Response == null)
                    break;

                brands.AddRange(brandsWithinPage.Response);
                if (brandsWithinPage.Response.Count < RequestMaxLimit)
                    break;
            }

            this.FillBrandsForProducts(products, brands);
        }

        /// <summary>
        /// Fill brands asynchronously.
        /// </summary>
        /// <param name="products">The products.</param>
        /// <param name="token">The token.</param>
        /// <param name="marker">The marker.</param>
        /// <returns>A Task.</returns>
        protected virtual async Task FillBrandsAsync(IEnumerable<BigCommerceProduct> products, CancellationToken token, string marker)
		{
			var brands = new List<BigCommerceBrand>();
			for (var i = 1; i < int.MaxValue; i++)
			{
				var endpoint = ParamsBuilder.CreateGetNextPageParams(new BigCommerceCommandConfig(i, RequestMaxLimit));
				var brandsWithinPage = await ActionPolicies.GetAsync(marker, endpoint).Get(async () =>
				 await this._webRequestServices.GetResponseByRelativeUrlAsync<List<BigCommerceBrand>>(BigCommerceCommand.GetBrandsV2_OAuth, endpoint, marker));
				await this.CreateApiDelay(brandsWithinPage.Limits, token); //API requirement

				if (brandsWithinPage.Response == null)
					break;

				brands.AddRange(brandsWithinPage.Response);
				if (brandsWithinPage.Response.Count < RequestMaxLimit)
					break;
			}

			this.FillBrandsForProducts(products, brands);
		}

        /// <summary>
        /// Fill products skus.
        /// </summary>
        /// <param name="products">The products.</param>
        /// <param name="marker">The marker.</param>
        protected void FillProductsSkus(IEnumerable<BigCommerceProduct> products, string marker)
        {
            foreach (var product in products.Where(product => product.InventoryTracking.Equals(InventoryTrackingEnum.sku)))
            {
                for (var i = 1; i < int.MaxValue; i++)
                {
                    var endpoint = ParamsBuilder.CreateGetNextPageParams(new BigCommerceCommandConfig(i, RequestMaxLimit));
                    var options = ActionPolicies.Get(marker, endpoint).Get(() =>
                     this._webRequestServices.GetResponseByRelativeUrl<List<BigCommerceProductOption>>(product.ProductOptionsReference!.Url!, endpoint, marker));
                    this.CreateApiDelay(options.Limits).Wait(); //API requirement

                    if (options.Response == null)
                        break;
                    product.ProductOptions!.AddRange(options.Response);
                    if (options.Response.Count < RequestMaxLimit)
                        break;
                }
            }
        }

        /// <summary>
        /// Fill products skus asynchronously.
        /// </summary>
        /// <param name="products">The products.</param>
        /// <param name="isUnlimit">If true, is unlimit.</param>
        /// <param name="token">The token.</param>
        /// <param name="marker">The marker.</param>
        /// <returns>A Task.</returns>
        protected async Task FillProductsSkusAsync(IEnumerable<BigCommerceProduct> products, bool isUnlimit, CancellationToken token, string marker)
		{
			var threadCount = isUnlimit ? MaxThreadsCount : 1;
			var skuProducts = products.Where(product => product.InventoryTracking.Equals(InventoryTrackingEnum.sku));
			await skuProducts.DoInBatchAsync(threadCount, async product =>
			{
				for (var i = 1; i < int.MaxValue; i++)
				{
					var endpoint = ParamsBuilder.CreateGetNextPageParams(new BigCommerceCommandConfig(i, RequestMaxLimit));
					var options = await ActionPolicies.GetAsync(marker, endpoint).Get(async () =>
					 await this._webRequestServices.GetResponseByRelativeUrlAsync<List<BigCommerceProductOption>>(product.ProductOptionsReference!.Url!, endpoint, marker));
					await this.CreateApiDelay(options.Limits, token); //API requirement

					if (options.Response == null)
						break;
					product.ProductOptions!.AddRange(options.Response);
					if (options.Response.Count < RequestMaxLimit)
						break;
				}
			});
		}

        /// <summary>
        /// Fill brands for products.
        /// </summary>
        /// <param name="products">The products.</param>
        /// <param name="brands">The brands.</param>
        protected void FillBrandsForProducts(IEnumerable<BigCommerceProduct> products, List<BigCommerceBrand> brands)
		{
			foreach (var product in products)
			{
				if (!product.BrandId.HasValue)
				{
					product.BrandName = null!;
					continue;
				}

				var brand = brands.FirstOrDefault(x => x.Id == product.BrandId.Value);
				if (brand == null)
				{
					product.BrandName = null!;
					continue;
				}

				product.BrandName = brand.Name;
			}
		}

        /// <summary>
        /// Get the store name.
        /// </summary>
        /// <param name="marker">The marker.</param>
        /// <returns>A string.</returns>
        protected virtual string GetStoreName(string marker)
        {
            var command = BigCommerceCommand.GetStoreV2_OAuth;
            var store = ActionPolicies.Get(marker, command.Command).Get(() =>
            this._webRequestServices.GetResponseByRelativeUrl<BigCommerceStore>(command, string.Empty, marker));
            this.CreateApiDelay(store.Limits).Wait(); //API requirement

            return store.Response.Name!;

        }

        /// <summary>
        /// Gets the domain.
        /// </summary>
        /// <param name="marker">The marker.</param>
        /// <returns>A string.</returns>
        protected virtual string GetDomain(string marker)
        {
            var command = BigCommerceCommand.GetStoreV2_OAuth;
            var store = ActionPolicies.Get(marker, command.Command).Get(() =>
             this._webRequestServices.GetResponseByRelativeUrl<BigCommerceStore>(command, string.Empty, marker));
            this.CreateApiDelay(store.Limits).Wait(); //API requirement

            return store.Response.Domain!;
        }

        /// <summary>
        /// Gets the secure URL.
        /// </summary>
        /// <param name="marker">The marker.</param>
        /// <returns>A string.</returns>
        protected virtual string GetSecureURL(string marker)
        {
            var command = BigCommerceCommand.GetStoreV2_OAuth;
            var store = ActionPolicies.Get(marker, command.Command).Get(() =>
             this._webRequestServices.GetResponseByRelativeUrl<BigCommerceStore>(command, string.Empty, marker));
            this.CreateApiDelay(store.Limits).Wait(); //API requirement

            return store.Response.SecureURL!;
        }

    }
}

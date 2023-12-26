using BigCommerceNET.Misc;
using BigCommerceNET.Models.Command;
using BigCommerceNET.Models.Configuration;
using BigCommerceNET.Models.Product;
using BigCommerceNET.Services;
using Netco.ActionPolicyServices;
using Netco.Extensions;
using ServiceStack;

namespace BigCommerceNET
{
    /// <summary>
    /// The big commerce products service v3.
    /// </summary>
    sealed class BigCommerceProductsServiceV3 : BigCommerceBaseProductsService, IBigCommerceProductsService
	{
        /// <summary>
        /// The inventory tracking by option.
        /// </summary>
        private const string _inventoryTrackingByOption = "variant";

        /// <summary>
        /// Initializes a new instance of the <see cref="BigCommerceProductsServiceV3"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        public BigCommerceProductsServiceV3( WebRequestServices services ) : base( services )
		{
		}

        #region Get
        /// <summary>
        /// Get the store name.
        /// </summary>
        /// <returns>A string.</returns>
        public string GetStoreName()
        {
            var marker = this.GetMarker();
            return base.GetStoreName(marker);

        }

        /// <summary>
        /// Get the store domain.
        /// </summary>
        /// <returns>A string.</returns>
        public string GetStoreDomain()
        {
            var marker = this.GetMarker();
            return base.GetDomain(marker);

        }
        /// <summary>
        /// Get the store safe URL.
        /// </summary>
        /// <returns>A string.</returns>
        public string GetStoreSafeURL()
        {
            var marker = this.GetMarker();
            return base.GetSecureURL(marker);

        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <param name="includeExtendedInfo">If true, include extended info.</param>
        /// <returns><![CDATA[A List<BigCommerceProduct>.]]></returns>
        public List<BigCommerceProduct> GetProducts(bool includeExtendedInfo)
        {
            var mainEndpoint = "?include=variants,images";
            var products = new List<BigCommerceProduct>();
            var marker = this.GetMarker();

            for (var i = 1; i < int.MaxValue; i++)
            {
                var endpoint = mainEndpoint.ConcatParams(ParamsBuilder.CreateGetNextPageParams(new BigCommerceCommandConfig(i, RequestMaxLimit)));
                var productsWithinPage = ActionPolicy.Handle<Exception>().Retry(ActionPolicies.RetryCount, (ex, retryAttempt) =>
                {
                    if (PageAdjuster.TryAdjustPageIfResponseTooLarge(new PageInfo(i, this.RequestMaxLimit), this.RequestMinLimit, ex, out var newPageInfo))
                    {
                        i = newPageInfo.Index;
                        this.RequestMaxLimit = newPageInfo.Size;
                    }

                    ActionPolicies.LogRetryAndWait(ex, marker, endpoint, retryAttempt);
                }).Get(() => {
                    return this._webRequestServices.GetResponseByRelativeUrl<BigCommerceProductInfoData>(BigCommerceCommand.GetProductsV3, endpoint, marker);
                });

                this.CreateApiDelay(productsWithinPage.Limits).Wait(); //API requirement

                if (productsWithinPage.Response == null)
                    break;

                foreach (var product in productsWithinPage.Response.Data)
                {
                    var productImageThumbnail = product.Images!.FirstOrDefault(img => img.IsThumbnail);

                    var additional_images = product.Images;

                    var custom_url = product.Product_URL;

                    products.Add(new BigCommerceProduct
                    {
                        Id = (long)product.Id!,
                        InventoryTracking = this.ToCompatibleWithV2InventoryTrackingEnum(product.InventoryTracking!),
                        Upc = product.Upc,
                        Sku = product.Sku,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        IsProductVisible = product.IsVisible,
                        Condition = product.Condition,
                        Availability = product.Availability,
                        ProductType = product.Type,
                        SalePrice = product.SalePrice,
                        RetailPrice = product.RetailPrice,
                        CostPrice = product.CostPrice,
                        Weight = product.Weight,
                        BrandId = product.BrandId,
                        Quantity = product.Quantity,
                        Product_URL = custom_url!.ProductURL,
                        Categories = product.Categories,
                        ThumbnailImageURL = new BigCommerceProductPrimaryImages()
                        {
                            StandardUrl = productImageThumbnail != null ? productImageThumbnail.UrlStandard : string.Empty
                        },
                        ProductOptions = product.Variants!.Select(x => new BigCommerceProductOption
                        {
                            Id = x.Id,
                            ProductId = x.ProductId,
                            Sku = x.Sku,
                            Quantity = x.Quantity,
                            Upc = x.Upc,
                            Price = x.Price,
                            SalePrice = x.SalePrice,
                            IsFreeShipping = x.IsFreeShipping,
                            Weight = x.Weight,
                            Width = x.Width,
                            Height = x.Height,
                            Depth = x.Depth,
                            CostPrice = x.CostPrice,
                            ImageUrl = x.ImageUrl,
                            Attributes = x.Attributes
                        }).ToList(),
                        Main_Images = product.Images!.Select(y => new BigCommerceImage
                        {
                            ImageFile = y.ImageFile,
                            IsThumbnail = y.IsThumbnail,
                            SortOrder = y.SortOrder,
                            Description = y.Description,
                            ImageUrl = y.ImageUrl,
                            UrlStandard = y.UrlStandard,
                            DateModified = y.DateModified
                        }).ToList()
                    });
                }

                if (productsWithinPage.Response.Data.Count < RequestMaxLimit)
                    break;
            }

            if (includeExtendedInfo)
            {
                base.FillWeightUnit(products, marker);
                base.FillBrands(products, marker);
            }

            return products;
        }

        /// <summary>
        /// Gets the products asynchronously.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="includeExtendedInfo">If true, include extended info.</param>
        /// <returns><![CDATA[A Task<List<BigCommerceProduct>>.]]></returns>
        public async Task<List<BigCommerceProduct>> GetProductsAsync(CancellationToken token, bool includeExtendedInfo)
		{
			var mainEndpoint = "?include=variants,images";
			var products = new List<BigCommerceProduct>();
			var marker = this.GetMarker();

			for (var i = 1; i < int.MaxValue; i++)
			{
				var endpoint = mainEndpoint.ConcatParams(ParamsBuilder.CreateGetNextPageParams(new BigCommerceCommandConfig(i, RequestMaxLimit)));
				var productsWithinPage = await ActionPolicyAsync.Handle<Exception>().RetryAsync(ActionPolicies.RetryCount, (ex, retryAttempt) =>
				{
					if (PageAdjuster.TryAdjustPageIfResponseTooLarge(new PageInfo(i, this.RequestMaxLimit), this.RequestMinLimit, ex, out var newPageInfo))
					{
						i = newPageInfo.Index;
						this.RequestMaxLimit = newPageInfo.Size;
					}

					return ActionPolicies.LogRetryAndWaitAsync(ex, marker, endpoint, retryAttempt);
				}).Get(() => {
					return this._webRequestServices.GetResponseByRelativeUrlAsync<BigCommerceProductInfoData>(BigCommerceCommand.GetProductsV3, endpoint, marker);
				});

				await this.CreateApiDelay(productsWithinPage.Limits, token); //API requirement

				if (productsWithinPage.Response == null)
					break;

				foreach (var product in productsWithinPage.Response.Data)
				{
					var productImageThumbnail = product.Images!.FirstOrDefault(img => img.IsThumbnail);

					var additional_images = product.Images;

                    var custom_url = product.Product_URL;

                    products.Add(new BigCommerceProduct
					{
                        Id = (long)product.Id!,
                        InventoryTracking = this.ToCompatibleWithV2InventoryTrackingEnum(product.InventoryTracking!),
                        Upc = product.Upc,
                        Sku = product.Sku,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        IsProductVisible = product.IsVisible,
                        Condition = product.Condition,
                        Availability = product.Availability,
                        ProductType = product.Type,
                        SalePrice = product.SalePrice,
                        RetailPrice = product.RetailPrice,
                        CostPrice = product.CostPrice,
                        Weight = product.Weight,
                        BrandId = product.BrandId,
                        Quantity = product.Quantity,
                        Product_URL = custom_url!.ProductURL,
                        Categories = product.Categories,
                        ThumbnailImageURL = new BigCommerceProductPrimaryImages()
                        {
                            StandardUrl = productImageThumbnail != null ? productImageThumbnail.UrlStandard : string.Empty
                        },
                        ProductOptions = product.Variants!.Select(x => new BigCommerceProductOption
                        {
                            Id = x.Id,
                            ProductId = x.ProductId,                           
                            Sku = x.Sku,
                            Quantity = x.Quantity,
                            Upc = x.Upc,
                            Price = x.Price,
                            SalePrice = x.SalePrice,
                            IsFreeShipping = x.IsFreeShipping,
                            Weight = x.Weight,
                            Width = x.Width,
                            Height = x.Height,
                            Depth = x.Depth,
                            CostPrice = x.CostPrice,
                            ImageUrl = x.ImageUrl,
                            Attributes = x.Attributes
                        }).ToList(),
                        Main_Images = product.Images!.Select(y => new BigCommerceImage
                        {
                            ImageFile = y.ImageFile,
                            IsThumbnail = y.IsThumbnail,
                            SortOrder = y.SortOrder,
                            Description = y.Description,
                            ImageUrl = y.ImageUrl,
                            UrlStandard = y.UrlStandard,
                            DateModified = y.DateModified
                        }).ToList()
                    });
				}

				if (productsWithinPage.Response.Data.Count < RequestMaxLimit)
					break;
			}

			if (includeExtendedInfo)
			{
				await base.FillWeightUnitAsync(products, token, marker);
				await base.FillBrandsAsync(products, token, marker);
			}

			return products;
		}
        #endregion

        #region Update

        /// <summary>
        /// Updates the product options.
        /// </summary>
        /// <param name="productOptions">The product options.</param>
        public void UpdateProductOptions(List<BigCommerceProductOption> productOptions)
        {
            var marker = this.GetMarker();

            foreach (var productOption in productOptions)
            {
                var endpoint = string.Format("/{0}/variants/{1}", productOption.ProductId, productOption.Id);
                var jsonContent = new { inventory_level = productOption.Quantity }.ToJson();

                var limit = ActionPolicies.Submit(marker, endpoint).Get(() =>
                    this._webRequestServices.PutData(BigCommerceCommand.UpdateProductsV3, endpoint, jsonContent, marker));
                this.CreateApiDelay(limit).Wait(); //API requirement
            }
        }

        /// <summary>
        /// Updates the products.
        /// </summary>
        /// <param name="products">The products.</param>
        public void UpdateProducts(List<BigCommerceProduct> products)
        {
            var marker = this.GetMarker();

            foreach (var product in products)
            {
                var endpoint = string.Format("/{0}", product.Id);
                var jsonContent = new { inventory_level = product.Quantity }.ToJson();

                var limit = ActionPolicies.Submit(marker, endpoint).Get(() =>
                    this._webRequestServices.PutData(BigCommerceCommand.UpdateProductsV3, endpoint, jsonContent, marker));
                this.CreateApiDelay(limit).Wait(); //API requirement
            }
        }

        /// <summary>
        /// Updates the product options asynchronously.
        /// </summary>
        /// <param name="productOptions">The product options.</param>
        /// <param name="token">The token.</param>
        /// <returns>A Task.</returns>
        public async Task UpdateProductOptionsAsync( List< BigCommerceProductOption > productOptions, CancellationToken token )
		{
			var marker = this.GetMarker();

			await productOptions.DoInBatchAsync( MaxThreadsCount, async productOption =>
			{
				var endpoint = string.Format("/{0}/variants/{1}", productOption.ProductId, productOption.Id );
				var jsonContent = new { inventory_level = productOption.Quantity }.ToJson();

				var limit = await ActionPolicies.SubmitAsync( marker, endpoint ).Get( async () =>
					await this._webRequestServices.PutDataAsync( BigCommerceCommand.UpdateProductsV3, endpoint, jsonContent, marker ) );
				await this.CreateApiDelay( limit, token ); //API requirement
			} );
		}

        /// <summary>
        /// Updates the products asynchronously.
        /// </summary>
        /// <param name="products">The products.</param>
        /// <param name="token">The token.</param>
        /// <returns>A Task.</returns>
        public async Task UpdateProductsAsync( List< BigCommerceProduct > products, CancellationToken token )
		{
			var marker = this.GetMarker();

			await products.DoInBatchAsync( MaxThreadsCount, async product =>
			{
				var endpoint = string.Format("/{0}", product.Id);
				var jsonContent = new { inventory_level = product.Quantity }.ToJson();

				var limit = await ActionPolicies.SubmitAsync( marker, endpoint ).Get( async () =>
					await this._webRequestServices.PutDataAsync( BigCommerceCommand.UpdateProductsV3, endpoint, jsonContent, marker ) );

				await this.CreateApiDelay( limit, token ); //API requirement
			} );
		}

        #endregion

        #region Misc
        /// <summary>
        /// Converts to compatible with v2 inventory tracking enum.
        /// </summary>
        /// <param name="inventoryTracking">The inventory tracking.</param>
        /// <returns>An InventoryTrackingEnum.</returns>
        private InventoryTrackingEnum ToCompatibleWithV2InventoryTrackingEnum( string inventoryTracking )
		{
			if ( string.IsNullOrWhiteSpace( inventoryTracking ) )
			{
				return InventoryTrackingEnum.none;
			}

			if ( inventoryTracking.Equals( _inventoryTrackingByOption ) )
			{
				return InventoryTrackingEnum.sku;
			}

			return InventoryTrackingEnum.simple;
		}
		#endregion
	}
}
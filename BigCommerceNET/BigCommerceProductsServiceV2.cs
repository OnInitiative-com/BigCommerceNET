﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BigCommerceNET.Misc;
using BigCommerceNET.Models.Command;
using BigCommerceNET.Models.Configuration;
using BigCommerceNET.Models.Product;
using BigCommerceNET.Services;
using Netco.Extensions;
using ServiceStack;

namespace BigCommerceNET
{
    /// <summary>
    /// The big commerce products service v2.
    /// </summary>
    sealed class BigCommerceProductsServiceV2 : BigCommerceBaseProductsService, IBigCommerceProductsService
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="BigCommerceProductsServiceV2"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        public BigCommerceProductsServiceV2( WebRequestServices services ) : base( services )
		{ }

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
        /// <param name="includeExtendInfo">If true, include extend info.</param>
        /// <returns><![CDATA[A List<BigCommerceProduct>.]]></returns>
        public List<BigCommerceProduct> GetProducts(bool includeExtendInfo)
        {
            var products = new List<BigCommerceProduct>();
            var marker = this.GetMarker();

            for (var i = 1; i < int.MaxValue; i++)
            {
                var endpoint = ParamsBuilder.CreateGetNextPageParams(new BigCommerceCommandConfig(i, RequestMaxLimit));
                var productsWithinPage = ActionPolicies.Get(marker, endpoint).Get(() =>
                    base._webRequestServices.GetResponseByRelativeUrl<List<BigCommerceProduct>>(BigCommerceCommand.GetProductsV2, endpoint, marker));
                this.CreateApiDelay(productsWithinPage.Limits).Wait(); //API requirement

                if (productsWithinPage.Response == null)
                    break;

                this.FillProductsSkus(productsWithinPage.Response, marker);
                products.AddRange(productsWithinPage.Response);
                if (productsWithinPage.Response.Count < RequestMaxLimit)
                    break;
            }

            if (includeExtendInfo)
            {
                this.FillWeightUnit(products, marker);
                this.FillBrands(products, marker);
            }

            return products;
        }

        /// <summary>
        /// Gets the products asynchronously.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="includeExtendedInfo">If true, include extended info.</param>
        /// <returns><![CDATA[A Task< List< BigCommerceProduct > >.]]></returns>
        public async Task< List< BigCommerceProduct > > GetProductsAsync( CancellationToken token, bool includeExtendedInfo )
		{
			var products = new List< BigCommerceProduct >();
			var marker = this.GetMarker();

			for( var i = 1; i < int.MaxValue; i++ )
			{
				var endpoint = ParamsBuilder.CreateGetNextPageParams( new BigCommerceCommandConfig( i, RequestMaxLimit ) );
				var productsWithinPage = await ActionPolicies.GetAsync( marker, endpoint ).Get( async () =>
					await base._webRequestServices.GetResponseByRelativeUrlAsync< List< BigCommerceProduct > >( BigCommerceCommand.GetProductsV2, endpoint, marker ) );
				await this.CreateApiDelay( productsWithinPage.Limits, token ); //API requirement

				if( productsWithinPage.Response == null )
					break;

				await this.FillProductsSkusAsync( productsWithinPage.Response, productsWithinPage.Limits.IsUnlimitedCallsCount, token, marker );
				products.AddRange( productsWithinPage.Response );
				if( productsWithinPage.Response.Count < RequestMaxLimit )
					break;
			}

			if( includeExtendedInfo )
			{
				await this.FillWeightUnitAsync( products, token, marker );
				await this.FillBrandsAsync( products, token, marker );
			}

			return products;
		}



        /// <summary>
        /// Fill weight unit asynchronously.
        /// </summary>
        /// <param name="products">The products.</param>
        /// <param name="token">The token.</param>
        /// <param name="marker">The marker.</param>
        /// <returns>A Task.</returns>
        protected override async Task FillWeightUnitAsync( IEnumerable< BigCommerceProduct > products, CancellationToken token, string marker )
		{
			var command = BigCommerceCommand.GetStoreV2;
			var store = await ActionPolicies.GetAsync( marker, command.Command ).Get( async () =>
				await this._webRequestServices.GetResponseByRelativeUrlAsync< BigCommerceStore >( command, string.Empty, marker ) );
			await this.CreateApiDelay( store.Limits, token ); //API requirement

			foreach( var product in products )
			{
				product.WeightUnit = store.Response.WeightUnits;
			}
		}

        /// <summary>
        /// Fill brands asynchronously.
        /// </summary>
        /// <param name="products">The products.</param>
        /// <param name="token">The token.</param>
        /// <param name="marker">The marker.</param>
        /// <returns>A Task.</returns>
        protected override async Task FillBrandsAsync( IEnumerable< BigCommerceProduct > products, CancellationToken token, string marker )
		{
			var brands = new List< BigCommerceBrand >();
			for( var i = 1; i < int.MaxValue; i++ )
			{
				var endpoint = ParamsBuilder.CreateGetNextPageParams( new BigCommerceCommandConfig( i, RequestMaxLimit ) );
				var brandsWithinPage = await ActionPolicies.GetAsync( marker, endpoint ).Get( async () =>
					await this._webRequestServices.GetResponseByRelativeUrlAsync< List< BigCommerceBrand > >( BigCommerceCommand.GetBrandsV2, endpoint, marker ) );
				await this.CreateApiDelay( brandsWithinPage.Limits, token ); //API requirement

				if( brandsWithinPage.Response == null )
					break;

				brands.AddRange( brandsWithinPage.Response );
				if( brandsWithinPage.Response.Count < RequestMaxLimit )
					break;
			}

			this.FillBrandsForProducts( products, brands );
		}
        #endregion

        #region Update

        /// <summary>
        /// Updates the products.
        /// </summary>
        /// <param name="products">The products.</param>
        public void UpdateProducts(List<BigCommerceProduct> products)
        {
            var marker = this.GetMarker();

            foreach (var product in products)
            {
                var endpoint = ParamsBuilder.CreateProductUpdateEndpoint(product.Id);
                var jsonContent = new { inventory_level = product.Quantity }.ToJson();

                var limit = ActionPolicies.Submit(marker, endpoint).Get(() =>
                    this._webRequestServices.PutData(BigCommerceCommand.UpdateProductV2, endpoint, jsonContent, marker));
                this.CreateApiDelay(limit).Wait(); //API requirement
            }
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
				var endpoint = ParamsBuilder.CreateProductUpdateEndpoint( product.Id );
				var jsonContent = new { inventory_level = product.Quantity }.ToJson();

				var limit = await ActionPolicies.SubmitAsync( marker, endpoint ).Get( async () =>
					await this._webRequestServices.PutDataAsync( BigCommerceCommand.UpdateProductV2, endpoint, jsonContent, marker ) );

				await this.CreateApiDelay( limit, token ); //API requirement
			} );
		}

        /// <summary>
        /// Updates the product options.
        /// </summary>
        /// <param name="productOptions">The product options.</param>
        public void UpdateProductOptions(List<BigCommerceProductOption> productOptions)
        {
            var marker = this.GetMarker();

            foreach (var option in productOptions)
            {
                var endpoint = ParamsBuilder.CreateProductOptionUpdateEndpoint(option.ProductId, option.Id);
                var jsonContent = new { inventory_level = option.Quantity }.ToJson();

                var limit = ActionPolicies.Submit(marker, endpoint).Get(() =>
                    this._webRequestServices.PutData(BigCommerceCommand.UpdateProductV2, endpoint, jsonContent, marker));
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

			await productOptions.DoInBatchAsync( MaxThreadsCount, async option =>
			{
				var endpoint = ParamsBuilder.CreateProductOptionUpdateEndpoint( option.ProductId, option.Id );
				var jsonContent = new { inventory_level = option.Quantity }.ToJson();

				var limit = await ActionPolicies.SubmitAsync( marker, endpoint ).Get( async () =>
					await this._webRequestServices.PutDataAsync( BigCommerceCommand.UpdateProductV2, endpoint, jsonContent, marker ) );
				await this.CreateApiDelay( limit, token ); //API requirement
			} );
		}
		#endregion
	}
}

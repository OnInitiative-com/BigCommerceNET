using BigCommerceNET.Misc;
using BigCommerceNET.Models.Command;
using BigCommerceNET.Models.Category;
using BigCommerceNET.Services;
using Netco.ActionPolicyServices;

namespace BigCommerceNET
{
    sealed class BigCommerceCategoriesServiceV3 : BigCommerceBaseCategoriesService, IBigCommerceCategoriesService
	{
		
		public BigCommerceCategoriesServiceV3( WebRequestServices services ) : base( services )
		{
		}

        #region Get		

        public List<BigCommerceCategory> GetCategories()
        {
            var categories = new List<BigCommerceCategory>();
            var marker = this.GetMarker();

            for (var i = 1; i < int.MaxValue; i++)
            {
                var endpoint = "";//mainEndpoint.ConcatParams(ParamsBuilder.CreateGetNextPageParams(new BigCommerceCommandConfig(i, RequestMaxLimit)));
                var categoriesWithinPage = ActionPolicy.Handle<Exception>().Retry(ActionPolicies.RetryCount, (ex, retryAttempt) =>
                {
                    if (PageAdjuster.TryAdjustPageIfResponseTooLarge(new PageInfo(i, this.RequestMaxLimit), this.RequestMinLimit, ex, out var newPageInfo))
                    {
                        i = newPageInfo.Index;
                        this.RequestMaxLimit = newPageInfo.Size;
                    }

                    ActionPolicies.LogRetryAndWait(ex, marker, endpoint, retryAttempt);
                }).Get(() => {
                    return this._webRequestServices?.GetResponseByRelativeUrl<BigCommerceCategoryInfoData>(BigCommerceCommand.GetCategoriesV3, endpoint, marker);
                });

                this.CreateApiDelay(categoriesWithinPage!.Limits).Wait(); //API requirement

                if (categoriesWithinPage.Response == null)
                    break;

                foreach (var category in categoriesWithinPage.Response.Data)
                {
                    var CatURL = category.Category_URL;

                    categories.Add(new BigCommerceCategory
                    {
                        Id = category.Id,
                        Category_URL = new BigCommerceCategoryURL()
                        {
                            Url = CatURL!.Url
                        },
                        Category_Name = category.Name!,
                        IsVisible = category.IsVisible,
						Parent_Id = category.Parent_Id,
						SortOrder = category.SortOrder
                    });
                }

                if (categoriesWithinPage.Response.Data.Length < RequestMaxLimit)
                    break;
            }


            return categories;
        }

        public async Task<List<BigCommerceCategory>> GetCategoriesAsync(CancellationToken token)
		{			
			var categories = new List<BigCommerceCategory>();
			var marker = this.GetMarker();

			for (var i = 1; i < int.MaxValue; i++)
			{
				var endpoint = "";//mainEndpoint.ConcatParams(ParamsBuilder.CreateGetNextPageParams(new BigCommerceCommandConfig(i, RequestMaxLimit)));
				var categoriesWithinPage = await ActionPolicyAsync.Handle<Exception>().RetryAsync(ActionPolicies.RetryCount, (ex, retryAttempt) =>
				{
					if (PageAdjuster.TryAdjustPageIfResponseTooLarge(new PageInfo(i, this.RequestMaxLimit), this.RequestMinLimit, ex, out var newPageInfo))
					{
						i = newPageInfo.Index;
						this.RequestMaxLimit = newPageInfo.Size;
					}

					return ActionPolicies.LogRetryAndWaitAsync(ex, marker, endpoint, retryAttempt);
				}).Get(() => {
					return this._webRequestServices!.GetResponseByRelativeUrlAsync<BigCommerceCategoryInfoData>(BigCommerceCommand.GetCategoriesV3, endpoint, marker);
				});

				await this.CreateApiDelay(categoriesWithinPage.Limits, token ); //API requirement

				if (categoriesWithinPage.Response == null)
					break;

				foreach (var category in categoriesWithinPage.Response.Data)
				{
					var productCatURL = category.Category_URL;

					categories.Add(new BigCommerceCategory
					{
						Id = category.Id,
						Category_URL = new BigCommerceCategoryURL()
						{
							Url = productCatURL!.Url
						},
						Category_Name = category.Name!,
                        IsVisible = category.IsVisible,
                        Parent_Id = category.Parent_Id,
                        SortOrder = category.SortOrder
                    });
				}

				if (categoriesWithinPage.Response.Data.Length < RequestMaxLimit)
					break;
			}

			return categories;
		}
		
		#endregion

		#region Update		

		#endregion

		#region Misc
		
		#endregion
	}
}
using System;
using System.Collections.Generic;

namespace BigCommerceNET.Models.Configuration
{
    /// <summary>
    /// The big commerce config.
    /// </summary>
    public sealed class BigCommerceConfig
	{
        /// <summary>
        /// Gets the native host.
        /// </summary>
        public string? NativeHost{ get; private set; }
        /// <summary>
        /// Gets the custom host.
        /// </summary>
        public string? CustomHost{ get; private set; }
        /// <summary>
        /// Gets the shop name.
        /// </summary>
        public string? ShopName{ get; private set; }
        /// <summary>
        /// Gets the user name.
        /// </summary>
        public string? UserName{ get; private set; }
        /// <summary>
        /// Gets the api key.
        /// </summary>
        public string? ApiKey{ get; private set; }

        /// <summary>
        /// Gets the client id.
        /// </summary>
        public string? ClientId{ get; private set; }
        /// <summary>
        /// Gets the client secret.
        /// </summary>
        public string? ClientSecret{ get; private set; }
        /// <summary>
        /// Gets the token.
        /// </summary>
        public string? Token{ get; private set; }

        /// <summary>
        /// Gets or Sets the tenant id.
        /// </summary>
        public long? TenantId { get; set; }
        /// <summary>
        /// Gets or Sets the channel account id.
        /// </summary>
        public long? ChannelAccountId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BigCommerceConfig"/> class.
        /// </summary>
        /// <param name="shopName">The shop name.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="apiKey">The api key.</param>
        public BigCommerceConfig( string shopName, string userName, string apiKey )
		{
			if (!String.IsNullOrEmpty(shopName) && !String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(apiKey))
			{

				var domainAndShopName = this.GetDomainAndShopName(shopName);

				this.NativeHost = string.Format("https://{0}.mybigcommerce{1}", domainAndShopName.Item2, domainAndShopName.Item1);
				this.CustomHost = string.Format("https://www.{0}{1}", domainAndShopName.Item2, domainAndShopName.Item1);
				this.UserName = userName;
				this.ApiKey = apiKey;

				this.ShopName = shopName;

				this.ClientId = string.Empty;
				this.ClientSecret = string.Empty;
				this.Token = string.Empty;
			}

		}

        /// <summary>
        /// Initializes a new instance of the <see cref="BigCommerceConfig"/> class.
        /// </summary>
        /// <param name="shopName">The shop name.</param>
        /// <param name="clientId">The client id.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="token">The token.</param>
        public BigCommerceConfig( string shopName, string clientId, string clientSecret, string token )
		{			

			if (!String.IsNullOrEmpty(shopName) && !String.IsNullOrEmpty(clientId) && !String.IsNullOrEmpty(clientSecret)
                && !String.IsNullOrEmpty(token))
			{

				this.NativeHost = string.Format("https://api.bigcommerce.com/stores/{0}", shopName);
				this.CustomHost = string.Empty;
				this.UserName = string.Empty;
				this.ApiKey = string.Empty;

				this.ShopName = shopName;

				this.ClientId = clientId;
				this.ClientSecret = clientSecret;
				this.Token = token;
			}
		}

        /// <summary>
        /// Gets the API version.
        /// </summary>
        /// <returns>An APIVersion.</returns>
        public APIVersion GetAPIVersion()
		{
			return string.IsNullOrEmpty( this.ClientId ) ? APIVersion.V2 : APIVersion.V3;
		}

        /// <summary>
        /// Gets the domain and shop name.
        /// </summary>
        /// <param name="shopName">The shop name.</param>
        /// <returns><![CDATA[A Tuple< string, string >.]]></returns>
        private Tuple< string, string > GetDomainAndShopName( string shopName )
		{
			var lastIndexPoint = shopName.LastIndexOf( '.' );
			if( lastIndexPoint == -1 )
				return new Tuple< string, string >( ".com", shopName );

			var domain = shopName.Substring( lastIndexPoint );
			if( this._existDomains.Contains( domain ) )
				return new Tuple< string, string >( domain, shopName.Substring( 0, lastIndexPoint ) );

			return new Tuple< string, string >( ".com", shopName );
		}

        /// <summary>
        /// exist domains.
        /// </summary>
        private readonly List< string > _existDomains = new List< string >
		{
			".aero",
			".asia",
			".biz",
			".cat",
			".com",
			".coop",
			".edu",
			".gov",
			".info",
			".int",
			".jobs",
			".mil",
			".mobi",
			".museum",
			".name",
			".net",
			".org",
			".post",
			".pro",
			".properties",
			".tel",
			".travel",
			".ru",
			".uk"
		};
	}

    /// <summary>
    /// The API version.
    /// </summary>
    public enum APIVersion
	{
		V2 = 0,
		V3 = 1
	}
}
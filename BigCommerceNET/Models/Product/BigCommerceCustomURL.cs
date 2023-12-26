using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Product
{
    /// <summary>
    /// The big commerce custom URL.
    /// </summary>
    [ DataContract ]
	public class BigCommerceCustomURL
	{
        /// <summary>
        /// Gets or Sets a value indicating whether url standard.
        /// </summary>
        [ DataMember( Name = "is_customized") ]
		public bool UrlStandard{ get; set; }

        /// <summary>
        /// Gets or Sets the product URL.
        /// </summary>
        [ DataMember( Name = "url") ]
		public string? ProductURL { get; set; }
	}
}
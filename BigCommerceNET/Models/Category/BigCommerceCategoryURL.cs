using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Category
{
    /// <summary>
    /// The big commerce category URL.
    /// </summary>
    [ DataContract ]
	public class BigCommerceCategoryURL
	{
        /// <summary>
        /// Gets or Sets a value indicating whether is customized.
        /// </summary>
        [ DataMember( Name = "is_customized") ]
		public bool isCustomized{ get; set; }

        /// <summary>
        /// Gets or Sets the url.
        /// </summary>
        [ DataMember( Name = "url") ]
		public string? Url { get; set; }
	}
}
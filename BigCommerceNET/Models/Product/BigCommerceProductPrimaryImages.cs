using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Product
{
    /// <summary>
    /// The big commerce product primary images.
    /// </summary>
    [ DataContract ]
	public class BigCommerceProductPrimaryImages
	{
        /// <summary>
        /// Gets or Sets the standard url.
        /// </summary>
        [ DataMember( Name = "standard_url" ) ]
		public string? StandardUrl{ get; set; }
	}
}
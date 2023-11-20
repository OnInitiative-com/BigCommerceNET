using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Product
{
	[ DataContract ]
	public class BigCommerceProductPrimaryImages
	{
		[ DataMember( Name = "standard_url" ) ]
		public string? StandardUrl{ get; set; }
	}
}
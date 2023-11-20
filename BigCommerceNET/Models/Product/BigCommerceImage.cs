using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Product
{
	[ DataContract ]
	public class BigCommerceImage
	{
		[ DataMember( Name = "url_standard" ) ]
		public string? UrlStandard{ get; set; }

		[ DataMember( Name = "is_thumbnail" ) ]
		public bool IsThumbnail { get; set; }
	}
}
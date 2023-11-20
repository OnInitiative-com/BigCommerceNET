using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Product
{
	[ DataContract ]
	public class BigCommerceBrand: BigCommerceObjectBase
	{
		[ DataMember( Name = "name" ) ]
		public string? Name{ get; set; }

		public BigCommerceBrand()
		{
		}
	}
}
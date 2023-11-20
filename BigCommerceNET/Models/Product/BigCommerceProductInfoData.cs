using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Product
{
	[ DataContract ]
	public class BigCommerceProductInfoData: BigCommerceProductBase
	{
		[ DataMember( Name = "data" ) ]
		public List< BigCommerceProductInfo > Data{ get; set; }

		public BigCommerceProductInfoData()
		{
			this.Data = new List< BigCommerceProductInfo >();
		}
	}
}
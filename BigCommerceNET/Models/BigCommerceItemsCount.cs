using System.Runtime.Serialization;

namespace BigCommerceNET.Models
{
	[ DataContract ]
	sealed class BigCommerceItemsCount
	{
		[ DataMember( Name = "count" ) ]
		public int Count { get; set; }
	}
}
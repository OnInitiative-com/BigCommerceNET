using System.Runtime.Serialization;

namespace BigCommerceNET.Models
{
	[ DataContract ]
	public abstract class BigCommerceObjectBase
	{
		[ DataMember( Name = "id" ) ]
		public long Id{ get; set; }
	}
}
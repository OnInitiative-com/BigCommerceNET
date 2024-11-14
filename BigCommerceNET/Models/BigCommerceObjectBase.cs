using System.Runtime.Serialization;

namespace BigCommerceNET.Models
{
    /// <summary>
    /// The big commerce object base.
    /// </summary>
    [ DataContract ]
	public abstract class BigCommerceObjectBase
	{
        /// <summary>
        /// Gets or Sets the id.
        /// </summary>
        [ DataMember( Name = "id" ) ]
		public long Id{ get; set; }
	}
}
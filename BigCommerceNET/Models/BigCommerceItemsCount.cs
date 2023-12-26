using System.Runtime.Serialization;

namespace BigCommerceNET.Models
{
    /// <summary>
    /// The big commerce items count.
    /// </summary>
    [ DataContract ]
	sealed class BigCommerceItemsCount
	{
        /// <summary>
        /// Gets or Sets the count.
        /// </summary>
        [ DataMember( Name = "count" ) ]
		public int Count { get; set; }
	}
}
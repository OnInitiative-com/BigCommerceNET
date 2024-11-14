using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Product
{
    /// <summary>
    /// The big commerce product base.
    /// </summary>
    [ DataContract ]
	public class BigCommerceProductBase: BigCommerceObjectBase
	{
        /// <summary>
        /// Gets or Sets the quantity.
        /// </summary>
        [ DataMember( Name = "inventory_level" ) ]
		public string? Quantity{ get; set; }

        /// <summary>
        /// Gets or Sets the sku.
        /// </summary>
        [ DataMember( Name = "sku" ) ]
		public string? Sku{ get; set; }
	}

    /// <summary>
    /// The inventory tracking enum.
    /// </summary>
    public enum InventoryTrackingEnum
	{
		none,
		simple,
		sku
	}
}
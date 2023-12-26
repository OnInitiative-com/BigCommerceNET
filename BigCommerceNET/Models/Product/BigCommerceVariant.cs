using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Product
{
    /// <summary>
    /// The big commerce variant.
    /// </summary>
    [ DataContract ]
	public class BigCommerceVariant
	{
        /// <summary>
        /// Gets or Sets the id.
        /// </summary>
        [ DataMember( Name = "id" ) ]
		public long Id{ get; set; }

        /// <summary>
        /// Gets or Sets the product id.
        /// </summary>
        [ DataMember( Name = "product_id" ) ]
		public long ProductId{ get; set; }

        /// <summary>
        /// Gets or Sets the sku.
        /// </summary>
        [ DataMember( Name = "sku" ) ]
		public string? Sku{ get; set; }

        /// <summary>
        /// Gets or Sets the upc.
        /// </summary>
        [ DataMember( Name = "upc" ) ]
		public string? Upc{ get; set; }

        /// <summary>
        /// Gets or Sets the price.
        /// </summary>
        [ DataMember( Name = "price" ) ]
		public decimal? Price{ get; set; }

        /// <summary>
        /// Gets or Sets the sale price.
        /// </summary>
        [DataMember(Name = "sale_price")]
        public decimal? SalePrice { get; set; }

        /// <summary>
        /// Gets or Sets the weight.
        /// </summary>
        [DataMember(Name = "weight")]
        public decimal? Weight { get; set; }

        /// <summary>
        /// Gets or Sets the width.
        /// </summary>
        [DataMember(Name = "width")]
        public decimal? Width { get; set; }

        /// <summary>
        /// Gets or Sets the height.
        /// </summary>
        [DataMember(Name = "height ")]
        public decimal? Height { get; set; }

        /// <summary>
        /// Gets or Sets the depth.
        /// </summary>
        [DataMember(Name = "depth")]
        public decimal? Depth { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether free is shipping.
        /// </summary>
        [DataMember(Name = "is_free_shipping")]
        public bool IsFreeShipping { get; set; }

        /// <summary>
        /// Gets or Sets the cost price.
        /// </summary>
        [ DataMember( Name = "cost_price" ) ]
		public decimal? CostPrice{ get; set; }

        /// <summary>
        /// Gets or Sets the quantity.
        /// </summary>
        [ DataMember( Name = "inventory_level" ) ]
		public string? Quantity{ get; set; }

        /// <summary>
        /// Gets or Sets the image url.
        /// </summary>
        [DataMember(Name = "image_url")]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Gets or Sets the attributes.
        /// </summary>
        [DataMember(Name = "option_values")]
        public List<OptionValue>? Attributes { get; set; }


    }
}
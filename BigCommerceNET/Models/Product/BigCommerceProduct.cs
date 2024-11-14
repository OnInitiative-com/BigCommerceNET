using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Product
{
    /// <summary>
    /// The big commerce product.
    /// </summary>
    [ DataContract ]
	public class BigCommerceProduct: BigCommerceProductBase
	{
        /// <summary>
        /// Gets or Sets the inventory tracking.
        /// </summary>
        [ DataMember( Name = "inventory_tracking" ) ]
		public InventoryTrackingEnum InventoryTracking{ get; set; }

        /// <summary>
        /// Gets or Sets the product options reference.
        /// </summary>
        [ DataMember( Name = "skus" ) ]
		public BigCommerceReferenceObject? ProductOptionsReference{ get; set; }

        /// <summary>
        /// Gets or Sets the product options.
        /// </summary>
        public List<BigCommerceProductOption>? ProductOptions{ get; set; }

        /// <summary>
        /// Gets or Sets the main images.
        /// </summary>
        [DataMember(Name = "main_images")]
		public List<BigCommerceImage> Main_Images { get; set; }

        /// <summary>
        /// Gets or Sets the upc.
        /// </summary>
        [ DataMember( Name = "upc" ) ]
		public string? Upc{ get; set; }

        /// <summary>
        /// Gets or Sets the name.
        /// </summary>
        [ DataMember( Name = "name" ) ]
		public string? Name{ get; set; }

        /// <summary>
        /// Gets or Sets the availability.
        /// </summary>
        [DataMember(Name = "availability")]
		public string? Availability { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether free is shipping.
        /// </summary>
        [DataMember(Name = "is_free_shipping")]
        public bool IsFreeShipping { get; set; }

        /// <summary>
        /// Gets or Sets the fixed cost shipping price.
        /// </summary>
        [DataMember(Name = "fixed_cost_shipping_price")]
        public decimal FixedCostShippingPrice { get; set; }

        /// <summary>
        /// Gets or Sets the condition.
        /// </summary>
        [DataMember(Name = "condition")]
		public string? Condition { get; set; }

        /// <summary>
        /// Gets or Sets the description.
        /// </summary>
        [ DataMember( Name = "description" ) ]
		public string? Description{ get; set; }

        /// <summary>
        /// Gets or Sets the price.
        /// </summary>
        [ DataMember( Name = "price" ) ]
		public decimal? Price{ get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether product is visible.
        /// </summary>
        [DataMember(Name = "is_visible")]
		public bool IsProductVisible { get; set; }

        /// <summary>
        /// Gets or Sets the product type.
        /// </summary>
        [DataMember(Name = "type")]
		public string? ProductType { get; set; }

        /// <summary>
        /// Gets or Sets the sale price.
        /// </summary>
        [ DataMember( Name = "sale_price" ) ]
		public decimal? SalePrice{ get; set; }

        /// <summary>
        /// Gets or Sets the retail price.
        /// </summary>
        [ DataMember( Name = "retail_price" ) ]
		public decimal? RetailPrice{ get; set; }

        /// <summary>
        /// Gets or Sets the cost price.
        /// </summary>
        [ DataMember( Name = "cost_price" ) ]
		public decimal? CostPrice{ get; set; }

        /// <summary>
        /// Gets or Sets the weight.
        /// </summary>
        [ DataMember( Name = "weight" ) ]
		public decimal? Weight{ get; set; }

        /// <summary>
        /// Gets or Sets the weight unit.
        /// </summary>
        public string? WeightUnit{ get; set; }

        /// <summary>
        /// Gets or Sets the brand id.
        /// </summary>
        [ DataMember( Name = "brand_id" ) ]
		public long? BrandId{ get; set; }

        /// <summary>
        /// Gets or Sets the brand name.
        /// </summary>
        public string? BrandName{ get; set; }

        /// <summary>
        /// Gets or Sets the thumbnail image URL.
        /// </summary>
        [ DataMember( Name = "thumbnail_image") ]
		public BigCommerceProductPrimaryImages? ThumbnailImageURL { get; set; }

        /// <summary>
        /// Gets or Sets the product URL.
        /// </summary>
        [DataMember(Name = "custom_url")]
		public string? Product_URL { get; set; }

        /// <summary>
        /// Gets or Sets the categories.
        /// </summary>
        [DataMember(Name = "categories")]
		public int[]? Categories { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BigCommerceProduct"/> class.
        /// </summary>
        public BigCommerceProduct()
		{
			this.ProductOptions = new List<BigCommerceProductOption>();
			this.Main_Images = new List<BigCommerceImage>();
		}
	}
}
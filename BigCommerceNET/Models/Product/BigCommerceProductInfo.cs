using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Product
{
    /// <summary>
    /// The big commerce product info.
    /// </summary>
    [ DataContract ]
	public class BigCommerceProductInfo
	{
        /// <summary>
        /// Gets or Sets the id.
        /// </summary>
        [ DataMember( Name = "id" ) ]
		public int? Id{ get; set; }

        /// <summary>
        /// Gets or Sets the name.
        /// </summary>
        [ DataMember( Name = "name" ) ]
		public string? Name{ get; set; }

        /// <summary>
        /// Gets or Sets the availability.
        /// </summary>
        [DataMember(Name = "availability")]
		public string? Availability { get; set; }  //Possible values: available, disabled, preorder

        /// <summary>
        /// Gets or Sets the condition.
        /// </summary>
        [DataMember(Name = "condition")]
		public string? Condition { get; set; } //Possible values: New, Used, Refurbished

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
        /// Gets or Sets the description.
        /// </summary>
        [ DataMember( Name = "description" ) ]
		public string? Description{ get; set; }

        /// <summary>
        /// Gets or Sets the price.
        /// </summary>
        [DataMember(Name = "price")]
		public decimal? Price { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether is visible.
        /// </summary>
        [ DataMember( Name = "is_visible") ]
		public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or Sets the type.
        /// </summary>
        [DataMember(Name = "type")]
		public string? Type { get; set; }  //One of: "physical" - a physical stock unit, "digital" - a digital download.

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
        /// Gets or Sets the brand id.
        /// </summary>
        [ DataMember( Name = "brand_id" ) ]
		public int? BrandId{ get; set; }

        /// <summary>
        /// Gets or Sets the images.
        /// </summary>
        [ DataMember( Name = "images" ) ]
		public List<BigCommerceImage>? Images{ get; set; }

        /// <summary>
        /// Gets or Sets the categories.
        /// </summary>
        [DataMember(Name = "categories")]
		public int[]? Categories { get; set; }

        /// <summary>
        /// Gets or Sets the product URL.
        /// </summary>
        [DataMember(Name = "custom_url")]
		public BigCommerceCustomURL? Product_URL { get; set; }

        /// <summary>
        /// Gets or Sets the variants.
        /// </summary>
        [ DataMember( Name = "variants" ) ]
		public List<BigCommerceVariant>? Variants{ get; set; }

        /// <summary>
        /// Gets or Sets the inventory tracking.
        /// </summary>
        [ DataMember( Name = "inventory_tracking" ) ]
		public string? InventoryTracking{ get; set; }

        /// <summary>
        /// Gets or Sets the quantity.
        /// </summary>
        [ DataMember( Name = "inventory_level" ) ]
		public string? Quantity{ get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BigCommerceProductInfo"/> class.
        /// </summary>
        public BigCommerceProductInfo()
		{
			this.Images = new List< BigCommerceImage >();
			this.Variants = new List< BigCommerceVariant >();
		}
	}
}
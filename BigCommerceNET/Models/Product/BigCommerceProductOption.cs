using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Product
{
	[ DataContract ]
	public class BigCommerceProductOption: BigCommerceProductBase
	{
		[ DataMember( Name = "product_id" ) ]
		public long ProductId{ get; set; }        

        [ DataMember( Name = "upc" ) ]
		public string? Upc{ get; set; }

		[ DataMember( Name = "price" ) ]
		public decimal? Price{ get; set; }

        [DataMember(Name = "sale_price")]
        public decimal? SalePrice { get; set; }

        [DataMember(Name = "is_free_shipping")]
        public bool IsFreeShipping { get; set; }

        [DataMember(Name = "fixed_cost_shipping_price")]
        public decimal FixedCostShippingPrice { get; set; }

        [DataMember(Name = "condition")]
		public string? Condition { get; set; } 

        [DataMember(Name = "weight")]
        public decimal? Weight { get; set; }

        [DataMember(Name = "width")]
        public decimal? Width { get; set; }

        [DataMember(Name = "height ")]
        public decimal? Height { get; set; }

        [DataMember(Name = "depth")]
        public decimal? Depth { get; set; }

        [ DataMember( Name = "adjusted_price" ) ]
		public decimal? AdjustedPrice{ get; set; }

		[ DataMember( Name = "cost_price" ) ]
		public decimal? CostPrice{ get; set; }	

		[ DataMember( Name = "adjusted_weight" ) ]
		public decimal? AdjustedWeight{ get; set; }        

        [DataMember(Name = "image_url")]
        public string? ImageUrl { get; set; }

        [DataMember(Name = "option_values")]
        public List<OptionValue>? Attributes { get; set; }

    }
}
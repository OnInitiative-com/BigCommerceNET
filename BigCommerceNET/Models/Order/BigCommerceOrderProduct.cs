using System.Globalization;
using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Order
{
    /// <summary>
    /// The big commerce order product.
    /// </summary>
    [ DataContract ]
	public class BigCommerceOrderProduct : BigCommerceObjectBase
	{
        /// <summary>
        /// Gets or Sets the name.
        /// </summary>
        [ DataMember( Name = "name" ) ]
		public string? Name { get; set; }

        /// <summary>
        /// Gets or Sets the sku.
        /// </summary>
        [ DataMember( Name = "sku" ) ]
		public string? Sku { get; set; }

        /// <summary>
        /// Gets or Sets the quantity.
        /// </summary>
        [ DataMember( Name = "quantity" ) ]
		public int Quantity { get; set; }

        /// <summary>
        /// Gets or Sets the price inc tax.
        /// </summary>
        [ DataMember( Name = "price_inc_tax" ) ]
		public string? PriceIncTax{ get; set; }

        /// <summary>
        /// Gets or Sets the price excl tax.
        /// </summary>
        [ DataMember( Name = "price_ex_tax" ) ]
		public string? PriceExclTax{ get; set; }

        /// <summary>
        /// Gets or Sets the base price.
        /// </summary>
        [ DataMember( Name = "base_price" ) ]
		public string? BasePrice{ get; set; }

        /// <summary>
        /// Gets the tax.
        /// </summary>
        public decimal Tax
		{
			get
			{
				decimal priceIncludingTax;
				decimal.TryParse( this.PriceIncTax, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out priceIncludingTax );

				decimal priceExcludingTax;
				decimal.TryParse( this.PriceExclTax, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out priceExcludingTax );

				return  priceIncludingTax - priceExcludingTax;
			}
		}
	}
}
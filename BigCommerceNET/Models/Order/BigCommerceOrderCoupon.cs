using System.Globalization;
using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Order
{
    /// <summary>
    /// The big commerce order coupon.
    /// </summary>
    [ DataContract ]
	public class BigCommerceOrderCoupon : BigCommerceObjectBase
	{
        /// <summary>
        /// Gets or Sets the code.
        /// </summary>
        [ DataMember( Name = "code" ) ]
		public string? Code { get; set; }

        /// <summary>
        /// Gets or Sets the type.
        /// </summary>
        [ DataMember( Name = "type" ) ]
		public string? Type { get; set; }

        /// <summary>
        /// Gets or Sets the discount value.
        /// </summary>
        [ DataMember( Name = "discount" ) ]
		public string? DiscountValue { get; set; }
        /// <summary>
        /// Gets the discount.
        /// </summary>
        public decimal Discount
		{
			get
			{	
				decimal discountAmount;
				decimal.TryParse( this.DiscountValue, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out discountAmount );
				return discountAmount;
			}
		}
	}
}

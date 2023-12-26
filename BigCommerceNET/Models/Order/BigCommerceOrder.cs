using System.Globalization;
using System.Runtime.Serialization;
using BigCommerceNET.Models.Address;

namespace BigCommerceNET.Models.Order
{
    /// <summary>
    /// The big commerce order.
    /// </summary>
    [ DataContract ]
	public class BigCommerceOrder: BigCommerceObjectBase
	{
        /// <summary>
        /// Gets or Sets the status id.
        /// </summary>
        [ DataMember( Name = "status_id" ) ]
		public int StatusId{ get; set; }

        /// <summary>
        /// Gets or Sets the date created.
        /// </summary>
        [ DataMember( Name = "date_created" ) ]
		public DateTime? DateCreated{ get; set; }

        /// <summary>
        /// Gets or Sets the date shipped.
        /// </summary>
        [ DataMember( Name = "date_shipped" ) ]
		public DateTime? DateShipped{ get; set; }

        /// <summary>
        /// Gets or Sets the products reference.
        /// </summary>
        [ DataMember( Name = "products" ) ]
		public BigCommerceReferenceObject? ProductsReference{ get; set; }

        /// <summary>
        /// Gets or Sets the shipping addresses reference.
        /// </summary>
        [ DataMember( Name = "shipping_addresses" ) ]
		public BigCommerceReferenceObject? ShippingAddressesReference{ get; set; }

        /// <summary>
        /// Gets or Sets the billing address.
        /// </summary>
        [ DataMember( Name = "billing_address" ) ]
		public BigCommerceBillingAddress? BillingAddress{ get; set; }

        /// <summary>
        /// Gets or Sets the customer message.
        /// </summary>
        [ DataMember( Name = "customer_message" ) ]
		public string? CustomerMessage{ get; set; }

        /// <summary>
        /// Gets or Sets the staff notes.
        /// </summary>
        [ DataMember( Name = "staff_notes" ) ]
		public string? StaffNotes{ get; set; }

        /// <summary>
        /// Gets or Sets the total.
        /// </summary>
        [ DataMember( Name = "total_inc_tax" ) ]
		public string? Total{ get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether is deleted.
        /// </summary>
        [ DataMember( Name = "is_deleted" ) ]
		public bool IsDeleted{ get; set; }

        /// <summary>
        /// Gets or Sets the shipping cost ex tax.
        /// </summary>
        [ DataMember( Name = "shipping_cost_ex_tax" ) ]
		public string? ShippingCostExTax{ get; set; }

        /// <summary>
        /// Gets or Sets the handling cost ex tax.
        /// </summary>
        [ DataMember( Name = "handling_cost_ex_tax" ) ]
		public string? HandlingCostExTax{ get; set; }

        /// <summary>
        /// Gets or Sets the wrapping cost ex tax.
        /// </summary>
        [ DataMember( Name = "wrapping_cost_ex_tax" ) ]
		public string? WrappingCostExTax{ get; set; }

        /// <summary>
        /// The products.
        /// </summary>
        private List< BigCommerceOrderProduct > _products;
        /// <summary>
        /// The shipping addresses.
        /// </summary>
        private List< BigCommerceShippingAddress > _shippingAddresses;

        /// <summary>
        /// Gets or Sets the discount amount value.
        /// </summary>
        [ DataMember( Name = "discount_amount" ) ]
		public string? DiscountAmountValue{ get; set; }

        /// <summary>
        /// Gets or Sets the total tax value.
        /// </summary>
        [ DataMember( Name = "total_tax" ) ]
		public string? TotalTaxValue{ get; set; }

        /// <summary>
        /// Gets or Sets the currency code.
        /// </summary>
        [ DataMember( Name = "currency_code" ) ]
		public string? CurrencyCode{ get; set; }

        /// <summary>
        /// Gets or Sets the coupons reference.
        /// </summary>
        [ DataMember( Name = "coupons" ) ]
		public BigCommerceReferenceObject? CouponsReference{ get; set; }

        /// <summary>
        /// The coupons.
        /// </summary>
        private List<BigCommerceOrderCoupon>? _coupons;
        /// <summary>
        /// Gets or Sets the coupons.
        /// </summary>
        public List<BigCommerceOrderCoupon>? Coupons
		{
			get { return this._coupons; }
			set
			{
				if( value != null )
					this._coupons = value;
			}
		}

        /// <summary>
        /// Gets or Sets the products.
        /// </summary>
        public List< BigCommerceOrderProduct > Products
		{
			get { return this._products; }
			set
			{
				if( value != null )
					this._products = value;
			}
		}

        /// <summary>
        /// Gets or Sets the shipping addresses.
        /// </summary>
        public List< BigCommerceShippingAddress > ShippingAddresses
		{
			get { return this._shippingAddresses; }
			set
			{
				if( value != null )
					this._shippingAddresses = value;
			}
		}

        /// <summary>
        /// Gets a value indicating whether is shipped.
        /// </summary>
        public bool IsShipped
		{
			get { return this.DateShipped != DateTime.MinValue; }
		}

        /// <summary>
        /// Gets the order status.
        /// </summary>
        public BigCommerceOrderStatusEnum OrderStatus
		{
			get { return ( BigCommerceOrderStatusEnum )this.StatusId; }
		}

        /// <summary>
        /// Gets the shipping charge.
        /// </summary>
        public decimal ShippingCharge
		{
			get
			{
				decimal baseShippingCost;
				decimal.TryParse( this.ShippingCostExTax, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out baseShippingCost );

				decimal baseHandlingCost;
				decimal.TryParse( this.HandlingCostExTax, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out baseHandlingCost );

				decimal baseWrappingCost;
				decimal.TryParse( this.WrappingCostExTax, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out baseWrappingCost );
				return baseShippingCost + baseHandlingCost + baseWrappingCost;
			}
		}

        /// <summary>
        /// Gets the discount amount.
        /// </summary>
        public decimal DiscountAmount
		{
			get
			{	
				decimal discountAmount;
				decimal.TryParse( this.DiscountAmountValue, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out discountAmount );
				return discountAmount;
			}
		}

        /// <summary>
        /// Gets the total tax.
        /// </summary>
        public decimal TotalTax
		{
			get
			{	
				decimal totalTax;
				decimal.TryParse( this.TotalTaxValue, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out totalTax );
				return totalTax;
			}
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="BigCommerceOrder"/> class.
        /// </summary>
        public BigCommerceOrder()
		{
			this._products = new List< BigCommerceOrderProduct >();
			this._coupons = new List< BigCommerceOrderCoupon >();
			this._shippingAddresses = new List< BigCommerceShippingAddress >();
		}
	}

    /// <summary>
    /// The big commerce order status enum.
    /// </summary>
    public enum BigCommerceOrderStatusEnum
	{
		Incomplete,
		Pending,
		Shipped,
		PartiallyShipped,
		Refunded,
		Canceled,
		Declined,
		AwaitingPayment,
		AwaitingPickup,
		AwaitingShipment,
		Completed,
		AwaitingFulfillment,
		ManualVerificationRequired,
		Disputed,
		PartiallyRefunded
	}
}
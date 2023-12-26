using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Address
{
    /// <summary>
    /// The big commerce shipping address.
    /// </summary>
    [ DataContract ]
	public class BigCommerceShippingAddress
	{
        /// <summary>
        /// Gets or Sets the street1.
        /// </summary>
        [ DataMember( Name = "street_1" ) ]
		public string? Street1 { get; set; }

        /// <summary>
        /// Gets or Sets the street2.
        /// </summary>
        [ DataMember( Name = "street_2" ) ]
		public string? Street2 { get; set; }

        /// <summary>
        /// Gets or Sets the city.
        /// </summary>
        [ DataMember( Name = "city" ) ]
		public string? City { get; set; }

        /// <summary>
        /// Gets or Sets the zip.
        /// </summary>
        [ DataMember( Name = "zip" ) ]
		public string? Zip { get; set; }

        /// <summary>
        /// Gets or Sets the state.
        /// </summary>
        [ DataMember( Name = "state" ) ]
		public string? State { get; set; }

        /// <summary>
        /// Gets or Sets the country.
        /// </summary>
        [ DataMember( Name = "country" ) ]
		public string? Country { get; set; }

        /// <summary>
        /// Gets or Sets the country iso2.
        /// </summary>
        [ DataMember( Name = "country_iso2" ) ]
		public string? CountryIso2 { get; set; }

        /// <summary>
        /// Gets or Sets the shipping method.
        /// </summary>
        [ DataMember( Name = "shipping_method" ) ]
		public string? ShippingMethod{ get; set; }

        /// <summary>
        /// Gets or Sets the first name.
        /// </summary>
        [ DataMember( Name = "first_name" ) ]
		public string? FirstName{ get; set; }

        /// <summary>
        /// Gets or Sets the last name.
        /// </summary>
        [ DataMember( Name = "last_name" ) ]
		public string? LastName{ get; set; }

        /// <summary>
        /// Gets or Sets the company.
        /// </summary>
        [ DataMember( Name = "company" ) ]
		public string? Company{ get; set; }

        /// <summary>
        /// Gets or Sets the phone.
        /// </summary>
        [ DataMember( Name = "phone" ) ]
		public string? Phone{ get; set; }

        /// <summary>
        /// Gets or Sets the email.
        /// </summary>
        [ DataMember( Name = "email" ) ]
		public string? Email{ get; set; }
	}
}
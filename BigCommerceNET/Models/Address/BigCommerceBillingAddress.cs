using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Address
{
    /// <summary>
    /// The big commerce billing address.
    /// </summary>
    [ DataContract ]
	public class BigCommerceBillingAddress
	{
        /// <summary>
        /// Gets or Sets the first name.
        /// </summary>
        [ DataMember( Name = "first_name" ) ]
		public string? FirstName { get; set; }

        /// <summary>
        /// Gets or Sets the last name.
        /// </summary>
        [ DataMember( Name = "last_name" ) ]
		public string? LastName { get; set; }

        /// <summary>
        /// Gets or Sets the company.
        /// </summary>
        [ DataMember( Name = "company" ) ]
		public string? Company { get; set; }

        /// <summary>
        /// Gets or Sets the phone.
        /// </summary>
        [ DataMember( Name = "phone" ) ]
		public string? Phone { get; set; }

        /// <summary>
        /// Gets or Sets the email.
        /// </summary>
        [ DataMember( Name = "email" ) ]
		public string? Email { get; set; }
	}
}
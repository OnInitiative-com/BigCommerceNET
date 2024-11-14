using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Product
{
    /// <summary>
    /// The big commerce store.
    /// </summary>
    [DataContract]
	public class BigCommerceStore
	{

        /// <summary>
        /// Gets or Sets the name.
        /// </summary>
        [DataMember(Name = "name")]
		public string? Name { get; set; }

        /// <summary>
        /// Gets or Sets the domain.
        /// </summary>
        [DataMember(Name = "domain")]
		public string? Domain { get; set; }

        /// <summary>
        /// Gets or Sets the secure URL.
        /// </summary>
        [DataMember(Name = "secure_URL")]
		public string? SecureURL { get; set; }

        /// <summary>
        /// Gets or Sets the weight units.
        /// </summary>
        [DataMember(Name = "weight_units")]
		public string? WeightUnits { get; set; }
	}
}
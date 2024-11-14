using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Product
{
    /// <summary>
    /// The big commerce brand.
    /// </summary>
    [ DataContract ]
	public class BigCommerceBrand: BigCommerceObjectBase
	{
        /// <summary>
        /// Gets or Sets the name.
        /// </summary>
        [ DataMember( Name = "name" ) ]
		public string? Name{ get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BigCommerceBrand"/> class.
        /// </summary>
        public BigCommerceBrand()
		{
		}
	}
}
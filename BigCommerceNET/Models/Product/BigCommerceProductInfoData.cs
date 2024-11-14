using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Product
{
    /// <summary>
    /// The big commerce product info data.
    /// </summary>
    [ DataContract ]
	public class BigCommerceProductInfoData: BigCommerceProductBase
	{
        /// <summary>
        /// Gets or Sets the data.
        /// </summary>
        [ DataMember( Name = "data" ) ]
		public List< BigCommerceProductInfo > Data{ get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BigCommerceProductInfoData"/> class.
        /// </summary>
        public BigCommerceProductInfoData()
		{
			this.Data = new List< BigCommerceProductInfo >();
		}
	}
}
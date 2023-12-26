using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Category
{
    /// <summary>
    /// The big commerce category info data.
    /// </summary>
    [ DataContract ]
	public class BigCommerceCategoryInfoData : BigCommerceObjectBase
	{
        /// <summary>
        /// Gets or Sets the data.
        /// </summary>
        [DataMember(Name = "data")]
		public BigCommerceCategoryInfo [] Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BigCommerceCategoryInfoData"/> class.
        /// </summary>
        public BigCommerceCategoryInfoData()
		{
			this.Data = new BigCommerceCategoryInfo[500];
		}
	}
	
}
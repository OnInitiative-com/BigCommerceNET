using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Category
{
    /// <summary>
    /// The big commerce category.
    /// </summary>
    [ DataContract ]
	public class BigCommerceCategory : BigCommerceObjectBase
	{
        /// <summary>
        /// Gets or Sets the category URL.
        /// </summary>
        [ DataMember( Name = "url") ]
		public BigCommerceCategoryURL Category_URL { get; set; }

        /// <summary>
        /// Gets or Sets the category name.
        /// </summary>
        [ DataMember( Name = "name") ]
		public string Category_Name { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether is visible.
        /// </summary>
        [DataMember(Name = "is_visible")]
		public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or Sets the parent id.
        /// </summary>
        [DataMember(Name = "parent_id")]
        public int Parent_Id { get; set; }

        /// <summary>
        /// Gets or Sets the sort order.
        /// </summary>
        [DataMember(Name = "sort_order")]
        public int SortOrder { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="BigCommerceCategory"/> class.
        /// </summary>
        public BigCommerceCategory()
		{
			this.Category_URL = new BigCommerceCategoryURL();
			this.Category_Name = "";
		}
	}
}
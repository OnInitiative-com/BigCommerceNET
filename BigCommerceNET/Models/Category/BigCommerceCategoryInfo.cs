using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Category
{
    /// <summary>
    /// The big commerce category info.
    /// </summary>
    [ DataContract ]
	public class BigCommerceCategoryInfo
	{
        /// <summary>
        /// Gets or Sets the id.
        /// </summary>
        [ DataMember( Name = "id" ) ]
		public int Id{ get; set; }

        /// <summary>
        /// Gets or Sets the name.
        /// </summary>
        [ DataMember( Name = "name" ) ]
		public string? Name{ get; set; }

        /// <summary>
        /// Gets or Sets the category URL.
        /// </summary>
        [DataMember(Name = "custom_url")]		
		public BigCommerceCategoryURL? Category_URL { get; set; }

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

    }
}
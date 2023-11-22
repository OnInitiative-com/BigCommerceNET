using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Category
{
	[ DataContract ]
	public class BigCommerceCategoryInfo
	{
		[ DataMember( Name = "id" ) ]
		public int Id{ get; set; }

		[ DataMember( Name = "name" ) ]
		public string? Name{ get; set; }

		[DataMember(Name = "custom_url")]		
		public BigCommerceCategoryURL? Category_URL { get; set; }

		[DataMember(Name = "is_visible")]
		public bool IsVisible { get; set; }

        [DataMember(Name = "parent_id")]
        public int Parent_Id { get; set; }

        [DataMember(Name = "sort_order")]
        public int SortOrder { get; set; }

    }
}
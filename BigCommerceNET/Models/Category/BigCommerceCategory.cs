using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Category
{
	[ DataContract ]
	public class BigCommerceCategory : BigCommerceObjectBase
	{
		[ DataMember( Name = "url") ]
		public BigCommerceCategoryURL Category_URL { get; set; }      

        [ DataMember( Name = "name") ]
		public string Category_Name { get; set; }

		[DataMember(Name = "is_visible")]
		public bool IsVisible { get; set; }

        [DataMember(Name = "parent_id")]
        public int Parent_Id { get; set; }

        [DataMember(Name = "sort_order")]
        public int SortOrder { get; set; }
        public BigCommerceCategory()
		{
			this.Category_URL = new BigCommerceCategoryURL();
			this.Category_Name = "";
		}
	}
}
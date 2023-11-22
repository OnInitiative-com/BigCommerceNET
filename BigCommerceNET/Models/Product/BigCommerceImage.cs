using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Product
{
	[ DataContract ]
	public class BigCommerceImage
	{

        [DataMember(Name = "image_file")]
        public string? ImageFile { get; set; }

        [DataMember(Name = "is_thumbnail")]
        public bool IsThumbnail { get; set; }

        [DataMember(Name = "sort_order")]
        public int? SortOrder { get; set; }

        [DataMember(Name = "description")]
        public string? Description { get; set; }

        [DataMember(Name = "image_url")]
        public string? ImageUrl { get; set; }

        [ DataMember( Name = "url_standard" ) ]
		public string? UrlStandard{ get; set; }

        [ DataMember( Name = "date_modified" ) ]
        public string? DateModified { get; set; }

        
       

        
        
	}
}
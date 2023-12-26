using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Product
{
    /// <summary>
    /// The big commerce image.
    /// </summary>
    [ DataContract ]
	public class BigCommerceImage
	{

        /// <summary>
        /// Gets or Sets the image file.
        /// </summary>
        [DataMember(Name = "image_file")]
        public string? ImageFile { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether is thumbnail.
        /// </summary>
        [DataMember(Name = "is_thumbnail")]
        public bool IsThumbnail { get; set; }

        /// <summary>
        /// Gets or Sets the sort order.
        /// </summary>
        [DataMember(Name = "sort_order")]
        public int? SortOrder { get; set; }

        /// <summary>
        /// Gets or Sets the description.
        /// </summary>
        [DataMember(Name = "description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or Sets the image url.
        /// </summary>
        [DataMember(Name = "image_url")]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Gets or Sets the url standard.
        /// </summary>
        [ DataMember( Name = "url_standard" ) ]
		public string? UrlStandard{ get; set; }

        /// <summary>
        /// Gets or Sets the date modified.
        /// </summary>
        [ DataMember( Name = "date_modified" ) ]
        public string? DateModified { get; set; }

        
       

        
        
	}
}
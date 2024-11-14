using System.Runtime.Serialization;

namespace BigCommerceNET.Models
{
    /// <summary>
    /// The big commerce reference object.
    /// </summary>
    [ DataContract ]
	public sealed class BigCommerceReferenceObject
	{
        /// <summary>
        /// Gets or Sets the url.
        /// </summary>
        [ DataMember( Name = "url" ) ]
		public string? Url { get; set; }

        /// <summary>
        /// Gets or Sets the resource.
        /// </summary>
        [ DataMember( Name = "resource" ) ]
		public string? Resource { get; set; }
	}
}
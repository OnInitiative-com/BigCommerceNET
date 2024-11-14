using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Product
{
    /// <summary>
    /// The option value.
    /// </summary>
    [DataContract]
    public class OptionValue
    {

        /// <summary>
        /// Gets or Sets the option display name.
        /// </summary>
        [DataMember(Name = "option_display_name")]
        public string? option_display_name { get; set; }

        /// <summary>
        /// Gets or Sets the label.
        /// </summary>
        [DataMember(Name = "label")]
        public string? label { get; set; }

        /// <summary>
        /// Gets or Sets the id.
        /// </summary>
        [DataMember(Name = "id")]
        public int? id { get; set; }

        /// <summary>
        /// Gets or Sets the option id.
        /// </summary>
        [DataMember(Name = "option_id")]
        public int? option_id { get; set; }
    }
}

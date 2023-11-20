using System.Runtime.Serialization;

namespace BigCommerceNET.Models.Product
{
    [DataContract]
    public class OptionValue
    {      

        [DataMember(Name = "option_display_name")]
        public string? option_display_name { get; set; }

        [DataMember(Name = "label")]
        public string? label { get; set; }

        [DataMember(Name = "id")]
        public int? id { get; set; }

        [DataMember(Name = "option_id")]
        public int? option_id { get; set; }
    }
}

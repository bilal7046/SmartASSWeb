using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SmartASSWeb.Dal.Entities
{
    
    public class Tag : EntityBase
    {

        [Key]
        public int TagId { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("ColorCode")]
        public string ColorCode { get; set; }
        [JsonProperty("UserId")]
        public string UserId { get; set; }
    }
}

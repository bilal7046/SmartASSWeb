using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SmartASSWeb.Dal.Entities
{
    
    public class ContactGroup : EntityBase
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("sequence")]
        public int Sequence { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
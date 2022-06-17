using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SmartASSWeb.Dal.Entities
{
    
    public class LeadNote : EntityBase
    {
        [Key]

        public int LeadNoteId { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("leadId")]
        public string LeadId { get; set; }
    }
}
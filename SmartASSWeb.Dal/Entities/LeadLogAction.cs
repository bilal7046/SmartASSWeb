using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SmartASSWeb.Dal.Entities
{
    
    public class LeadLogAction : EntityBase
    {

        [Key]
        public int LeadLogActionId { get; set; }
        [Required]
        [JsonProperty("logAction")]
        public string LogAction { get; set; }
        [Required]
        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("dueDate")]
        public DateTime? DueDate { get; set; }

        [JsonProperty("leadId")]
        public string LeadId { get; set; }

        public string DueDateString { get; set; }
    }
}
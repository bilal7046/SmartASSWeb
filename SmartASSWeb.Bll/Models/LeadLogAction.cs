using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class LeadLogAction
    {
        [FirestoreDocumentId]
        public string LeadLogActionId { get; set; }
        [Required]
        [JsonProperty("logAction")]
        [FirestoreProperty("logAction")]
        public string LogAction { get; set; }
        [Required]
        [JsonProperty("notes")]
        [FirestoreProperty("notes")]
        public string Notes { get; set; }
        [FirestoreProperty("dateCreated")]
        public Timestamp DateCreated { get; set; }

        [JsonProperty("subject")]
        [FirestoreProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("dueDate")]
        [FirestoreProperty("dueDate")]
        public Timestamp? DueDate { get; set; }

        [JsonProperty("leadId")]
        [FirestoreProperty("leadId")]
        public string LeadId { get; set; }

        public string DueDateString { get; set; }
    }
}
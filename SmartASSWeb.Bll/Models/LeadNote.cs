using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class LeadNote
    {
        [FirestoreDocumentId]
        public string LeadNoteId { get; set; }
        [JsonProperty("subject")]
        [FirestoreProperty("subject")]
        public string Subject { get; set; }
        [FirestoreProperty("dateCreated")]
        public Timestamp DateCreated { get; set; }
        [JsonProperty("text")]
        [FirestoreProperty("text")]
        public string Text { get; set; }

        [JsonProperty("leadId")]
        [FirestoreProperty("leadId")]
        public string LeadId { get; set; }
    }
}
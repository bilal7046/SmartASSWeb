using Google.Cloud.Firestore;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class Tag
    {
        [FirestoreDocumentId]
        public string TagId { get; set; }
        [FirestoreProperty("Name")]
        public string Name { get; set; }
        [FirestoreProperty("ColorCode")]
        public string ColorCode { get; set; }
        [FirestoreProperty("UserId")]
        public string UserId { get; set; }
    }
}

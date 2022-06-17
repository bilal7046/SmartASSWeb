using Google.Cloud.Firestore;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class UserAction
    {
        [FirestoreDocumentId]
        public string UserActionId { get; set; }
        [FirestoreProperty("requestCount")]
        public int RequestCount { get; set; }
        [FirestoreProperty("userId")]
        public string UserId { get; set; }
    }
}
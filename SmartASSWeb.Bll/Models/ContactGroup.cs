using Google.Cloud.Firestore;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class ContactGroup
    {
        [FirestoreProperty("id")]
        public string Id { get; set; }
        [FirestoreProperty("name")]
        public string Name { get; set; }
        [FirestoreProperty("sequence")]
        public int Sequence { get; set; }
        [FirestoreProperty("userId")]
        public string UserId { get; set; }
    }
}
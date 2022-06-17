using Google.Cloud.Firestore;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class ConversionGoal
    {
        [FirestoreDocumentId]
        public string Id { get; set; }
        [FirestoreProperty("userId")]
        public string UserId { get; set; }
        [FirestoreProperty("start")]
        public Timestamp Start { get; set; }
        [FirestoreProperty("end")]
        public Timestamp End { get; set; }
        [FirestoreProperty("name")]
        public string Name { get; set; }
        [FirestoreProperty("period")]
        public string Period { get; set; }
        [FirestoreProperty("conversionValue")]
        public string ConversionValue { get; set; }

        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
    }
}

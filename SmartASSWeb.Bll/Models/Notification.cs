using Google.Cloud.Firestore;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class Notification
    {
        private string _requestorPhotoUrl;

        [FirestoreDocumentId]
        public string Id { get; set; }
        [FirestoreProperty("data")]
        public string Data { get; set; }
        [FirestoreProperty("dateCreated")]
        public Timestamp DateCreated { get; set; }
        [FirestoreProperty("isRead")]
        public bool IsRead { get; set; }
        [FirestoreProperty("message")]
        public string Message { get; set; }
        [FirestoreProperty("notificationType")]
        public string NotificationType { get; set; }
        [FirestoreProperty("userId")]
        public string UserId { get; set; }
        [FirestoreProperty("requestorUserId")]
        public string RequestorUserId { get; set; }
        [FirestoreProperty("requestorName")]
        public string RequestorName { get; set; }

        [FirestoreProperty("requestorPhotoUrl")]
        public string RequestorPhotoUrl
        {
            get => string.IsNullOrEmpty(_requestorPhotoUrl) ? Defaults.DefaultProfileImage : _requestorPhotoUrl;
            set => _requestorPhotoUrl = value;
        }
    }
}
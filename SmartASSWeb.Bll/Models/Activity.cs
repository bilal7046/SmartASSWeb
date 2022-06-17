using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class Activity
    {
        private string _activityCreatorImage;

        [FirestoreDocumentId]
        public string ActivityId { get; set; }
        [FirestoreProperty("activityType")]
        public string ActivityType { get; set; }
        [FirestoreProperty("activityMessage")]
        public string ActivityMessage { get; set; }
        [FirestoreProperty("activityCreator")]
        public string ActivityCreator { get; set; }

        [FirestoreProperty("activityCreatorImage")]
        public string ActivityCreatorImage
        {
            get => string.IsNullOrEmpty(_activityCreatorImage) ? Defaults.DefaultProfileImage : _activityCreatorImage;
            set => _activityCreatorImage = value;
        }

        [FirestoreProperty("activityTime")]
        public Timestamp ActivityTime { get; set; }
        [Required]
        [FirestoreProperty("activityOwner")]
        public string ActivityOwner { get; set; }

        [FirestoreProperty("activityBusiness")]
        public string ActivityBusiness { get; set; }

        [FirestoreProperty("activityCreatorName")]
        public string ActivityCreatorName { get; set; }
    }
}
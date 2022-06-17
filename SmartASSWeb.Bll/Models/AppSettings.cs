using Google.Cloud.Firestore;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class AppSettings
    {
        [FirestoreProperty("settingName")]
        public string SettingName { get; set; }
        [FirestoreProperty("settingValue")]
        public string SettingValue { get; set; }
        [FirestoreProperty("userId")]
        public string UserId { get; set; }
    }
}

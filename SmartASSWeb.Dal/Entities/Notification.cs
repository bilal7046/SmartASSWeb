using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SmartASSWeb.Dal.Entities
{
    
    public class Notification : EntityBase
    {
        [Key]
        public int Id { get; set; }
        [JsonProperty("data")]
        public string Data { get; set; }
        [JsonProperty("isRead")]
        public bool IsRead { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("notificationType")]
        public string NotificationType { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("requestorUserId")]
        public string RequestorUserId { get; set; }
        [JsonProperty("requestorName")]
        public string RequestorName { get; set; }

        [JsonProperty("requestorPhotoUrl")]
        public string RequestorPhotoUrl
        {
            get;
            set;
        }
    }
}
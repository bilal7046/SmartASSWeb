using System.ComponentModel.DataAnnotations;

namespace SmartASSWeb.Dal.Entities
{
    
    public class AppSettings : EntityBase
    {
        [Key]
        public int AppSettingsId { get; set; }
        [MaxLength(200)]
        public string SettingName { get; set; }
        [MaxLength(200)]
        public string SettingValue { get; set; }
        [MaxLength(200)]
        public string UserId { get; set; }
    }
}

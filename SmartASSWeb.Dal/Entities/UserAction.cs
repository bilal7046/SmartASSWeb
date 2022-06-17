using System.ComponentModel.DataAnnotations;

namespace SmartASSWeb.Dal.Entities
{
    public class UserAction : EntityBase
    {
        [Key]

        public string UserActionId { get; set; }
        public int RequestCount { get; set; }
        public string UserId { get; set; }
    }
}
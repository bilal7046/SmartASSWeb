using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SmartASSWeb.Dal.Entities
{
    public class Activity : EntityBase
    {
        [Key]
        public int ActivityId { get; set; }
        [MaxLength(200)]
        public string ActivityType { get; set; }
        [MaxLength(2000)]
        public string ActivityMessage { get; set; }
        [MaxLength(200)]
        public string ActivityCreator { get; set; }

        [MaxLength(200)]
        public string ActivityCreatorImage
        {
            get;set;
        }
        
        public DateTime ActivityTime { get; set; }
        [Required]
        [MaxLength(200)]
        public string ActivityOwner { get; set; }

        [MaxLength(200)]
        public string ActivityBusiness { get; set; }

        [MaxLength(200)]
        public string ActivityCreatorName { get; set; }
    }
}
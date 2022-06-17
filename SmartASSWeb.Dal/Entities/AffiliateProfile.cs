using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SmartASSWeb.Dal.Entities
{
    
    public class AffiliateProfile : EntityBase
    {
        [Key]
        public int AffiliateProfileId { get; set; }
        [MaxLength(200)]
        public string AffiliateCode { get; set; }
        [Required]
        [MaxLength(200)]
        public string Username { get; set; }
        [MaxLength(200)]
        public string Password { get; set; }
        [Required]
        [MaxLength(800)]
        public string VanityUrl { get; set; }
        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(200)]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(500)]
        public string EmailAddress{ get; set; }
        [MaxLength(200)]
        public string Website { get; set; }
        [MaxLength(200)]
        public string AffiliateAccountStatus { get; set; } = "Pending";
        //
        public string ConfirmPassword { get; set; }
    }
}
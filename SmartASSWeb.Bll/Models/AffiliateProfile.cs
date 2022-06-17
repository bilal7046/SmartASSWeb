using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class AffiliateProfile
    {
        [FirestoreDocumentId]
        public string AffiliateProfileId { get; set; }
        [FirestoreProperty("code")]
        public string AffiliateCode { get; set; }
        [Required]
        [FirestoreProperty("username")]
        public string Username { get; set; }
        [FirestoreProperty("password")]
        public string Password { get; set; }
        [Required]
        [FirestoreProperty("vanityUrl")]
        public string VanityUrl { get; set; }
        [Required]
        [FirestoreProperty("firstName")]
        public string FirstName { get; set; }
        [Required]
        [FirestoreProperty("lastName")]
        public string LastName { get; set; }
        [Required]
        [FirestoreProperty("phone")]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        [FirestoreProperty("emailAddress")]
        public string EmailAddress{ get; set; }
        [FirestoreProperty("website")]
        public string Website { get; set; }
        [FirestoreProperty("accountStatus")]
        public string AffiliateAccountStatus { get; set; } = "Pending";
        //
        public string ConfirmPassword { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;
using DateTime = System.DateTime;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class UserProfile
    {
        private string _photoUrl;
        private Timestamp _dateOfBirth;
        private DateTime? _dateOfBirthHolder;

        public UserProfile()
        {
            LinkedUsers = new string[]{};
            TeamMembers = new string[]{};
            ConnectedBusinessCards = new string[] { };
        }
        
        [FirestoreDocumentId]
        public string UserId { get; set; }
        [Required]
        [FirestoreProperty("firstName")]
        public string FirstName { get; set; }
        [Required]
        [FirestoreProperty("lastName")]
        public string LastName { get; set; }
        [FirestoreProperty("email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [FirestoreProperty("addressLine1")]
        [Required]
        public string AddressLine1 { get; set; }
        [FirestoreProperty("addressLine2")]
        public string AddressLine2 { get; set; }
        [Required]
        [FirestoreProperty("phone")]
        public string Phone { get; set; }
        [Required]
        [FirestoreProperty("mobile")]
        public string Mobile { get; set; }
        [Required]
        [FirestoreProperty("city")]
        public string City { get; set; }
        [Required]
        [FirestoreProperty("province")]
        public string Province { get; set; }
        [Required]
        [FirestoreProperty("country")]
        public string Country { get; set; }
        [Required]
        [FirestoreProperty("postalCode")]
        public string PostalCode { get; set; }
        [Required]
        [FirestoreProperty("gender")]
        public string Gender { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime? DateOfBirthHolder
        {
            get
            {
                if (_dateOfBirthHolder == null)
                {
                    return DateOfBirth?.ToDateTime();
                }
                return _dateOfBirthHolder;
            }
            set => _dateOfBirthHolder = value;
        }

        [Required]
        [FirestoreProperty("dateOfBirth")]
        public Timestamp? DateOfBirth { get; set; }

        [FirestoreProperty("profileSummary")]
        public string ProfileSummary { get; set; }
        [FirestoreProperty("designation")]
        public string Designation { get; set; }
        [FirestoreProperty("socialTwitterUrl")]
        public string SocialTwitterUrl { get; set; }
        [FirestoreProperty("socialFacebookUrl")]
        public string SocialFacebookUrl { get; set; }
        [FirestoreProperty("socialLinkedUrl")]
        public string SocialLinkedUrl { get; set; }
        [FirestoreProperty("socialInstagramUrl")]
        public string SocialInstagramUrl { get; set; }
        [FirestoreProperty("socialMediaLink1")]
        public string SocialMediaLink1 { get; set; }
        [FirestoreProperty("socialMediaLink2")]
        public string SocialMediaLink2 { get; set; }

        [FirestoreProperty("photoUrl")]
        public string PhotoUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_photoUrl))
                    return Defaults.DefaultProfileImage;
                else
                    return _photoUrl;
            }
            set => _photoUrl = value;
        }
        [FirestoreProperty("website")]
        public string Website { get; set; }
        [FirestoreProperty("package")]
        public string Package { get; set; }
        [FirestoreProperty("employeeCode")]
        public string EmployeeCode { get; set; }
        [FirestoreProperty("linkedUsers")]
        public string[] LinkedUsers { get; set; }
        [FirestoreProperty("teamMembers")]
        public string[] TeamMembers { get; set; }
        [FirestoreProperty("connectedBusinessCards")]
        public string[] ConnectedBusinessCards { get; set; }
        [FirestoreProperty("createdAt")]
        public Timestamp CreatedAt { get; set; }
        [FirestoreProperty("updatedAt")]
        public Timestamp UpdatedAt { get; set; }

        public string ProfileIDUrl => $"https://smartass.global/{this.UserId}";
        public string FullName => $"{this.FirstName} {this.LastName}";
    }
}

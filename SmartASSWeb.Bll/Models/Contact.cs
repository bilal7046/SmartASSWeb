using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class Contact
    {
        private string _photoUrl;

        [FirestoreDocumentId]
        public string ContactId { get; set; }
        [Required]
        [FirestoreProperty("firstName")]
        public string FirstName { get; set; }
        [Required]
        [FirestoreProperty("lastName")]
        public string LastName { get; set; }
        [Required]
        [FirestoreProperty("phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string Phone { get; set; }
        [Required]
        [FirestoreProperty("mobile")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Mobile number")]
        public string Mobile { get; set; }
        [Required]
        [EmailAddress]
        [FirestoreProperty("emailAddress")]
        public string EmailAddress { get; set; }
        [FirestoreProperty("website")]
        public string Website { get; set; }
        [FirestoreProperty("addressLine1")]
        public string AddressLine1 { get; set; }
        [FirestoreProperty("addressLine2")]
        public string AddressLine2 { get; set; }
        [FirestoreProperty("city")]
        public string City { get; set; }
        [FirestoreProperty("province")]
        public string Province { get; set; }
        [FirestoreProperty("country")]
        public string Country { get; set; }
        [FirestoreProperty("postalCode")]
        public string PostalCode { get; set; }
        [FirestoreProperty("photoURL")]
        public string PhotoUrl
        {
            get => string.IsNullOrEmpty(_photoUrl) ? Defaults.DefaultProfileImage : _photoUrl;
            set => _photoUrl = value;
        }

        [Required]
        [FirestoreProperty("userId")]
        public string UserId { get; set; }
        [FirestoreProperty("dateCreated")]
        public Timestamp DateCreated { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";
    }
}

using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class Enterprise
    {
        [FirestoreDocumentId]
        public string EnterpriseId { get; set; }
        [Required(ErrorMessage = "Enterprise Name is required")]
        [FirestoreProperty("name")] 
        public string Name { get; set; }
        [Required]
        [FirestoreProperty("code")] 
        public string Code { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [FirestoreProperty("address1")]
        public string Address1 { get; set; }

        [FirestoreProperty("address2")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "Province is Required")]
        [FirestoreProperty("province")]
        public string Province { get; set; }

        [Required(ErrorMessage = "City is required")]
        [FirestoreProperty("city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is Required")]
        [FirestoreProperty("country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Postal code is Required")]
        [FirestoreProperty("postalCode")]
        public string PostalCode { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is Required")]
        [FirestoreProperty("email")]
        public string Email { get; set; }

        [Url(ErrorMessage = "Must be a valid url")]
        [FirestoreProperty("website")]
        public string Website { get; set; }

        [Required(ErrorMessage = "Phone is Required")]
        [FirestoreProperty("telephone1")]
        public string Phone1 { get; set; }

        [FirestoreProperty("telephone2")]
        public string Phone2 { get; set; }

        [Required(ErrorMessage = "Mobile is Required")]
        [FirestoreProperty("mobile")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Industry is Required")]
        [FirestoreProperty("industry")]
        public string Industry { get; set; }

        [Required(ErrorMessage = "Manager First Name is Required")]
        [FirestoreProperty("managerName")]
        public string ManagerFirstName { get; set; }

        [Required(ErrorMessage = "Manager Last Name is Required")]
        [FirestoreProperty("managerLastName")]
        public string ManagerLastName { get; set; }

        [Required(ErrorMessage = "Manager Title is Required")]
        [FirestoreProperty("managerTitle")]
        public string ManagerTitle { get; set; }

        [Required(ErrorMessage = "Manager Phone is Required")]
        [FirestoreProperty("managerPhone")]
        public string ManagerPhone { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Required(ErrorMessage = "Manager Email is Required")]
        [FirestoreProperty("managerEmail")]
        public string ManagerEmail { get; set; }


    }
}
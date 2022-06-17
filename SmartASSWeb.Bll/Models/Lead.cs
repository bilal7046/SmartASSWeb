using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Google.Cloud.Firestore;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class Lead
    {
        [FirestoreDocumentId]
        public string LeadId { get; set; }
        [Required(ErrorMessage = "Lead Owner is Required")]
        [FirestoreProperty("leadOwner")]
        public string LeadOwner { get; set; }
        [Required]
        [Display(Name = "Lead Status")]
        [FirestoreProperty("leadStatus")]
        public string LeadStatus { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [FirestoreProperty("firstName")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [FirestoreProperty("lastName")]
        public string LastName { get; set; }
        [FirestoreProperty("website")]
        public string Website { get; set; }
        [Required]
        [Display(Name = "Title*")]
        [FirestoreProperty("title")]
        public string Title { get; set; }
        [FirestoreProperty("company")]
        public string Company { get; set; }
        [Required]
        [Display(Name = "Email*")]
        [FirestoreProperty("email")]
        public string Email { get; set; }
        [FirestoreProperty("noOfEmployees")]
        public int NoOfEmployees { get; set; }
        [FirestoreProperty("industry")]
        public string Industry { get; set; }
        [FirestoreProperty("leadSource")]
        public string LeadSource { get; set; }
        [Required]
        [Display(Name = "Phone*")]
        [FirestoreProperty("phone")]
        public string Phone { get; set; }
        [FirestoreProperty("mobile")]
        public string Mobile { get; set; }
        [Required]
        [Display(Name = "Address 1*")]
        [FirestoreProperty("address1")]
        public string Address1 { get; set; }
        [FirestoreProperty("address2")]
        public string Address2 { get; set; }
        [Display(Name = "City*")]
        [FirestoreProperty("city")]
        public string City { get; set; }
        [Display(Name = "Province*")]
        [FirestoreProperty("province")]
        public string Province { get; set; }
        [Required]
        [Display(Name = "Country*")]
        [FirestoreProperty("country")]
        public string Country { get; set; }
        [Required]
        [Display(Name = "Postal Code*")]
        [FirestoreProperty("postalCode")]
        public string PostalCode { get; set; }
        [FirestoreProperty("notes")]
        public string Notes { get; set; }
        [Required]
        [FirestoreProperty("userId")]
        public string UserId { get; set; }
        [FirestoreProperty("revenue")]
        public double Revenue { get; set; }
        [FirestoreProperty("dateCreated")]
        public Timestamp DateCreated { get; set; }

        public string DateMonthString => this.DateCreated.ToDateTime().ToString("MMM");
        public int DateMonthNumber => this.DateCreated.ToDateTime().Month;
    }
}
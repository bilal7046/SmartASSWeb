using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SmartASSWeb.Dal.Entities
{
    
    public class Lead : EntityBase
    {
        [Key]
        public int LeadId { get; set; }
        [Required(ErrorMessage = "Lead Owner is Required")]
        [JsonProperty("leadOwner")]
        public string LeadOwner { get; set; }
        [Required]
        [Display(Name = "Lead Status")]
        [JsonProperty("leadStatus")]
        public string LeadStatus { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("website")]
        public string Website { get; set; }
        [Required]
        [Display(Name = "Title*")]
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("company")]
        public string Company { get; set; }
        [Required]
        [Display(Name = "Email*")]
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("noOfEmployees")]
        public int NoOfEmployees { get; set; }
        [JsonProperty("industry")]
        public string Industry { get; set; }
        [JsonProperty("leadSource")]
        public string LeadSource { get; set; }
        [Required]
        [Display(Name = "Phone*")]
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("mobile")]
        public string Mobile { get; set; }
        [Required]
        [Display(Name = "Address 1*")]
        [JsonProperty("address1")]
        public string Address1 { get; set; }
        [JsonProperty("address2")]
        public string Address2 { get; set; }
        [Display(Name = "City*")]
        [JsonProperty("city")]
        public string City { get; set; }
        [Display(Name = "Province*")]
        [JsonProperty("province")]
        public string Province { get; set; }
        [Required]
        [Display(Name = "Country*")]
        [JsonProperty("country")]
        public string Country { get; set; }
        [Required]
        [Display(Name = "Postal Code*")]
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }
        [JsonProperty("notes")]
        public string Notes { get; set; }
        [Required]
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("revenue")]
        public double Revenue { get; set; }

        public string DateMonthString => this.DateCreated.ToString("MMM");
        public int DateMonthNumber => this.DateCreated.Month;
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SmartASSWeb.Dal.Entities
{
    
    public class Contact : EntityBase
    {
        private string _photoUrl;


        [Key]
        public int ContactId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [Required]
        [JsonProperty("mobile")]
        public string Mobile { get; set; }
        [Required]
        [EmailAddress]
        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }
        [JsonProperty("website")]
        public string Website { get; set; }
        [JsonProperty("addressLine1")]
        public string AddressLine1 { get; set; }
        [JsonProperty("addressLine2")]
        public string AddressLine2 { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("province")]
        public string Province { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }
        [JsonProperty("photoURL")]
        public string PhotoUrl
        {
            get;
            set;
        }

        [Required]
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("dateCreated")]
        public DateTime DateCreated { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";
    }
}

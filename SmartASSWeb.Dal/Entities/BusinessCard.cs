using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace SmartASSWeb.Dal.Entities
{
    
    public class BusinessCard : EntityBase
    {
        #region Ctor
        
        #endregion

        [Key]
        public int BusinessCardId { get; set; }
        [Required(ErrorMessage = "Company name is required")]
        [MaxLength(500)]
        public string Name { get; set; }
        [MaxLength(800)]
        public string Slogan { get; set; }
        [Required(ErrorMessage = "Overview is required")]
        [MaxLength(500)]
        public string Overview { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [MaxLength(200)]
        public string Address1 { get; set; }
        [MaxLength(200)]
        public string Address2 { get; set; }
        [Required(ErrorMessage = "City is required")]
        [MaxLength(200)]
        public string City { get; set; }
        [Required(ErrorMessage = "Company Type is Required")]
        [MaxLength(200)]
        public string CompanyType { get; set; }
        [Required(ErrorMessage = "Country is Required")]
        [MaxLength(200)]
        public string Country { get; set; }
        [Required(ErrorMessage = "Postal code is Required")]
        [MaxLength(200)]
        public string PostalCode { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is Required")]
        [MaxLength(200)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Province is Required")]
        [MaxLength(200)]
        public string Province { get; set; }
        [Required(ErrorMessage = "Phone is Required")]
        [MaxLength(200)]
        public string Phone1 { get; set; }
        [MaxLength(200)]
        public string Phone2 { get; set; }
        [Required(ErrorMessage = "Industry is Required")]
        [MaxLength(200)]
        public string Industry { get; set; }
        [Required(ErrorMessage = "Mobile is Required")]
        [MaxLength(200)]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Title is Required")]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Job Level is Required")]
        [MaxLength(200)]
        public string JobLevel { get; set; }
        [MaxLength(200)]
        public string UserId { get; set; }
        [Url(ErrorMessage = "Must be a valid url")]
        [MaxLength(200)]
        public string Website { get; set; }
        [NotMapped]
        public ICollection<string> Tags { get; set; } = new List<string>();
        /// <summary> <see cref="Tags"/> for database persistence. </summary>
        [Obsolete("Only for Persistence by EntityFramework")]
        public string TagList
        {
            get => Tags == null || !Tags.Any()
                    ? null
                    : JsonConvert.SerializeObject(Tags);

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    Tags.Clear();
                else
                    Tags = JsonConvert.DeserializeObject<ICollection<string>>(value);
            }
        }
        /// <summary>
        /// Products or Services offered under this business card profile
        /// </summary>

        [NotMapped]
        public ICollection<string> ProductServices { get; set; } = new List<string>();
        /// <summary> <see cref="ProductServices"/> for database persistence. </summary>
        [Obsolete("Only for Persistence by EntityFramework")]
        public string ProductServiceList
        {
            get => ProductServices == null || !ProductServices.Any()
                ? null
                : JsonConvert.SerializeObject(ProductServices);

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    ProductServices.Clear();
                else
                    ProductServices = JsonConvert.DeserializeObject<ICollection<string>>(value);
            }
        }
        /// <summary>
        /// Set of users the business card has been shared with
        /// </summary>

        [NotMapped]
        public ICollection<string> ConnectedUsers { get; set; } = new List<string>();
        /// <summary> <see cref="ConnectedUsers"/> for database persistence. </summary>
        [Obsolete("Only for Persistence by EntityFramework")]
        public string ConnectedUserList
        {
            get => ConnectedUsers == null || !ConnectedUsers.Any()
                ? null
                : JsonConvert.SerializeObject(Tags);

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    ConnectedUsers.Clear();
                else
                    ConnectedUsers = JsonConvert.DeserializeObject<ICollection<string>>(value);
            }
        }
        public string MissionStatement { get; set; }
        
        public string LogoUrl
        {
            get;
            set;
        }
        [MaxLength(200)]
        public string Theme { get; set; }

        [MaxLength(200)]
        public string ThemeColor
        {
            get;
            set;
        }

        [MaxLength(200)]
        public string CompanyFontColor
        {
            get;
            set;
        }
        [MaxLength(200)]
        public string PersonFontColor
        {
            get;
            set;
        }

        [MaxLength(200)]
        public string PersonFontSize
        {
            get;
            set;
        }

        [MaxLength(200)]
        public string TitleFontSize
        {
            get;
            set;
        }
        [MaxLength(200)]
        public string CompanyFontSize
        {
            get;
            set;
        }
        [MaxLength(200)]
        public string SloganFontSize
        {
            get;
            set;
        }
        [MaxLength(200)]
        public string ThemeBackgroundImage { get; set; }

        [Url(ErrorMessage = "Must be a valid url")]
        [MaxLength(200)]
        public string SocialLinkedUrl { get; set; }
        [Url(ErrorMessage = "Must be a valid url")]
        [MaxLength(200)]
        public string SocialFacebookUrl { get; set; }
        [Url(ErrorMessage = "Must be a valid url")]
        [MaxLength(200)]
        public string SocialTwitterUrl { get; set; }
        [Url(ErrorMessage = "Must be a valid url")]
        [MaxLength(200)]
        public string SocialInstagramUrl { get; set; }
        [Url(ErrorMessage = "Must be a valid url")]
        [MaxLength(200)]
        public string SocialYoutubeUrl { get; set; }
        [Required(ErrorMessage = "Please select at least one template")]
        [MaxLength(200)]
        public string BusinessCardVersion { get; set; }
        //
        [MaxLength(200)]
        public string ProfileImage { get; set; }
        [MaxLength(200)]
        public string  ProfileFullName { get; set; }
        [MaxLength(200)]
        public string ProfileEmail { get; set; }
    }
}
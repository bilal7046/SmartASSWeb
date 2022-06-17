using System.ComponentModel.DataAnnotations;
using System.Web;
using Google.Cloud.Firestore;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class BusinessCard
    {
        #region Ctor

        private string _logoUrl;
        private string _companyFontColor;
        private string _personFontColor;
        private string _companyFontSize;
        private string _personFontSize;
        private string _titleFontSize;
        private string _sloganFontSize;
        private string _primaryFontSize;
        private string _secondaryFontSize;
        private string _themeColor;

        public BusinessCard()
        {
            Tags = new string[]{};
            ProductServices = new string[]{};
            ConnectedUsers = new string[] { };
        }

        #endregion

        [FirestoreDocumentId]
        public string BusinessCardId { get; set; }
        [Required(ErrorMessage = "Company name is required")]
        [FirestoreProperty("name")]
        public string Name { get; set; }
        [FirestoreProperty("slogan")]
        public string Slogan { get; set; }
        [Required(ErrorMessage = "Overview is required")]
        [MaxLength(500)]
        [FirestoreProperty("overview")]
        public string Overview { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [FirestoreProperty("address1")]
        public string Address1 { get; set; }
        [FirestoreProperty("address2")]
        public string Address2 { get; set; }
        [Required(ErrorMessage = "City is required")]
        [FirestoreProperty("city")]
        public string City { get; set; }
        [Required(ErrorMessage = "Company Type is Required")]
        [FirestoreProperty("companyType")]
        public string CompanyType { get; set; }
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
        [Required(ErrorMessage = "Province is Required")]
        [FirestoreProperty("province")]
        public string Province { get; set; }
        [Required(ErrorMessage = "Phone is Required")]
        [FirestoreProperty("telephone1")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string Phone1 { get; set; }
        [FirestoreProperty("telephone2")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string Phone2 { get; set; }
        [Required(ErrorMessage = "Industry is Required")]
        [FirestoreProperty("industry")]
        public string Industry { get; set; }
        [Required(ErrorMessage = "Mobile is Required")]
        [FirestoreProperty("mobile")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Title is Required")]
        [FirestoreProperty("title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Job Level is Required")]
        [FirestoreProperty("jobLevel")]
        public string JobLevel { get; set; }
        [FirestoreProperty("userId")]
        public string UserId { get; set; }
        [Url(ErrorMessage = "Must be a valid url")]
        [FirestoreProperty("website")]
        public string Website { get; set; }
        [FirestoreProperty("tags")]
        public string[] Tags { get; set; }
        /// <summary>
        /// Products or Services offered under this business card profile
        /// </summary>
        [FirestoreProperty("productServices")]
        public string[] ProductServices { get; set; }
        /// <summary>
        /// Set of users the business card has been shared with
        /// </summary>
        [FirestoreProperty("connectedUsers")]
        public string[] ConnectedUsers { get; set; }
        [FirestoreProperty("missionStatement")]
        public string MissionStatement { get; set; }

        [FirestoreProperty("logoUrl")]
        public string LogoUrl { get; set; }

        [FirestoreProperty("theme")]
        public string Theme { get; set; }

        [FirestoreProperty("themeColor")]
        public string ThemeColor
        {
            get => string.IsNullOrEmpty(_themeColor) ? Defaults.DefaultPrimaryThemeColor : _themeColor;
            set => _themeColor = value;
        }

        [FirestoreProperty("companyFontColor")]
        public string CompanyFontColor
        {
            get => string.IsNullOrEmpty(_companyFontColor) ? Defaults.DefaultPrimaryTextColor : _companyFontColor;
            set => _companyFontColor = value;
        }
        [FirestoreProperty("personFontColor")]
        public string PersonFontColor
        {
            get => string.IsNullOrEmpty(_personFontColor) ? Defaults.DefaultPersonFontColor : _personFontColor;
            set => _personFontColor = value;
        }

        [FirestoreProperty("personFontSize")]
        public string PersonFontSize
        {
            get => string.IsNullOrEmpty(_personFontSize) ? Defaults.DefaultPersonFontSize : _personFontSize;
            set => _personFontSize = (value.EndsWith("px") ? value : value + "px");
        }

        [FirestoreProperty("titleFontSize")]
        public string TitleFontSize
        {
            get => string.IsNullOrEmpty(_titleFontSize) ? Defaults.DefaultTitleFontSize : _titleFontSize;
            set => _titleFontSize = (value.EndsWith("px") ? value : value + "px");
        }
        [FirestoreProperty("companyFontSize")]
        public string CompanyFontSize
        {
            get => string.IsNullOrEmpty(_companyFontSize) ? Defaults.DefaultCompanyFontSize : _companyFontSize;
            set => _companyFontSize = (value.EndsWith("px") ? value : value + "px");
        }
        [FirestoreProperty("sloganNameFontSize")]
        public string SloganFontSize
        {
            get => string.IsNullOrEmpty(_sloganFontSize) ? Defaults.DefaultSloganFontSize : _sloganFontSize;
            set => _sloganFontSize = (value.EndsWith("px") ? value : value + "px");
        }
        [FirestoreProperty("primaryFontSize")]
        public string PrimaryFontSize
        {
            get => string.IsNullOrEmpty(_primaryFontSize) ? Defaults.DefaultPrimaryFontSize : _primaryFontSize;
            set => _primaryFontSize = value;
        }
        [FirestoreProperty("secondaryFontSize")]
        public string SecondaryFontSize
        {
            get => string.IsNullOrEmpty(_secondaryFontSize) ? Defaults.DefaultSecondaryFontSize : _secondaryFontSize;
            set => _secondaryFontSize = value;
        }
        [FirestoreProperty("themeBackgroundImage")]
        public string ThemeBackgroundImage { get; set; }

        [Url(ErrorMessage = "Must be a valid url")]
        [FirestoreProperty("socialLinkedUrl")]
        public string SocialLinkedUrl { get; set; }
        [Url(ErrorMessage = "Must be a valid url")]
        [FirestoreProperty("socialFacebookUrl")]
        public string SocialFacebookUrl { get; set; }
        [Url(ErrorMessage = "Must be a valid url")]
        [FirestoreProperty("socialTwitterUrl")]
        public string SocialTwitterUrl { get; set; }
        [Url(ErrorMessage = "Must be a valid url")]
        [FirestoreProperty("socialInstagramUrl")]
        public string SocialInstagramUrl { get; set; }
        [Url(ErrorMessage = "Must be a valid url")]
        [FirestoreProperty("socialYoutubeUrl")]
        public string SocialYoutubeUrl { get; set; }
        [Required(ErrorMessage = "Please select at least one template")]
        [FirestoreProperty("businessCardVersion")]
        public string BusinessCardVersion { get; set; }
        public string BusinessCardBackgroundImage { get; set; }
        //
        public string ProfileImage { get; set; }
        public string  ProfileFullName { get; set; }
        public string ProfileEmail { get; set; }
        public string ProfileFullAddress => $"{this.Address1}, {this.Address2}, {this.City} {this.Province}, {this.PostalCode}";
        public string BusinessCodeUrl => $"{Defaults.DefaultSmartAssUrl}/ViewCard?businessCardId={BusinessCardId}";
    }
}
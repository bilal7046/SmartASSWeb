namespace SmartASSWeb.Bll
{
    public interface IBusinessCardMapper
    {
        BusinessCard Map(BusinessCard toMap, UserProfile profile);
    }
    
    public class BusinessCardMapper
        : IBusinessCardMapper
    {
        public BusinessCard Map(BusinessCard c, UserProfile profile)
        {
            return new BusinessCard
            {
                UserId = c.UserId,
                Email = c.Email,
                SocialInstagramUrl = c.SocialInstagramUrl,
                City = c.City,
                CompanyFontColor = c.CompanyFontColor,
                SocialTwitterUrl = c.SocialTwitterUrl,
                SocialFacebookUrl = c.SocialFacebookUrl,
                Tags = c.Tags,
                Country = c.Country,
                ProductServices = c.ProductServices,
                Name = c.Name,
                Website = c.Website,
                TitleFontSize = c.TitleFontSize,
                Province = c.Province,
                Title = c.Title,
                CompanyFontSize = c.CompanyFontSize,
                Address2 = c.Address2,
                LogoUrl = c.LogoUrl,
                PersonFontSize = c.PersonFontSize,
                SloganFontSize = c.SloganFontSize,
                SocialLinkedUrl = c.SocialLinkedUrl,
                PostalCode = c.PostalCode,
                Mobile = c.Mobile,
                SocialYoutubeUrl = c.SocialYoutubeUrl,
                Slogan = c.Slogan,
                ThemeColor = c.ThemeColor,
                PersonFontColor = c.PersonFontColor,
                JobLevel = c.JobLevel,
                Overview = c.Overview,
                Address1 = c.Address1,
                CompanyType = c.CompanyType,
                MissionStatement = c.MissionStatement,
                ConnectedUsers = c.ConnectedUsers,
                BusinessCardId = c.BusinessCardId,
                Industry = c.Industry,
                Phone1 = c.Phone1,
                Phone2 = c.Phone2,
                Theme = c.Theme,
                ThemeBackgroundImage = c.ThemeBackgroundImage,
                BusinessCardVersion = c.BusinessCardVersion,
                ProfileFullName = (profile == null ? c.ProfileFullName : profile.FullName),
                ProfileEmail = (profile == null ? c.Email : profile.Email),
                ProfileImage = (profile == null ? c.ProfileImage : profile.PhotoUrl)
            };
        }
    }
}

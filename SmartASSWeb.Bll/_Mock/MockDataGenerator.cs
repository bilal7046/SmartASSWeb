using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Faker;
using Google.Cloud.Firestore;
using SmartASSWeb.Bll.Core;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Bll._Mock
{
    public class MockDataGenerator
    {
        private readonly ILookupDictionary _dictionary;
        public const string UserId = "Id7QxCuXIAMIVgBPLw8kjqbMWoJ3";
        public const string BerthaDoe = "PavwfXwA5VWjQU0ghXIw9cS4bms1";
        public const string SimonDoe = "Mpk1RBDOmhP1Z8NQOe52Tva9gTt1";
        public const string PeterDoe = "2tXUL5zdLmY0r4kzUqUzik4p2ma2";
        public const string JuliaDoe = "OMpMVPDK4LP3LusHAInAd6szg6u1";
        public const string DemoUser = "xDwyrHua98dga6Cqg4USzhrkTnO2";
        
        public MockDataGenerator()
        {
            _dictionary = new LookupDictionary();
        }

        public static IEnumerable<string> Users =>
            new List<string>
            {
                BerthaDoe,SimonDoe,PeterDoe,JuliaDoe
            };

        public IEnumerable<Lead> GetLeads(int numberOfLeads, string userId)
        {
            var leads = new List<Lead>();
            for (int i = 0; i < numberOfLeads; i++)
            {
                var lead = new Lead
                {
                    FirstName = Name.First(),
                    LastName = Name.Last(),
                    Email = Internet.Email(),
                    Address1 = Address.StreetAddress(),
                    Address2 = Address.SecondaryAddress(),
                    City = Address.City(),
                    Province = Address.UkCounty(),
                    Country = Address.UkCountry(),
                    PostalCode = Address.UkPostCode(),
                    LeadOwner = "Demo User",
                    Industry = _dictionary.Industries.RandomItem().Value,
                    LeadSource = _dictionary.LeadSources.RandomItem().Value,
                    LeadStatus = _dictionary.LeadStatuses.RandomItem().Value,
                    Mobile = PhoneFaker.Phone(),
                    Phone = PhoneFaker.Phone(),
                    Revenue = RandomNumber.Next(100000, 1000000),
                    Company = Faker.Company.Name(),
                    UserId = userId,
                    Website = Internet.Url(),
                    NoOfEmployees = RandomNumber.Next(1, 500),
                    Notes = Lorem.Paragraph(4),
                    Title = _dictionary.Titles.RandomItem().Value,
                    DateCreated = Timestamp.FromDateTime(RandomDay(2021, endDate: DateTime.Now.AddMonths(2)).ToUniversalTime())
                };
                leads.Add(lead);
            }
            return leads;
        }
        public IEnumerable<Lead> GetLeads(int numberOfLeads, string userId, string leadStatus, DateTime startDate, DateTime endDate)
        {
            var leads = new List<Lead>();
            for (int i = 0; i < numberOfLeads; i++)
            {
                var lead = new Lead
                {
                    FirstName = Name.First(),
                    LastName = Name.Last(),
                    Email = Internet.Email(),
                    Address1 = Address.StreetAddress(),
                    Address2 = Address.SecondaryAddress(),
                    City = Address.City(),
                    Province = Address.UkCounty(),
                    Country = Address.UkCountry(),
                    PostalCode = Address.UkPostCode(),
                    LeadOwner = "Demo User",
                    Industry = _dictionary.Industries.RandomItem().Value,
                    LeadSource = _dictionary.LeadSources.RandomItem().Value,
                    LeadStatus = leadStatus,
                    Mobile = Faker.PhoneFaker.Phone(),
                    Phone = PhoneFaker.Phone(),
                    Revenue = RandomNumber.Next(100000, 1000000),
                    Company = Faker.Company.Name(),
                    UserId = userId,
                    Website = Internet.Url(),
                    NoOfEmployees = RandomNumber.Next(1, 500),
                    Notes = Lorem.Paragraph(4),
                    Title = _dictionary.Titles.RandomItem().Value,
                    DateCreated = Timestamp.FromDateTime(RandomDay(2021, endDate: DateTime.Now.AddMonths(2)).ToUniversalTime())
                };
                leads.Add(lead);
            }
            return leads;
        }
        public List<BusinessCard> GetBusinessCards(string userId, int numberOfBusinessCards)
        {
            var businessCards = new List<BusinessCard>();
            for (int i = 0; i < numberOfBusinessCards; i++)
            {
                var businessCard = GetBusinessCard(userId, new string[]{});
                businessCards.Add(businessCard);
            }
            return businessCards;
        }

        public BusinessCard GetBusinessCard(string userId, string[] connectedUsers, string companyId = "")
        {
            var company = new BusinessCard
            {
                BusinessCardId = companyId,
                Name = Faker.Company.Name(),
                Slogan = Faker.Company.CatchPhrase(),
                Phone1 = Faker.Phone.Number(),
                Mobile = Faker.Phone.Number(),
                Address1 = Address.StreetAddress(true),
                City = Address.City(),
                PostalCode = Address.ZipCode(),
                Province = Address.UsState(),
                Country = Address.Country(),
                CompanyFontColor = Defaults.DefaultCompanyFontColor,
                CompanyFontSize = Defaults.DefaultCompanyFontSize,
                ThemeColor = Defaults.DefaultCompanyThemeColor,
                TitleFontSize = Defaults.DefaultTitleFontSize,
                LogoUrl = Defaults.DefaultCompanyImage,
                CompanyType = _dictionary.CompanyTypes.RandomItem().Value,
                JobLevel = _dictionary.JobLevels.RandomItem().Value,
                Email = Internet.Email(),
                UserId = userId,
                Overview = Lorem.Paragraph(),
                Industry = _dictionary.Industries.RandomItem().Value,
                PersonFontColor = Defaults.DefaultPersonFontColor,
                PersonFontSize = Defaults.DefaultPersonFontSize,
                ProductServices = new string[] { "Pencils", "Books", "Paper" },
                Tags = new[] { "Paper Supplier", "Wood", "Office", "Education" },
                Title = _dictionary.Titles.RandomItem().Value,
                BusinessCardVersion = _dictionary.BusinessCardVersions.RandomItem().Value,
                Website = Internet.Url(),
                SocialInstagramUrl = "https://www.instagram.com",
                SocialTwitterUrl = "https://www.twitter.com",
                SocialFacebookUrl = "https://www.facebook.com",
                SocialLinkedUrl = "https://www.linkedin.com",
                SocialYoutubeUrl = "https://www.youtube.com",
                SloganFontSize = Defaults.DefaultSloganFontSize,
                MissionStatement = Lorem.Sentence(),
                ConnectedUsers = connectedUsers
            };
            return company;
        }

        public async Task<IEnumerable<Activity>> GetActivities(int numberOfActivities, string userId, EnumActivity activityType)
        {
            IFirebaseService service = new FirebaseService(new KeyFileResolver());
            var creator = await service.Get<UserProfile>(FirestoreTableStore.UserProfiles, userId);
            var businessCards = await service.GetCollection<BusinessCard>(FirestoreTableStore.BusinessCards, "userId", userId);
            var activities = new List<Activity>();
            foreach (var businessCard in businessCards)
            {
                for (int i = 0; i < numberOfActivities; i++)
                {
                    var activity = new Activity
                    {
                        ActivityCreator = creator.UserId,
                        ActivityCreatorImage = creator.PhotoUrl,
                        ActivityCreatorName = creator.FullName,
                        ActivityTime = Timestamp.FromDateTime(new DateTime(2021, 4, RandomNumber.Next(1, 30)).ToUniversalTime()),
                        ActivityMessage = $"Test Data Information from '{creator.FirstName} {creator.LastName}'",
                        ActivityOwner = DemoUser,
                        ActivityType = activityType.ToString(),
                        ActivityBusiness = businessCard.BusinessCardId
                    };
                    activities.Add(activity);
                }
            }

            return activities;
        }
        public async Task<IEnumerable<Notification>> GetNotifications(int numberOfNotifications, string userId, string requestorId, EnumNotificationType notificationType, string message)
        {
            IFirebaseService service = new FirebaseService(new KeyFileResolver());
            var requestor = await service.Get<UserProfile>(FirestoreTableStore.UserProfiles, requestorId);
            var notifications = new List<Notification>();
            for (int i = 0; i < numberOfNotifications; i++)
            {
                var notification = new Notification
                {
                    UserId = userId,
                    Message = (string.IsNullOrEmpty(message)) ? Faker.Lorem.Sentence(7) : message,
                    DateCreated = Timestamp.GetCurrentTimestamp(),
                    IsRead = false,
                    NotificationType = notificationType.ToString(),
                    RequestorName = requestor.FullName,
                    RequestorPhotoUrl = requestor.PhotoUrl,
                    RequestorUserId = requestor.UserId
                };
                notifications.Add(notification);
            }

            return notifications;
        }

        #region GetUserProfile

        public UserProfile GetUserProfile(string userId, string firstName, string lastName, string gender, string jobLevel, string designation, string[] linkedUsers)
        {
            return new UserProfile
            {
                UserId = userId,
                FirstName = firstName,
                LastName = lastName,
                Email = $"{firstName}.{lastName}@SmartAss.com",
                PhotoUrl = Defaults.DefaultProfileImage,
                DateOfBirth = new Timestamp(),
                AddressLine1 = Faker.Address.StreetAddress(),
                AddressLine2 = Faker.Address.SecondaryAddress(),
                City = Faker.Address.City(),
                Country = "South Africa",
                Designation = designation,
                Phone = Faker.Phone.Number(),
                Mobile = Faker.Phone.Number(),
                PostalCode = Faker.Address.UkPostCode(),
                Website = Faker.Internet.Url(),
                Gender = gender,
                LinkedUsers = linkedUsers
            };
        }
        public UserProfile GetUserProfile(string userId, string email, string firstName, string lastName, string gender, string jobLevel, string designation, string[] linkedUsers)
        {
            return new UserProfile
            {
                UserId = userId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhotoUrl = Defaults.DefaultProfileImage,
                DateOfBirth = new Timestamp(),
                AddressLine1 = Faker.Address.StreetAddress(),
                AddressLine2 = Faker.Address.SecondaryAddress(),
                City = Faker.Address.City(),
                Country = "South Africa",
                Designation = designation,
                Phone = Faker.Phone.Number(),
                Mobile = Faker.Phone.Number(),
                PostalCode = Faker.Address.UkPostCode(),
                Website = Faker.Internet.Url(),
                Gender = gender,
                LinkedUsers = linkedUsers
            };
        }

        #endregion

        #region CalendarEvents

        public CalendarEvent GetCalendarEvent()
        {
            return new CalendarEvent
            {
                UserId = UserId,
                Title = Faker.Lorem.Words(3).ToString(),
                //Start = 
            };
        }

        #endregion

        public DateTime RandomDay(int year)
        {
            Random _gen = new Random();
            var start = new DateTime(year, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_gen.Next(range));
        }
        public DateTime RandomDay(int year, DateTime endDate)
        {
            Random _gen = new Random();
            var start = new DateTime(year, 1, 1);
            int range = (endDate - start).Days;
            return start.AddDays(_gen.Next(range));
        }
        public DateTime RandomDay(DateTime startDate, DateTime endDate)
        {
            Random _gen = new Random();
            var start = startDate;
            int range = (endDate - start).Days;
            return start.AddDays(_gen.Next(range));
        }
        public DateTime RandomDayThisMonth(int year, int month)
        {
            Random _gen = new Random();
            var start = new DateTime(year, month, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_gen.Next(range));
        }
    }
}

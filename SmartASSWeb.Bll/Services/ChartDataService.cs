using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using SmartASSWeb.Bll._Mock;
using SmartASSWeb.Bll.Core;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Bll.Services
{
    public interface IBusinessCardChartDataService
    {
        Task<List<ChartData>> GetNumberOfCardsSent(string businessId, DateTime startDate, DateTime endDate);
        Task<List<ChartData>> GetAcceptedInvites(string businessId, DateTime startDate, DateTime endDate);
        Task<List<ChartData>> GetPendingCards(string businessId, DateTime startDate, DateTime endDate);
        Task<List<ChartData>> GetInvitesReceived(string businessId, DateTime startDate, DateTime endDate);
        Task<List<ChartData>> GetPeopleSharedMyCards(string businessId, DateTime startDate, DateTime endDate);
        Task<List<DonutData>> GetSocialMediaLinks(string businessId);
        Task<List<ChartData>> GetAgeClicks(string businessId, string currentUserId);
        Task<List<ChartData3>> GetGenderClicks(string businessId, DateTime startDate, DateTime endDate);
        Task<List<ChartData>> GetJobLevelClicks(string businessCardId, string userId);
        Task<List<ChartData>> GetEngagementRates(string businessId, DateTime startDate, DateTime endDate);
    }

    public class BusinessCardChartDataService
        : IBusinessCardChartDataService
    {
        #region Ctor

        private readonly IFirebaseService _service;
        private readonly IActivityService _activityService;

        public BusinessCardChartDataService(IFirebaseService service, IActivityService activityService)
        {
            _service = service;
            _activityService = activityService;
        }

        #endregion

        public async Task<List<ChartData>> GetNumberOfCardsSent(string businessId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var cardsSentActivities = await _activityService.GetActivities(businessId, EnumActivity.CardSharedInternal, Timestamp.FromDateTime(startDate.ToUniversalTime()), Timestamp.FromDateTime(endDate.ToUniversalTime()));
                
                var data = cardsSentActivities.OrderBy(c => c.ActivityTime)
                    .GroupBy(p => p.ActivityTime.ToDateTime().ToString("dd/MM/yyyy"))
                    .Select(p => new ChartData
                    {
                        Key = p.Key,
                        Value = p.Count()
                    }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ChartData>> GetAcceptedInvites(string businessId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var acceptedInvites = await _activityService.GetActivities(businessId, EnumActivity.RequestAccepted, Timestamp.FromDateTime(startDate.ToUniversalTime()), Timestamp.FromDateTime(endDate.ToUniversalTime()));
                var data = acceptedInvites.OrderBy(c => c.ActivityTime)
                    .GroupBy(p => p.ActivityTime.ToDateTime().ToString("dd/MM/yyyy"))
                    .Select(p => new ChartData
                    {
                        Key = p.Key,
                        Value = p.Count()
                    }).ToList();
            
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cards I have sent out but have not been accepted by users
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<ChartData>> GetPendingCards(string businessId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var internalActivities = await _activityService.GetActivities(businessId, EnumActivity.CardSharedInternal, Timestamp.FromDateTime(startDate.ToUniversalTime()), Timestamp.FromDateTime(endDate.ToUniversalTime()));
                var businessCard = await _service.Get<BusinessCard>(FirestoreTableStore.BusinessCards, businessId);

                var usersThatHaveNotResponded = internalActivities.Where(p => !businessCard.ConnectedUsers.Contains(p.ActivityCreator)).ToList();

                var data = usersThatHaveNotResponded.OrderBy(c => c.ActivityTime)
                    .GroupBy(p => p.ActivityTime.ToDateTime().ToString("dd/MM/yyyy"))
                    .Select(p => new ChartData
                    {
                        Key = p.Key,
                        Value = p.Count()
                    }).ToList();
            
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Invites I have received from other users
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<ChartData>> GetInvitesReceived(string businessId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var activities = await _activityService.GetActivities(businessId, EnumActivity.RequestToConnect, Timestamp.FromDateTime(startDate.ToUniversalTime()), Timestamp.FromDateTime(endDate.ToUniversalTime()));

                var data = activities.OrderBy(c => c.ActivityTime)
                    .GroupBy(p => p.ActivityTime.ToDateTime().ToString("dd/MM/yyyy"))
                    .Select(p => new ChartData
                    {
                        Key = p.Key,
                        Value = p.Count()
                    }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<ChartData>> GetPeopleSharedMyCards(string businessId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var cardsSharedOnBehalfActivities = await _activityService.GetActivities(businessId, EnumActivity.CardSharedExternal, Timestamp.FromDateTime(startDate.ToUniversalTime()), Timestamp.FromDateTime(endDate.ToUniversalTime()));

                var data = cardsSharedOnBehalfActivities.OrderBy(c=> c.ActivityTime).Where(p=> p.ActivityCreator != businessId)
                    .GroupBy(p => p.ActivityTime.ToDateTime().ToString("dd/MM/yyyy"))
                    .Select(p => new ChartData
                    {
                        Key = p.Key,
                        Value = p.Count()
                    }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<ChartData>> GetEngagementRates(string businessId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var cardsSharedOnBehalfActivities = await _activityService.GetActivities(businessId, EnumActivity.CardSharedExternal, Timestamp.FromDateTime(startDate.ToUniversalTime()), Timestamp.FromDateTime(endDate.ToUniversalTime()));

                var data = cardsSharedOnBehalfActivities.OrderBy(c=> c.ActivityTime).Where(p=> p.ActivityCreator != businessId)
                    .GroupBy(p => p.ActivityTime.ToDateTime().ToString("dd/MM/yyyy"))
                    .Select(p => new ChartData
                    {
                        Key = p.Key,
                        Value = p.Count()
                    }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DonutData>> GetSocialMediaLinks(string businessId)
        {
            try
            {
                var emailClicks = await _activityService.GetActivities(businessId, EnumActivity.EmailClick);
                var websiteClicks = await _activityService.GetActivities(businessId, EnumActivity.WebsiteClick);
                var linkedInClicks = await _activityService.GetActivities(businessId, EnumActivity.LinkedInClick);
                var twitterClicks = await _activityService.GetActivities(businessId, EnumActivity.TwitterClick);
                var facebookClicks = await _activityService.GetActivities(businessId, EnumActivity.FacebookClick);
                var instagramClicks = await _activityService.GetActivities(businessId, EnumActivity.InstagramClick);
                var youtubeClicks = await _activityService.GetActivities(businessId, EnumActivity.YouTubeClick);
            
                var col = new List<DonutData>
                {
                    new DonutData("Email", emailClicks.Count()),
                    new DonutData("Website", websiteClicks.Count()),
                    new DonutData("Linked-In", linkedInClicks.Count()),
                    new DonutData("Facebook", facebookClicks.Count()),
                    new DonutData("Twitter", twitterClicks.Count()),
                    new DonutData("Instagram", instagramClicks.Count()),
                    new DonutData("YouTube", youtubeClicks.Count())
                };
                return col;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ChartData>> GetAgeClicks(string businessId, string currentUserId)
        {
            try
            {
                if (currentUserId == MockDataGenerator.DemoUser)
                {
                    return new List<ChartData>
                    {
                        new ChartData("18-24", 13),
                        new ChartData("25-34", 22),
                        new ChartData("35-44", 9),
                        new ChartData("45-54", 17),
                        new ChartData("55-64", 4)
                    };
                }
                var activities = await _activityService.GetActivities(businessId);
                var activitiesByInternalPeople = activities.Where(p => !string.IsNullOrEmpty(p.ActivityCreator)).ToList();
                var profiles = new List<UserProfile>();
                
                foreach (var activityCreatorId in activitiesByInternalPeople.Select(c=>c.ActivityCreator).Distinct())
                {
                    var user = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, activityCreatorId);
                    profiles.Add(user);
                }
                var col = new List<ChartData>
                {
                    new ChartData("18-24", GetAgeRangeCount(profiles, 18, 24)),
                    new ChartData("25-34", GetAgeRangeCount(profiles, 25,34)),
                    new ChartData("35-44", GetAgeRangeCount(profiles, 35, 44)),
                    new ChartData("45-54", GetAgeRangeCount(profiles, 45, 54)),
                    new ChartData("55-64", GetAgeRangeCount(profiles, 55,64))
                };
                return col;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int GetAgeRangeCount(List<UserProfile> profiles, int startAge, int endAge)
        {
            return profiles.Count(p => GetAge(p.DateOfBirth.GetValueOrDefault().ToDateTime()) >= startAge && GetAge(p.DateOfBirth.GetValueOrDefault().ToDateTime()) <= endAge);
        }

        private int GetAge(DateTime birthdate)
        {
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - birthdate.Year;

            // Go back to the year in which the person was born in case of a leap year
            if (birthdate.Date > today.AddYears(-age)) age--;

            return age;
        }

        public async Task<List<ChartData3>> GetGenderClicks(string businessId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var activities = await _activityService.GetActivities(businessId, Timestamp.FromDateTime(startDate.ToUniversalTime()), Timestamp.FromDateTime(endDate.ToUniversalTime()));
                var activitiesByInternalPeople = activities.Where(p => !string.IsNullOrEmpty(p.ActivityCreator)).ToList();

                var profiles = new List<UserProfile>();
                foreach (var activityCreatorId in activitiesByInternalPeople.Select(c => c.ActivityCreator).Distinct())
                {
                    var user = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, activityCreatorId);
                    profiles.Add(user);
                }

                var maleActivities = (from a in activitiesByInternalPeople
                    join p in profiles on a.ActivityCreator equals p.UserId
                    where p.Gender == EnumGender.Male.ToString()
                    select new
                    {
                        Date = a.ActivityTime.ToDateTime().ToString("dd/MM/yyyy"),
                        Gender = p.Gender,
                        UserId = p.UserId
                    }).ToList();
                var femaleActivities = (from a in activitiesByInternalPeople
                    join p in profiles on a.ActivityCreator equals p.UserId
                    where p.Gender == EnumGender.Female.ToString()
                    select new
                    {
                        Date = a.ActivityTime.ToDateTime().ToString("dd/MM/yyyy"),
                        Gender = p.Gender,
                        UserId = p.UserId
                    }).ToList();
                var otherActivities = (from a in activitiesByInternalPeople
                    join p in profiles on a.ActivityCreator equals p.UserId
                    where p.Gender == EnumGender.Other.ToString()
                    select new
                    {
                        Date = a.ActivityTime.ToDateTime().ToString("dd/MM/yyyy"),
                        Gender = p.Gender,
                        UserId = p.UserId
                    }).ToList();
                var allGenderActivities = maleActivities.Concat(femaleActivities).Concat(otherActivities);
                var allGenderActivitiesGrouped= (from activity in allGenderActivities
                    group activity by new
                    {
                        activity.Date,
                        activity.Gender,
                        activity.UserId
                    } into stdGroup
                    select new
                    {
                        Date = stdGroup.Key.Date,
                        Gender = stdGroup.Key.Gender,
                        UserId = stdGroup.Key.UserId,
                        Count = stdGroup.Count()
                    }).ToList();


                var data = allGenderActivitiesGrouped
                    .GroupBy(p => p.Date)
                    .Select(p => new ChartData3
                    {
                        Date = p.Key,
                        Value = p.Count(c=> c.Gender == EnumGender.Male.ToString()),
                        Value2 = p.Count(c => c.Gender == EnumGender.Female.ToString()),
                        Value3 = p.Count(c => c.Gender == EnumGender.Other.ToString()),
                    }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ChartData>> GetJobLevelClicks(string businessCardId, string currentUserId)
        {
            try
            {
                if (currentUserId == MockDataGenerator.DemoUser)
                {
                    return new List<ChartData>
                    {
                        new ChartData(EnumJobLevel.EntryLevel.GetDescription(), 12),
                        new ChartData(EnumJobLevel.Intermediate.GetDescription(), 6),
                        new ChartData(EnumJobLevel.MidLevel.GetDescription(), 11),
                        new ChartData(EnumJobLevel.Senior.GetDescription(), 28),
                        new ChartData(EnumJobLevel.Executive.GetDescription(), 3)
                    };
                }
                var activities = await _activityService.GetActivities(businessCardId);
                var activitiesByInternalPeople = activities.Where(p => !string.IsNullOrEmpty(p.ActivityCreator)).ToList();
                var usersThatActioned = new List<UserProfile>();
                var userIdsThatActioned = activitiesByInternalPeople.Select(c => c.ActivityCreator).Distinct();//2-
                foreach (var activityCreatorId in userIdsThatActioned)
                {
                    var user = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, activityCreatorId);
                    usersThatActioned.Add(user);
                }

                //After getting who clicked, based on their job level, determine the number of times they clicked 
                foreach (var jobLevel in Enum.GetValues(typeof(EnumJobLevel)).Cast<EnumJobLevel>())
                {
                    //Get User in JobLevel
                    /*foreach (var userProfile in usersThatActioned.Where(c=> c.JobLevel))
                    {
                        
                    }*/
                }
                var col = new List<ChartData>
                {
                    // new ChartData(EnumJobLevel.EntryLevel.GetDescription(), usersThatActioned.Count(p=> p.JobLevel == EnumJobLevel.EntryLevel.ToString())),
                    // new ChartData(EnumJobLevel.Intermediate.GetDescription(), usersThatActioned.Count(p=> p.JobLevel == EnumJobLevel.Intermediate.ToString())),
                    // new ChartData(EnumJobLevel.MidLevel.GetDescription(), usersThatActioned.Count(p=> p.JobLevel == EnumJobLevel.MidLevel.ToString())),
                    // new ChartData(EnumJobLevel.Senior.GetDescription(), usersThatActioned.Count(p=> p.JobLevel == EnumJobLevel.Senior.ToString())),
                    // new ChartData(EnumJobLevel.Executive.GetDescription(), usersThatActioned.Count(p=> p.JobLevel == EnumJobLevel.Executive.ToString()))
                };
                return col;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
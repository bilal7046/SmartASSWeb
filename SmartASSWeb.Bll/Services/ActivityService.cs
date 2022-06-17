using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Bll.Services
{
    public interface IActivityService
    {
        Task<IEnumerable<Activity>> GetActivities(string businessId);
        Task<IEnumerable<Activity>> GetActivities(string businessId, Timestamp start, Timestamp end);
        Task<IEnumerable<Activity>> GetActivities(string businessId, EnumActivity activityType);
        Task<IEnumerable<Activity>> GetActivities(string businessId, EnumActivity activityType, Timestamp start, Timestamp end);
        Task LogActivity(Activity activity);
        Task<Response> LogActivity(string activityOwner, string activityCreator, string activityType, string activityMessage);
        Task<Response> LogActivity(string businessId, string activityOwner, string activityCreator, string activityType, string activityMessage);
    }
    public class ActivityService
        : IActivityService
    {
        private readonly IFirebaseService _service;

        public ActivityService(IFirebaseService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<Activity>> GetActivities(string businessId)
        {
            try
            {
                var activities = await _service.GetCollection<Activity>(FirestoreTableStore.Activities, "activityBusiness", businessId);
                return activities.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IEnumerable<Activity>> GetActivitiesByBusinessId(string businessId)
        {
            try
            {
                var activities = await _service.GetCollection<Activity>(FirestoreTableStore.Activities, "activityBusiness", businessId);
                return activities.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Activity>> GetActivities(string businessId, EnumActivity activityType)
        {
            try
            {
                var activities = await _service.GetCollection<Activity>(FirestoreTableStore.Activities, "activityBusiness", businessId);
                return activities.Where(p => p.ActivityType == activityType.ToString()).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Activity>> GetActivities(string businessId, Timestamp start, Timestamp end)
        {
            try
            {
                var activities = await _service.GetCollection<Activity>(FirestoreTableStore.Activities, "activityBusiness", businessId);
                var col = activities
                                            .Where(p => p.ActivityTime >= start && p.ActivityTime <= end)
                                            .ToList();
                return col;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Activity>> GetActivities(string businessId, EnumActivity activityType, Timestamp start, Timestamp end)
        {
            try
            {
                var activities = await _service.GetCollection<Activity>(FirestoreTableStore.Activities, "activityBusiness", businessId);
                var col = activities
                                            .Where(p => p.ActivityType == activityType.ToString())
                                            .Where(p => p.ActivityTime >= start && p.ActivityTime <= end)
                                            .ToList();
                return col;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task LogActivity(Activity activity)
        {
            try
            {
                activity.ActivityTime = Timestamp.GetCurrentTimestamp();
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Response> LogActivity(string activityOwner, string activityCreator, string activityType, string activityMessage)
        {
            try
            {
                var activity = new Activity
                {
                    ActivityOwner = activityOwner,
                    ActivityCreator = activityCreator,
                    ActivityType = activityType,
                    ActivityMessage = activityMessage,
                    ActivityTime = Timestamp.GetCurrentTimestamp()
                };
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);

                return new Response
                {
                    IsSuccessful = true,
                    Message = "Logged"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Response> LogActivity(string businessId, string activityOwner, string activityCreator, string activityType, string activityMessage)
        {
            try
            {
                var activity = new Activity
                {
                    ActivityOwner = activityOwner,
                    ActivityCreator = activityCreator,
                    ActivityType = activityType,
                    ActivityMessage = activityMessage,
                    ActivityTime = Timestamp.GetCurrentTimestamp(),
                    ActivityBusiness = businessId
                };
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);

                return new Response
                {
                    IsSuccessful = true,
                    Message = "Logged"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

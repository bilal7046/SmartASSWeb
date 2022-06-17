using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Google.Cloud.Firestore;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Core;
using SmartASSWeb.Bll.Services;
using SmartASSWeb.Core.Service;
using SmartASSWeb.ViewModels;

namespace SmartASSWeb.Controllers
{
    [SmartAssAuthorize(Roles = Common.INDIVIDUAL)]
    public class UserProfileController 
        : ControllerBase
    {
        private readonly IFirebaseService _service;
        private readonly IActivityService _activityService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="activityService"></param>
        public UserProfileController(IFirebaseService service, IActivityService activityService)
        {
            _service = service;
            _activityService = activityService;
        }
        public async Task<ActionResult> Index()
        {
            var profile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, User.UserId);
            var userAction = await _service.Get<UserAction>(FirestoreTableStore.UserActions, "userId", User.UserId);
            var model = new ProfileViewModel
            {
                Profile = profile,
                TotalContacts = profile.LinkedUsers.Length,
                RequestCount = userAction?.RequestCount ?? default(int),

            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> Card(string id)
        {
            var activity = new Activity
            {
                ActivityTime = Timestamp.GetCurrentTimestamp(),
                ActivityType = EnumActivity.ProfileClick.ToString(),
                ActivityCreator = User?.UserId,
                ActivityOwner = id
            };
            await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
            //
            var obj = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, "userId", id);
            return View(obj);
        }

        public async Task<ActionResult> Manage()
        {
            var profile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, "userId", User.UserId);
            return View(new ProfileViewModel
            {
                Profile = profile
            });
        }

        [HttpPost]
        public async Task<ActionResult> SaveProfile(UserProfile profile)
        {
            if (profile.DateOfBirthHolder != null)
            {
                DateTime universalTime = profile.DateOfBirthHolder.GetValueOrDefault().ToUniversalTime();
                var dob = Timestamp.FromDateTime(universalTime);
                await _service.Update(profile.UserId, FirestoreTableStore.UserProfiles, "dateOfBirth", dob);
            }
            await _service.Update(profile.UserId, FirestoreTableStore.UserProfiles, profile.ToDictionary(false));

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<JsonResult> LogActivity()
        {
            var obj = new Activity
            {
                ActivityTime = Timestamp.GetCurrentTimestamp(),
                ActivityId = Guid.NewGuid().ToString(),

            };
            await _activityService.LogActivity(obj);
            return Json("Saved");
        }

        #region LogActivity

        [HttpPost]
        public async Task<JsonResult> ShareClick(string id)
        {
            await Log(id, EnumActivity.ShareClick);

            return Json("Successful");
        }
        [HttpPost]
        public async Task<JsonResult> EmailClick(string id)
        {
            await Log(id, EnumActivity.EmailClick);

            return Json("Successful");
        }
        [HttpPost]
        public async Task<JsonResult> WebsiteClick(string id)
        {
            await Log(id, EnumActivity.WebsiteClick);

            return Json("Successful");
        }
        [HttpPost]
        public async Task<JsonResult> LinkedInClick(string id)
        {
            await Log(id, EnumActivity.LinkedInClick);

            return Json("Successful");
        }
        [HttpPost]
        public async Task<JsonResult> TwitterClick(string id)
        {
            await Log(id, EnumActivity.TwitterClick);

            return Json("Successful");
        }
        [HttpPost]
        public async Task<JsonResult> InstagramClick(string id)
        {
            await Log(id, EnumActivity.InstagramClick);

            return Json("Successful");
        }
        [HttpPost]
        public async Task<JsonResult> FacebookClick(string id)
        {
            await Log(id, EnumActivity.FacebookClick);

            return Json("Successful");
        }

        private async Task Log(string id, EnumActivity activityType)
        {
            var activity = new Activity
            {
                ActivityTime = Timestamp.GetCurrentTimestamp(),
                ActivityType = activityType.ToString(),
                ActivityCreator = User?.UserId,
                ActivityOwner = id
            };
            await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
        }

        #endregion
    }
}
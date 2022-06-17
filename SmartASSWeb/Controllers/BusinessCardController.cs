using System.Threading.Tasks;
using System.Web.Mvc;
using Google.Cloud.Firestore;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Services;
using SmartASSWeb.Core.Service;
using SmartASSWeb.ViewModels;

namespace SmartASSWeb.Controllers
{
    public class BusinessCardController
        : ControllerBase
    {
        #region Ctor

        private readonly IFirebaseService _service;
        private readonly IActivityService _activityService;
        private readonly IBusinessCardMapper _businessCardMapper;

        public BusinessCardController(IFirebaseService service, IActivityService activityService, IBusinessCardMapper businessCardMapper)
        {
            _service = service;
            _activityService = activityService;
            _businessCardMapper = businessCardMapper;
        }

        #endregion
        
        [AllowAnonymous]
        public async Task<ActionResult> ViewCard(string businessCardId)
        {
            var businessCard = await _service.Get<BusinessCard>(FirestoreTableStore.BusinessCards, businessCardId);
            var profile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, businessCard.UserId);
            //
            var activity = new Activity
            {
                ActivityType = EnumActivity.ProfileClick.ToString(),
                ActivityCreator = User?.UserId,
                ActivityOwner = businessCard.UserId,
                ActivityBusiness = businessCard.BusinessCardId,
            };
            await _activityService.LogActivity(activity);
            //
            var model = new MyCardsViewModel
            {
                CurrentBusinessCard = _businessCardMapper.Map(businessCard, profile),
                CurrentUserProfile = profile
            };
            return View(model);
        }

        #region Social Media Clicks

        [HttpPost]
        public async Task<JsonResult> ShareClick(string userId, string businessCardId)
        {
            await Log(userId, businessCardId, EnumActivity.ShareClick);

            return Json("Successful");
        }
        [HttpPost]
        public async Task<JsonResult> EmailClick(string userId, string businessCardId)
        {
            await Log(userId, businessCardId, EnumActivity.EmailClick);

            return Json("Successful");
        }
        [HttpPost]
        public async Task<JsonResult> WebsiteClick(string userId, string businessCardId)
        {
            await Log(userId, businessCardId, EnumActivity.WebsiteClick);

            return Json("Successful");
        }
        [HttpPost]
        public async Task<JsonResult> LinkedInClick(string userId, string businessCardId)
        {
            await Log(userId, businessCardId, EnumActivity.LinkedInClick);

            return Json("Successful");
        }
        [HttpPost]
        public async Task<JsonResult> TwitterClick(string userId, string businessCardId)
        {
            await Log(userId, businessCardId, EnumActivity.TwitterClick);

            return Json("Successful");
        }
        [HttpPost]
        public async Task<JsonResult> InstagramClick(string userId, string businessCardId)
        {
            await Log(userId, businessCardId, EnumActivity.InstagramClick);

            return Json("Successful");
        }
        [HttpPost]
        public async Task<JsonResult> FacebookClick(string userId, string businessCardId)
        {
            await Log(userId, businessCardId, EnumActivity.FacebookClick);

            return Json("Successful");
        }
        [HttpPost]
        public async Task<JsonResult> YouTubeClick(string userId, string businessCardId)
        {
            await Log(userId, businessCardId, EnumActivity.YouTubeClick);

            return Json("Successful");
        }

        #endregion

        #region HelperMethods

        private async Task Log(string userId, string companyId, EnumActivity activityType)
        {
            var activity = new Activity
            {
                ActivityTime = Timestamp.GetCurrentTimestamp(),
                ActivityType = activityType.ToString(),
                ActivityCreator = User?.UserId,
                ActivityOwner = userId,
                ActivityBusiness = companyId
            };
            await _activityService.LogActivity(activity);
        }

        #endregion
    }
}
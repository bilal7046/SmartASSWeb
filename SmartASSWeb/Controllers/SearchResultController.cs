using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Google.Cloud.Firestore;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Services;
using SmartASSWeb.Core.Service;
using SmartASSWeb.ViewModels;

namespace SmartASSWeb.Controllers
{
    public class SearchResultController : ControllerBase
    {
        private readonly IFirebaseService _service;
        private readonly INotificationService _notificationService;
        private readonly IActivityService _activityService;

        public SearchResultController(IFirebaseService service, INotificationService notificationService, IActivityService activityService)
        {
            _service = service;
            _notificationService = notificationService;
            _activityService = activityService;
        }

        [HttpPost]
        public async Task<ActionResult> Find(SearchResultViewModel model)
        {
            if (string.IsNullOrEmpty(model.SearchText)) return View(model);
            var userProfiles = await _service.GetCollection<UserProfile>(FirestoreTableStore.UserProfiles);
            
            model.CurrentUserProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, "userId", User.UserId);

            model.UserProfiles = userProfiles.Where(p => (!string.IsNullOrEmpty(p.FirstName) && p.FirstName.ToLower().Contains(model.SearchText.ToLower()))
                                                         || (!string.IsNullOrEmpty(p.LastName) && p.LastName.ToLower().Contains(model.SearchText.ToLower()))
            ).Where(c=> c.UserId != User.UserId).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> SendRequest(string requestedUserId)
        {
            await _notificationService.SendAssociateRequestNotification(User.UserId, requestedUserId);

            var activity = new Activity
            {
                ActivityTime = Timestamp.GetCurrentTimestamp(),
                ActivityType = EnumActivity.RequestToConnect.ToString(),
                ActivityCreator = User.UserId,
                ActivityOwner = requestedUserId,
                ActivityMessage = "Invite Requested"
            };
            await _activityService.LogActivity(activity);

            return Json("OK");
        }
    }
}
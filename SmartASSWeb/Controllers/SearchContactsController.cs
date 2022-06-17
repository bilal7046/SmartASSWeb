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
    public class SearchContactsController 
        : ControllerBase
    {
        private readonly IFirebaseService _firebaseService;
        private readonly INotificationService _notificationService;
        private readonly IActivityService _activityService;

        public SearchContactsController(IFirebaseService firebaseService, INotificationService notificationService, IActivityService activityService)
        {
            _firebaseService = firebaseService;
            _notificationService = notificationService;
            _activityService = activityService;
        }
        // GET: SearchContacts
        public ActionResult Index()
        {
            return View(new SearchContactsViewModel());
        }

        [HttpPost]
        public async Task<PartialViewResult> Search(string searchText)
        {
            var model = new SearchContactsViewModel();
            if (string.IsNullOrEmpty(searchText)) return PartialView("SearchPanel", model);
            var userProfiles = await _firebaseService.GetCollection<UserProfile>(FirestoreTableStore.UserProfiles);

            model.SearchText = searchText;

            model.CurrentUserProfile = await _firebaseService.Get<UserProfile>(FirestoreTableStore.UserProfiles, "userId", User.UserId);

            model.UserProfiles = userProfiles.Where(p => (!string.IsNullOrEmpty(p.FirstName) && p.FirstName.ToLower().Contains(searchText.ToLower()))
                                                         || (!string.IsNullOrEmpty(p.LastName) && p.LastName.ToLower().Contains(searchText.ToLower()))
                                                                ).ToList();
            
            return PartialView("SearchPanel", model);
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
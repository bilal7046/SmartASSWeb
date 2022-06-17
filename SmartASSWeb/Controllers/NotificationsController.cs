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
    [SmartAssAuthorize(Roles = Common.INDIVIDUAL)]
    public class NotificationsController 
        : ControllerBase
    {
        private readonly IFirebaseService _firebaseService;
        private readonly INotificationService _notificationService;
        private readonly IActivityService _activityService;

        public NotificationsController(IFirebaseService firebaseService, INotificationService notificationService, IActivityService activityService)
        {
            _firebaseService = firebaseService;
            _notificationService = notificationService;
            _activityService = activityService;
        }

        // GET: Notifications
        public async Task<ActionResult> Index()
        {
            var notifications = await _firebaseService.GetCollection<Notification>(FirestoreTableStore.Notifications, "userId", User.UserId);
            var model = new NotificationsViewModel
            {
                SearchText = string.Empty,
                Notifications = notifications
            };
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> Search(string searchText)
        {
            var model = new NotificationsViewModel();
            if (string.IsNullOrEmpty(searchText)) return PartialView("NotificationPanel", model);
            var notifications = await _firebaseService.GetCollection<Notification>(FirestoreTableStore.Notifications, "userId", User.UserId);

            model.SearchText = searchText;
            
            model.Notifications = notifications.Where(p => (!string.IsNullOrEmpty(p.Message) && p.Message.ToLower().Contains(searchText.ToLower()))
                                                         || (!string.IsNullOrEmpty(p.RequestorName) && p.RequestorName.ToLower().Contains(searchText.ToLower()))
            ).ToList();

            return PartialView("NotificationPanel", model);
        }

        [HttpPost]
        public async Task<PartialViewResult> Delete(string id)
        {
            await _firebaseService.Delete(id, FirestoreTableStore.Notifications);
            var notifications = await _firebaseService.GetCollection<Notification>(FirestoreTableStore.Notifications, "userId", User.UserId);
            var model = new NotificationsViewModel
            {
                SearchText = string.Empty,
                Notifications = notifications
            };
            return PartialView("NotificationPanel", model);
        }

        [HttpPost]
        public async Task<PartialViewResult> ViewNotification(string id)
        {
            var notification = await _firebaseService.Get<Notification>(FirestoreTableStore.Notifications, "id", id);
            var model = new NotificationsViewModel
            {
                CurrentNotification = notification
            };

            await _firebaseService.Update(id, FirestoreTableStore.Notifications, "isRead", true);
            return PartialView("ViewNotification", model);
        }

        [HttpPost]
        public async Task<JsonResult> LinkUser(string requestUserId)
        {
            var currentUser = await _firebaseService.Get<UserProfile>(FirestoreTableStore.UserProfiles, User.UserId);
            var activity = new Activity
            {
                ActivityTime = Timestamp.GetCurrentTimestamp(),
                ActivityType = EnumActivity.RequestAccepted.ToString(),
                ActivityCreator = User.UserId,
                ActivityOwner = requestUserId,
                ActivityMessage = "Request has been accepted by creator",
                ActivityCreatorImage = currentUser.PhotoUrl
            };
            await _activityService.LogActivity(activity);
            //
            await _notificationService.SendRequestAcceptNotification(requestUserId, User.UserId);
            //
            await _firebaseService.Update(User.UserId, FirestoreTableStore.UserProfiles, "linkedUsers", FieldValue.ArrayUnion(requestUserId));
            return Json("New contact accepted!");
        }
    }
}
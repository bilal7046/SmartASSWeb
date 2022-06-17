using System.Collections.Generic;
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
    public class ConnectedCardsController 
        : ControllerBase
    {
        #region Ctor

        private readonly IFirebaseService _service;
        private readonly IBusinessCardLinkService _businessCardLinkService;
        private readonly INotificationService _notificationService;
        private readonly IActivityService _activityService;
        private readonly IBusinessCardMapper _businessCardMapper;

        public ConnectedCardsController(IFirebaseService service, IBusinessCardLinkService businessCardLinkService, INotificationService notificationService, IActivityService activityService, IBusinessCardMapper businessCardMapper)
        {
            _service = service;
            _businessCardLinkService = businessCardLinkService;
            _notificationService = notificationService;
            _activityService = activityService;
            _businessCardMapper = businessCardMapper;
        }

        #endregion

        public async Task<ActionResult> Index()
        {
            var currentUserProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, User.UserId);
            var businessCards = new List<BusinessCard>();
            foreach (var connectedBusinessCardId in currentUserProfile.ConnectedBusinessCards)
            {
                var businessCard = await _service.Get<BusinessCard>(FirestoreTableStore.BusinessCards, "businessCardId", connectedBusinessCardId);
                var businessCardProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, businessCard.UserId);
                businessCard.ProfileImage = businessCardProfile.PhotoUrl;
                businessCard.ProfileFullName = businessCardProfile.FullName;
                businessCard.ProfileEmail = businessCardProfile.Email;
                businessCards.Add(businessCard);
            }
            var model = new ConnectedCardsViewModel
            {
                CurrentUserProfile = currentUserProfile,
                ConnectedBusinessCards = businessCards.Select(c => _businessCardMapper.Map(c, null))
            };
            return View(model);
        }

        /// <summary>
        /// Unlink Business Card
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Unlink(string id)
        {
            await _businessCardLinkService.UnconnectToCard(id, User.UserId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> ShareBusinessCard(IEnumerable<ShareContactViewModel> contacts)
        {
            string businessCardId = Request["SelectedBusinessCardId"];
            var userProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, User.UserId);
            var businessCard = await _service.Get<BusinessCard>(FirestoreTableStore.BusinessCards, businessCardId);
            foreach (var contact in contacts.Where(p => p.IsSelected))
            {
                if (string.IsNullOrEmpty(contact.UserId))
                {
                    string message = $"Dear {contact.Name},<br/><br/>Please find below a link to my business card:<br/>{Defaults.DefaultSmartAssUrl}{businessCard.BusinessCardId}";
                    await _notificationService.SendEmail("Smart-ASS: Business Card Share -" + contact.Name, contact.Email, "info@smartassdigitalbusinesscard.com", message);

                    var activity = new Activity
                    {
                        ActivityType = EnumActivity.CardSharedOnBehalf.ToString(),
                        ActivityCreator = User.UserId,
                        ActivityTime = Timestamp.GetCurrentTimestamp(),
                        ActivityBusiness = businessCardId,
                        ActivityMessage = "Card shared with user not part of Smart-ASS",
                        ActivityOwner = null,
                        ActivityCreatorName = userProfile.FullName,
                        ActivityCreatorImage = userProfile.PhotoUrl
                    };
                    await _activityService.LogActivity(activity);
                }
                else
                {
                    await _notificationService.SendShareMyCardNotification(User.UserId, contact.UserId);

                    var activity = new Activity
                    {
                        ActivityType = EnumActivity.CardSharedOnBehalf.ToString(),
                        ActivityCreator = User.UserId,
                        ActivityTime = Timestamp.GetCurrentTimestamp(),
                        ActivityBusiness = businessCardId,
                        ActivityMessage = "Card Shared on behalf of Smart-ASS user",
                        ActivityOwner = contact.UserId,
                        ActivityCreatorName = userProfile.FullName,
                        ActivityCreatorImage = userProfile.PhotoUrl
                    };
                    await _activityService.LogActivity(activity);
                }
                //
            }
            return Json("OK");
        }
    }
}
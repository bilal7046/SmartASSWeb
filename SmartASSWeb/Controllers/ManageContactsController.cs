using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ManageContactsController 
        : ControllerBase
    {
        private readonly IFirebaseService _firebaseService;
        private readonly IActivityService _activityService;
        private readonly INotificationService _notificationService;

        public ManageContactsController(IFirebaseService firebaseService, IActivityService activityService, INotificationService notificationService)
        {
            _firebaseService = firebaseService;
            _activityService = activityService;
            _notificationService = notificationService;
        }

        // GET: ManageContacts
        public async Task<ActionResult> Index()
        {
            var contacts = new List<Contact>();

            var currentUserProfile = await _firebaseService.Get<UserProfile>("user-profile", "userId", User.UserId);
            foreach (var linkedUser in currentUserProfile.LinkedUsers)
            {
                var userProfileContact = await _firebaseService.Get<UserProfile>("user-profile", "userId", linkedUser);
                var contact = new Contact
                {
                    FirstName = userProfileContact.FirstName,
                    LastName = userProfileContact.LastName,
                    City = userProfileContact.City,
                    PhotoUrl = userProfileContact.PhotoUrl,
                    AddressLine1 = userProfileContact.AddressLine1,
                    Website = userProfileContact.Website,
                    AddressLine2 = userProfileContact.AddressLine2,
                    UserId = userProfileContact.UserId,
                    Country = userProfileContact.Country,
                    EmailAddress = userProfileContact.Email,
                    Phone = userProfileContact.Phone,
                    Mobile = userProfileContact.Mobile,
                    Province = userProfileContact.Province,
                    ContactId = string.Empty
                };
                contacts.Add(contact);
            }

            var col= await _firebaseService.GetCollection<Contact>("contacts", "userId", User.UserId);
            contacts.AddRange(col);
            var model = new ContactViewModel
            {
                Contacts = contacts.OrderBy(o => o.FirstName).ThenBy(o => o.LastName)
            };
            return View(model);
        }

        public async Task<ActionResult> Editor(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                var obj = new Contact
                {
                    UserId = User.UserId
                };
                return View(obj);
            }
            else
            {
                var obj = await _firebaseService.Get<Contact>("contacts", id);
                return View(obj);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddContact(Contact contact)
        {
            await _firebaseService.Add(Guid.NewGuid().ToString(), "contacts", contact);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Save(Contact contact)
        {
            if (string.IsNullOrEmpty(contact.ContactId))
            {
                _firebaseService.Add(Guid.NewGuid().ToString(), "contacts", contact);
            }
            else
            {
                _firebaseService.Update(contact.ContactId, "contacts", contact.ToDictionary());
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            _firebaseService.Delete(id, "contacts");

            return RedirectToAction("Index");
        }

        public ActionResult Unlink(string id)
        {
            //TODO: Remove from Array

            return RedirectToAction("Index");
        }

        #region Share My Card

        /// <summary>
        /// This is for when I send someone on the platform my card
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> ShareMyCardInternal(string userId)
        {
            var userToShareWith = await _firebaseService.Get<UserProfile>(FirestoreTableStore.UserProfiles, userId);
            var activity = new Activity
            {
                ActivityType = EnumActivity.CardSharedInternal.ToString(),
                ActivityTime = Timestamp.GetCurrentTimestamp(),
                ActivityCreator = User.UserId,
                ActivityOwner = userId,
                ActivityMessage = $"Shared my card with {userToShareWith.FirstName} {userToShareWith.LastName}: {userToShareWith.UserId}"
            };
            await _firebaseService.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);

            await _notificationService.SendShareMyCardNotification(User.UserId, userId);

            return Json("Card shared successfully");
        }

        /// <summary>
        /// This is for when I share someone else's card externally
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fullName"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> ShareCardExternal(string userId, string fullName, string email)
        {
            UserProfile currentUser;
            string message;
            if (string.IsNullOrEmpty(userId))
            {
                var activity = new Activity
                {
                    ActivityType = EnumActivity.CardSharedExternal.ToString(),
                    ActivityTime = Timestamp.GetCurrentTimestamp(),
                    ActivityCreator = User.UserId,
                    ActivityOwner = User.UserId,
                    ActivityMessage = "Shared my card"
                };
                await _firebaseService.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
                currentUser = await _firebaseService.Get<UserProfile>(FirestoreTableStore.UserProfiles, User.UserId);
                message = $"Dear {fullName},\n\nPlease find below a link to my digital business card:\n\n<a href=\"{currentUser.ProfileIDUrl}\">{currentUser.ProfileIDUrl}</a>";
            }
            else
            {
                var activity = new Activity
                {
                    ActivityType = EnumActivity.CardSharedExternal.ToString(),
                    ActivityTime = Timestamp.GetCurrentTimestamp(),
                    ActivityCreator = User.UserId,
                    ActivityOwner = userId,
                    ActivityMessage = "Shared card"
                };
                await _firebaseService.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
                currentUser = await _firebaseService.Get<UserProfile>(FirestoreTableStore.UserProfiles, userId);
                message = $"Dear {fullName},\n\nPlease find below a link to my digital business card:\n\n<a href=\"{currentUser.ProfileIDUrl}\">{currentUser.ProfileIDUrl}</a>";
            }
            await _notificationService.SendEmail($"Smart-ASS Card Share with {currentUser.FirstName} {currentUser.LastName}", email, currentUser.Email, message);

            return Json("Card shared successfully");
        }

        #endregion
    }
}
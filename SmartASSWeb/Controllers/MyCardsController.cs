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
    public class MyCardsController
        : ControllerBase
    {
        private readonly IFirebaseService _service;
        private readonly IAddressService _addressService;
        private readonly ILookupDictionary _lookupDictionary;
        private readonly IUniqueCodeGenerator _codeGenerator;
        private readonly INotificationService _notificationService;
        private readonly IActivityService _activityService;
        private readonly IBusinessCardMapper _businessCardMapper;

        public MyCardsController(IFirebaseService service, IAddressService addressService, ILookupDictionary lookupDictionary, IUniqueCodeGenerator codeGenerator, INotificationService notificationService, IActivityService activityService, IBusinessCardMapper businessCardMapper)
        {
            _service = service;
            _addressService = addressService;
            _lookupDictionary = lookupDictionary;
            _codeGenerator = codeGenerator;
            _notificationService = notificationService;
            _activityService = activityService;
            _businessCardMapper = businessCardMapper;
        }

        public async Task<ActionResult> Index()
        {
            var profile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, User.UserId);
            var col = await _service.GetCollection<BusinessCard>(FirestoreTableStore.BusinessCards, "userId", User.UserId);

            var sharedContacts = new List<ShareContactViewModel>();
            foreach (var linkedUserId in profile.LinkedUsers)
            {
                var linkedUser = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, linkedUserId);
                var sharedContact = new ShareContactViewModel
                {
                    UserId = linkedUser.UserId,
                    PhotoUrl = linkedUser.PhotoUrl,
                    Name = linkedUser.FullName,
                    Phone = linkedUser.Phone,
                    Email = linkedUser.Email,
                    IsSmartAssUser = true
                };
                sharedContacts.Add(sharedContact);
            }
            var contacts = await _service.GetCollection<Contact>(FirestoreTableStore.Contacts, "userId", User.UserId);
            foreach (var contact in contacts)
            {
                var item = new ShareContactViewModel
                {
                    UserId = null,
                    PhotoUrl = contact.PhotoUrl,
                    Name = contact.FullName,
                    Email = contact.EmailAddress,
                    Phone = contact.Phone,
                    IsSmartAssUser = false
                };
                sharedContacts.Add(item);
            }
            var model = new MyCardsViewModel
            {
                CurrentUserProfile = profile,
                BusinessCards = col.Select(c=> _businessCardMapper.Map(c, profile)),
                Contacts = sharedContacts.OrderBy(c=> c.Name).ToList()
            };
            return View(model);
        }
        
        public async Task<ActionResult> Editor(string id)
        {
            ViewBag.Industries = _lookupDictionary.Industries;
            ViewBag.CompanyTypes = _lookupDictionary.CompanyTypes;
            ViewBag.ProductServices = _lookupDictionary.Tags;
            ViewBag.Tags = _lookupDictionary.Tags;
            if (string.IsNullOrEmpty(id))
            {
                var obj = new BusinessCard {BusinessCardId = _codeGenerator.Generate(8)};
                var model = new BusinessCardViewModel
                {
                    BusinessCard = obj,
                    Countries = await _addressService.GetCountries()
                };
                return View(model);
            }
            else
            {
                var obj = await _service.Get<BusinessCard>(FirestoreTableStore.BusinessCards, id);
                var model = new BusinessCardViewModel
                {
                    BusinessCard = obj,
                    Countries = await _addressService.GetCountries()
                };
                return View(model);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ShareBusinessCard(IEnumerable<ShareContactViewModel> contacts)
        {
            string businessCardId = Request["SelectedBusinessCardId"];
            var userProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, User.UserId);
            var businessCard = await _service.Get<BusinessCard>(FirestoreTableStore.BusinessCards, businessCardId);
            foreach (var contact in contacts.Where(p=> p.IsSelected))
            {
                if (string.IsNullOrEmpty(contact.UserId))
                {
                    string message = $"Dear {contact.Name},<br/><br/>Please find below a link to my business card:<br/>{Defaults.DefaultSmartAssUrl}{businessCard.BusinessCardId}";
                    await _notificationService.SendEmail("Smart-ASS: Business Card Share -" + contact.Name, contact.Email, "info@smartassdigitalbusinesscard.com", message);
                    
                    var activity = new Activity
                    {
                        ActivityType = EnumActivity.CardSharedExternal.ToString(),
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
                        ActivityType = EnumActivity.CardSharedInternal.ToString(),
                        ActivityCreator = User.UserId,
                        ActivityTime = Timestamp.GetCurrentTimestamp(),
                        ActivityBusiness = businessCardId,
                        ActivityMessage = "Card Shared with Smart-ASS user",
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

        [HttpPost]
        public async Task<ActionResult> Save(BusinessCardViewModel model)
        {
            var exists = await _service.Get<BusinessCard>(FirestoreTableStore.BusinessCards, model.BusinessCard.BusinessCardId);
            if (exists == null)
            {
                model.BusinessCard.UserId = User.UserId;
                await _service.Add(model.BusinessCard.BusinessCardId, FirestoreTableStore.BusinessCards, model.BusinessCard);
            }
            else
            {
                await _service.Update(model.BusinessCard.BusinessCardId, FirestoreTableStore.BusinessCards, model.BusinessCard.ToDictionary());
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            _service.Delete(id, FirestoreTableStore.BusinessCards);

            return RedirectToAction("Index");
        }

        //[AllowAnonymous]
        public ActionResult Test()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetProvinces(string country)
        {
            return Json(await _addressService.GetStates(country), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetCities(string province)
        {
            return Json(await _addressService.GetCities(province), JsonRequestBehavior.AllowGet);
        }
    }

}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Services;
using SmartASSWeb.Core.Service;
using SmartASSWeb.ViewModels;

namespace SmartASSWeb.Controllers
{
    [SmartAssAuthorize(Common.INDIVIDUAL)]
    public class HomeController 
        : ControllerBase
    {
        private readonly IFirebaseService _service;
        private readonly IWeatherService _weatherService;
        private readonly IBusinessCardMapper _businessCardMapper;

        public HomeController(IFirebaseService service, IWeatherService weatherService, IBusinessCardMapper businessCardMapper)
        {
            _service = service;
            _weatherService = weatherService;
            _businessCardMapper = businessCardMapper;
        }

        public async Task<ActionResult> Index()
        {
            //Load current profile
            var user = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, "userId", User.UserId);
            var businessCards = await _service.GetCollection<BusinessCard>(FirestoreTableStore.BusinessCards, "userId", User.UserId);
            var defaultCard = businessCards.FirstOrDefault();
            var activities = new List<Activity>();
            if (defaultCard != null)
            {
                activities = (await _service.GetCollection<Activity>(FirestoreTableStore.Activities, "activityBusiness", defaultCard.BusinessCardId)).ToList();
            }
            var weatherDetail = await _weatherService.GetWeatherDetail(defaultCard?.City);
            //
            var contacts = new List<Contact>();
            foreach (var linkedUser in user.LinkedUsers)
            {
                var contactProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, "userId", linkedUser);
                var contact = new Contact
                {
                    PhotoUrl = contactProfile.PhotoUrl,
                    FirstName = contactProfile.FirstName,
                    LastName = contactProfile.LastName,
                    Phone = contactProfile.Phone,
                    Mobile = contactProfile.Mobile,
                    EmailAddress = contactProfile.Email,

                };
                contacts.Add(contact);
            }

            var savedContacts = await _service.GetCollection<Contact>(FirestoreTableStore.Contacts, "userId", User.UserId);
            contacts.AddRange(savedContacts);
            //
            return View(new HomeViewModel
            {
                UserProfile = user,
                TotalContacts = contacts.Count,
                TotalCardsSent = activities.Count(p => p.ActivityType == EnumActivity.CardSharedInternal.ToString()),
                TotalCardsAccepted = activities.Count(p => p.ActivityType == EnumActivity.RequestAccepted.ToString()),
                RequestCount = activities.Count(p=> p.ActivityType == EnumActivity.RequestToConnect.ToString()),
                Contacts = contacts.OrderByDescending(c=> c.DateCreated).Take(5),
                Activities = activities.OrderByDescending(p=> p.ActivityTime).Take(5).Select(p => new Activity
                {
                    ActivityType = p.ActivityType,
                    ActivityTime = p.ActivityTime,
                    ActivityCreator = p.ActivityCreator,
                    ActivityMessage = p.ActivityMessage,
                    ActivityOwner = p.ActivityOwner,
                    ActivityCreatorName = Task.Run(() => GetCreatorName(p.ActivityCreator)).Result
                }),
                City = user.City,
                WeatherDetail = weatherDetail,
                BusinessCards = businessCards.Select(c => _businessCardMapper.Map(c, user)).Take(1).ToList()
            });
        }

        private async Task<string> GetCreatorName(string activityCreator)
        {
            var user = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, "userId", activityCreator);
            if (user == null) return "Anonymous";
            return $"{user?.FirstName} {user?.LastName}";
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
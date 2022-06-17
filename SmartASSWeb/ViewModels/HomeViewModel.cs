using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Services;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.ViewModels
{
    public class HomeViewModel
    {
        private readonly IFirebaseService _service;
        private WeatherResult _weatherDetail;

        public HomeViewModel()
        {
            
        }
        public HomeViewModel(IFirebaseService service)
        {
            _service = service;
            Contacts = new List<Contact>();
            Activities = new List<Activity>();
            BusinessCards = new List<BusinessCard>();
        }
        public UserProfile UserProfile { get; set; }
        public int TotalContacts { get; set; }
        public int RequestCount { get; set; }
        public int TotalCardsSent { get; set; }
        public int TotalCardsAccepted { get; set; }

        public string City { get; set; }

        public WeatherResult WeatherDetail
        {
            get => _weatherDetail ?? new WeatherResult { Temp = "--", TempMin = "--", Description = "----"};
            set => _weatherDetail = value;
        }

        public List<BusinessCard> BusinessCards { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
        public IEnumerable<Activity> Activities { get; set; }

        public string GetActivityCreatorName(string userId)
        {
            var creator = _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, userId).Result;
            return $"{creator.FirstName} {creator.LastName}";
        }
    }
}
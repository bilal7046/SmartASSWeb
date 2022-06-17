using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Mvc;
using Google.Type;
using SmartASSWeb.Bll;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.ViewModels
{
    public class ChartBaseModel
    {
        public string BusinessId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string DateRange { get; set; }
        public int TotalCount { get; set; }

        public IEnumerable<BusinessCard> GetCompanies(string email)
        {
            var service = DependencyResolver.Current.GetService<IFirebaseService>();
            var userProfile = Task.Run(() => service.Get<UserProfile>(FirestoreTableStore.UserProfiles, "email", email)).Result;
            var companies = Task.Run(() => service.GetCollection<BusinessCard>(FirestoreTableStore.BusinessCards, "userId", userProfile.UserId)).Result;
            return companies;
        }
    }
}
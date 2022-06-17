using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.Mvc;
using SmartASSWeb.Bll.Services;
using SmartASSWeb.ViewModels.Charts.BusinessCards;

namespace SmartASSWeb.Controllers
{
    public class ProfileStatsController : ControllerBase
    {
        private readonly IBusinessCardChartDataService _businessCardChartDataService;

        public ProfileStatsController(IBusinessCardChartDataService businessCardChartDataService)
        {
            _businessCardChartDataService = businessCardChartDataService;
        }

        // GET: ProfileStats
        public async Task<ActionResult> UserRequests(string userId)
        {
            var startDate = DateTime.Now.AddDays(-6);
            var endDate = DateTime.Now;
            var model = new BusinessCardStatsViewModel
            {
                NumberOfCardsSent = await _businessCardChartDataService.GetNumberOfCardsSent(userId ?? User.UserId, startDate, endDate),
                AcceptedCards = await _businessCardChartDataService.GetAcceptedInvites(userId ?? User.UserId, startDate, endDate),
                InvitesReceived = await _businessCardChartDataService.GetInvitesReceived(userId ?? User.UserId, startDate, endDate),
                PendingCards = await _businessCardChartDataService.GetPendingCards(userId ?? User.UserId, startDate, endDate),
                PeopleSharedMyCards = await _businessCardChartDataService.GetPeopleSharedMyCards(userId ?? User.UserId, startDate, endDate),
                SocialMediaLinks = await _businessCardChartDataService.GetSocialMediaLinks(User.UserId)
            };
            return View(model);
        }
        // GET: ProfileStats
        public async Task<ActionResult> Demographics()
        {
            var startDate = DateTime.Now.AddDays(-6);
            var endDate = DateTime.Now;
            var model = new BusinessCardStatsViewModel
            {
                //DateRange = $"{startDate} - {endDate}",
                //AgeClicks = await _businessCardChartDataService.GetAgeClicks(User.UserId),
                GenderClicks = await _businessCardChartDataService.GetGenderClicks(User.UserId, startDate, endDate),
                //JobLevelClicks = await _businessCardChartDataService.GetJobLevelClicks(User.UserId)
            };
            return View(model);
        }
        // GET: ProfileStats
        public async Task<ActionResult> EngagementRate()
        {
            var startDate = DateTime.Now.AddDays(-6);
            var endDate = DateTime.Now;
            var model = new BusinessCardStatsViewModel
            {
                //DateRange = $"{startDate} - {endDate}",
            };
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> FilterUserRequests(BusinessCardStatsViewModel model)
        {
            var dates = model.DateRange.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            var startDate = DateTime.Parse(dates[0], CultureInfo.CurrentCulture);
            var endDate = DateTime.Parse(dates[1], CultureInfo.CurrentCulture);

            var filteredModel = new BusinessCardStatsViewModel
            {
                DateRange = $"{startDate} - {endDate}",
                NumberOfCardsSent = await _businessCardChartDataService.GetNumberOfCardsSent(User.UserId, startDate, endDate),
                AcceptedCards = await _businessCardChartDataService.GetAcceptedInvites(User.UserId, startDate, endDate),
                InvitesReceived = await _businessCardChartDataService.GetInvitesReceived(User.UserId, startDate, endDate),
                PendingCards = await _businessCardChartDataService.GetPendingCards(User.UserId, startDate, endDate),
                PeopleSharedMyCards = await _businessCardChartDataService.GetPeopleSharedMyCards(User.UserId, startDate, endDate),
                SocialMediaLinks = await _businessCardChartDataService.GetSocialMediaLinks(User.UserId)
            };
            return Json(filteredModel);
        }
    }
}
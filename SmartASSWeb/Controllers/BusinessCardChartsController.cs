using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Services;
using SmartASSWeb.Core.Service;
using SmartASSWeb.ViewModels.Charts.BusinessCards;
// ReSharper disable InlineOutVariableDeclaration

namespace SmartASSWeb.Controllers
{
    [SmartAssAuthorize(Roles = Common.INDIVIDUAL)]
    public class BusinessCardChartsController 
        : ControllerBase
    {
        #region Ctor

        private readonly IFirebaseService _service;
        private readonly IBusinessCardChartDataService _businessCardChartDataService;

        public BusinessCardChartsController(IFirebaseService service, IBusinessCardChartDataService businessCardChartDataService)
        {
            _service = service;
            _businessCardChartDataService = businessCardChartDataService;
        }

        #endregion

        #region Behavior

        public async Task<ActionResult> CardsRequested(string businessId, string dateRange, bool json = false)
        {
            DateTime startDate;
            DateTime endDate;
            GetDateRange(dateRange, out startDate, out endDate);
            var numberOfCardsSent = await _businessCardChartDataService.GetNumberOfCardsSent(businessId ?? await GetDefaultBusinessCardId(), startDate, endDate);
            var model = new BusinessCardStatsViewModel
            {
                NumberOfCardsSent = numberOfCardsSent,
                TotalCount = numberOfCardsSent.Count
            };
            if (json) return Json(model, JsonRequestBehavior.AllowGet);
            return View("CardsRequested", model);
        }

        public async Task<ActionResult> AcceptedCards(string businessId, string dateRange, bool json = false)
        {
            DateTime startDate;
            DateTime endDate;
            GetDateRange(dateRange, out startDate, out endDate);
            var businessCards = await _service.GetCollection<BusinessCard>(FirestoreTableStore.BusinessCards, "userId", User.UserId);
            var defaultBusinessCardId = businessCards.FirstOrDefault()?.BusinessCardId;
            var acceptedInvites = await _businessCardChartDataService.GetAcceptedInvites(businessId ?? defaultBusinessCardId, startDate, endDate);
            var model = new BusinessCardStatsViewModel
            {
                AcceptedCards = acceptedInvites,
                TotalCount = acceptedInvites.Count
            };
            if (json) return Json(model, JsonRequestBehavior.AllowGet);
            return View("AcceptedCards", model);
        }
        public async Task<ActionResult> PendingCards(string businessId, string dateRange, bool json = false)
        {
            DateTime startDate;
            DateTime endDate;
            GetDateRange(dateRange, out startDate, out endDate);
            var pendingCards = await _businessCardChartDataService.GetPendingCards(businessId ?? await GetDefaultBusinessCardId(), startDate, endDate);
            var model = new BusinessCardStatsViewModel
            {
                PendingCards = pendingCards,
                TotalCount = pendingCards.Count
            };
            if (json) return Json(model, JsonRequestBehavior.AllowGet);
            return View("PendingCards", model);
        }
        public async Task<ActionResult> CardInvites(string businessId, string dateRange, bool json = false)
        {
            DateTime startDate;
            DateTime endDate;
            GetDateRange(dateRange, out startDate, out endDate);
            var invitesReceived = await _businessCardChartDataService.GetInvitesReceived(businessId ?? await GetDefaultBusinessCardId(), startDate, endDate);
            var model = new BusinessCardStatsViewModel
            {
                InvitesReceived = invitesReceived,
                TotalCount = invitesReceived.Count
            };
            if (json) return Json(model, JsonRequestBehavior.AllowGet);
            return View("CardInvites", model);
        }
        public async Task<ActionResult> CardShares(string businessId, string dateRange, bool json = false)
        {
            DateTime startDate;
            DateTime endDate;
            GetDateRange(dateRange, out startDate, out endDate);
            var peopleSharedMyCards = await _businessCardChartDataService.GetPeopleSharedMyCards(businessId ?? await GetDefaultBusinessCardId(), startDate, endDate);
            var model = new BusinessCardStatsViewModel
            {
                PeopleSharedMyCards = peopleSharedMyCards,
                TotalCount = peopleSharedMyCards.Count
            };
            if (json) return Json(model, JsonRequestBehavior.AllowGet);
            return View("CardShares", model);
        }
        public async Task<ActionResult> SocialMediaLinks(string businessId, DateTime? startDate, DateTime? endDate, bool json = false)
        {
            var socialMediaLinks = await _businessCardChartDataService.GetSocialMediaLinks(businessId ?? await GetDefaultBusinessCardId());
            var model = new BusinessCardStatsViewModel
            {
                SocialMediaLinks = socialMediaLinks,
                TotalCount = socialMediaLinks.Count
            };
            if (json) return Json(model, JsonRequestBehavior.AllowGet);
            return View("SocialMediaLinks", model);
        }

        #endregion

        #region Demographics

        [SmartAssAuthorize(Roles = Common.SMALLBUSINESS)]
        public async Task<ActionResult> AgeGroupClicks(string businessId, DateTime? startDate, DateTime? endDate, bool json = false)
        {
            var ageClicks = await _businessCardChartDataService.GetAgeClicks(businessId ?? await GetDefaultBusinessCardId(), User.UserId);
            var model = new BusinessCardStatsViewModel
            {
                AgeClicks = ageClicks,
                AgeData = new AgeData
                {
                    AgeGroups = ageClicks.Select(p => p.Key).ToArray(),
                    Data = ageClicks.Select(p => p.Value).ToArray()
                },
                TotalCount = ageClicks.Sum(c => c.Value)
            };
            if (json) return Json(model, JsonRequestBehavior.AllowGet);
            return View("AgeGroupClicks", model);
        }

        [SmartAssAuthorize(Roles = Common.SMALLBUSINESS)]
        public async Task<ActionResult> GenderClicks(string businessId, string dateRange, bool json = false)
        {
            DateTime startDate;
            DateTime endDate;
            GetDateRange(dateRange, out startDate, out endDate);
            var genderClicks = await _businessCardChartDataService.GetGenderClicks(businessId ?? await GetDefaultBusinessCardId(), startDate, endDate);
            var model = new BusinessCardStatsViewModel
            {
                GenderClicks = genderClicks,
                TotalCount = genderClicks.Sum(c => c.Value)
            };
            if (json) return Json(model, JsonRequestBehavior.AllowGet);
            return View("GenderClicks", model);
        }

        [SmartAssAuthorize(Roles = Common.SMALLBUSINESS)]
        public async Task<ActionResult> JobLevelClicks(string businessId, DateTime? startDate, DateTime? endDate, bool json = false)
        {
            var jobLevelClicks = await _businessCardChartDataService.GetJobLevelClicks(businessId ?? await GetDefaultBusinessCardId(), User.UserId);
            var model = new BusinessCardStatsViewModel
            {
                JobLevelData = new JobLevelData
                {
                    JobLevels = jobLevelClicks.Select(p=> p.Key).ToArray(),
                    Data = jobLevelClicks.Select(p => p.Value).ToArray()
                },
                TotalCount = jobLevelClicks.Sum(c=> c.Value)
            };
            if (json) return Json(model, JsonRequestBehavior.AllowGet);
            return View("JobLevelClicks", model);
        }

        #endregion

        #region Engagement Rate
        
        public async Task<ActionResult> EngagementRate(string businessId, string dateRange, bool json = false)
        {
            DateTime startDate;
            DateTime endDate;
            GetDateRange(dateRange, out startDate, out endDate);
            var engagementRates = await _businessCardChartDataService.GetEngagementRates(businessId ?? await GetDefaultBusinessCardId(),startDate,endDate);
            var model = new BusinessCardStatsViewModel
            {
                EngagementRates = engagementRates,
                TotalCount = engagementRates.Count
            };
            if (json) return Json(model, JsonRequestBehavior.AllowGet);
            return View("EngagementRate", model);
        }

        #endregion

        #region Helper Methods

        private async Task<string> GetDefaultBusinessCardId()
        {
            var businessCards = await _service.GetCollection<BusinessCard>(FirestoreTableStore.BusinessCards, "userId", User.UserId);
            var defaultBusinessCardId = businessCards.FirstOrDefault()?.BusinessCardId;
            return defaultBusinessCardId;
        }
        
        #endregion
    }
}
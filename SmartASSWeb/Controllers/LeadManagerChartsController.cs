using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Services;
using SmartASSWeb.Core.Service;
using SmartASSWeb.ViewModels;

namespace SmartASSWeb.Controllers
{
    [SmartAssAuthorize(Roles = Common.BUSINESS)]
    public class LeadManagerChartsController 
        : ControllerBase
    {
        #region Ctor

        private readonly IFirebaseService _firebaseService;
        private readonly IManagerChartDataService _chartDataService;

        public LeadManagerChartsController(IFirebaseService firebaseService, IManagerChartDataService chartDataService)
        {
            _firebaseService = firebaseService;
            _chartDataService = chartDataService;
        }

        #endregion

        public async Task<ActionResult> LeadsPerIndustry()
        {
            var now = DateTime.Now;
            var startDate = new DateTime(now.Year, 1, 1);
            var endDate = now;
            var leadsPerIndustry = await _chartDataService.GetLeadPerIndustry(User.UserId, startDate, endDate);

            var model = new ManagerDashboardViewModel
            {
                TotalLeads = leadsPerIndustry.Sum(p=> p.Value),
                LeadsPerIndustry = new LeadsPerIndustry
                {
                    Industries = leadsPerIndustry.Select(c => c.Key).ToArray(),
                    Data = leadsPerIndustry.Select(c => c.Value).ToArray()
                }
            };
            return View("LeadsPerIndustry",model);
        }

        public async Task<ActionResult> TeamMemberLeads()
        {
            var now = DateTime.Now;
            var startDate = new DateTime(now.Year, 1, 1);
            var endDate = now;

            var model = new ManagerDashboardViewModel
            {
                LeadsPerTeamMember = await _chartDataService.GetLeadsPerTeam(User.UserId, startDate, endDate)
            };
            return View("TeamMemberLeads", model);
        }

        public async Task<ActionResult> TotalRevenue()
        {
            var now = DateTime.Now;
            var startDate = new DateTime(now.Year, 1, 1);
            var endDate = now;

            var revenueData = await _chartDataService.GetRevenueData(User.UserId, startDate, endDate);
            var model = new ManagerDashboardViewModel
            {
                TotalSales = revenueData.Sum(c=> c.Sum),
                TotalRevenueForPeriod = revenueData
            };
            return View("TotalRevenue", model);
        }
        public async Task<ActionResult> SalesContest()
        {
            var now = DateTime.Now;
            var startDate = new DateTime(now.Year, 1, 1);
            var endDate = now;

            var chartClosedLeadsPerMember = await _chartDataService.GetClosedLeadsPerTeam(User.UserId, startDate, endDate);
            var model = new ManagerDashboardViewModel
            {
                TotalLeads = chartClosedLeadsPerMember.ClosedLeads.Sum(p=> p),
                ChartClosedLeadsPerMember = chartClosedLeadsPerMember
            };
            return View("SalesContest", model);
        }
    }
}
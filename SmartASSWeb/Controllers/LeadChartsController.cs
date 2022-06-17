using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Core;
using SmartASSWeb.Bll.Services;
using SmartASSWeb.ViewModels;
// ReSharper disable InlineOutVariableDeclaration

namespace SmartASSWeb.Controllers
{
    [SmartAssAuthorize(Roles = Common.SMALLBUSINESS)]
    public class LeadChartsController 
        : ControllerBase
    {
        private readonly ITeamMemberChartDataService _teamMemberChartDataService;

        public LeadChartsController(ITeamMemberChartDataService teamMemberChartDataService)
        {
            _teamMemberChartDataService = teamMemberChartDataService;
        }

        public async Task<ActionResult> LeadSourceRatio(string userId)
        {
            var leadsPerSource = await _teamMemberChartDataService.GetLeadsPerSource(userId ?? User.UserId);
            var colors = new Queue<string>(LeadDashboardViewModel.ColorBasket);

            var model = new LeadDashboardViewModel
            {
                LeadSourceData = leadsPerSource.Select(c => new Pie { color = (colors.Count == default(int) ? string.Empty : colors.Dequeue()), data = c.Value, label = c.Key })
            };
            return View("LeadSourceRatio", model);
        }

        public async Task<ActionResult> LeadStatusRatio(string userId, string dateRange, bool json = false)
        {
            DateTime startDate;
            DateTime endDate;
            GetDateRange(dateRange, out startDate, out endDate);
            var leadStatusRatio = await _teamMemberChartDataService.GetLeadStatusRatio(userId ?? User.UserId, startDate, endDate.AddDays(30));

            var model = new LeadDashboardViewModel
            {
                LeadStatusData = leadStatusRatio.Select(c => new Funnel { y = c.Value, label = c.Key })
            };
            if (json) return Json(model, JsonRequestBehavior.AllowGet);
            return View("LeadStatusRatio", model);
        }

        public async Task<ActionResult> MonthlyLeads(string userId)
        {
            var leadsForYear = await _teamMemberChartDataService.GetLeadsForYear(userId ?? User.UserId);

            var model = new LeadDashboardViewModel
            {
                LeadsForYear = leadsForYear.Select(c => new NightingGale { name = c.Key, value = c.Value })
            };
            return View("MonthlyLeads", model);
        }

        public async Task<ActionResult> DailyLeads(string userId)
        {
            var dailyLeads = await _teamMemberChartDataService.GetDailyLeads(userId ?? User.UserId);

            var model = new LeadDashboardViewModel
            {
                LeadsForMonth = dailyLeads.Select(c => new long[] { DateTime.Parse(c.Key).ToJavascriptTimestamp(), c.Value })
            };
            return View("DailyLeads", model);
        }
    }
}
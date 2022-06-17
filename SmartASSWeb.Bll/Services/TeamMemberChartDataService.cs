using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using SmartASSWeb.Bll._Mock;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Bll.Services
{
    public interface ITeamMemberChartDataService
    {
        Task<IEnumerable<ChartData>> GetLeadStatusRatio(string userId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<ChartData>> GetLeadsPerSource(string userId);
        Task<IEnumerable<ChartData>> GetDailyLeads(string userId);
        Task<IEnumerable<ChartData>> GetLeadsForYear(string userId);
    }
    public class TeamMemberChartDataService
        : ITeamMemberChartDataService
    {
        #region Ctor

        private readonly IFirebaseService _service;

        public TeamMemberChartDataService(IFirebaseService service)
        {
            _service = service;
        }

        #endregion

        public async Task<IEnumerable<ChartData>> GetLeadStatusRatio(string userId, DateTime startDate, DateTime endDate)
        {
            if (userId == MockDataGenerator.DemoUser)
            {
                return new List<ChartData>
                {
                    new ChartData(EnumLeadStatus.Connected.ToString(), 100),
                    new ChartData(EnumLeadStatus.Qualified.ToString(), 43),
                    new ChartData(EnumLeadStatus.Quoted.ToString(), 34),
                    new ChartData(EnumLeadStatus.Closed.ToString(), 27),
                };
            }
            var user = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, userId);
            List<Lead> teamMemberLeads = new List<Lead>();
            foreach (var teamMember in user.TeamMembers)
            {
                var col = await _service.GetCollection<Lead>(FirestoreTableStore.Leads, "userId", teamMember);
                teamMemberLeads.AddRange(col);
            }

            var filteredLeads = teamMemberLeads
                                                    .Where(c => c.DateCreated >= Timestamp.FromDateTime(startDate.ToUniversalTime()) && c.DateCreated <= Timestamp.FromDateTime(endDate.ToUniversalTime()))
                                                    .GroupBy(p => p.LeadStatus)
                                                    .Select(p => new ChartData
                                                    {
                                                        Key = p.Key,
                                                        Value = p.Count()
                                                    }).ToList();

            var data = new List<ChartData>();
            var connectedLead = filteredLeads.FirstOrDefault(p => p.Key == EnumLeadStatus.Connected.ToString());
            data.Add(new ChartData(EnumLeadStatus.Connected.ToString(), connectedLead?.Value ?? 0));
            var qualifiedLead = filteredLeads.FirstOrDefault(p => p.Key == EnumLeadStatus.Qualified.ToString());
            data.Add(new ChartData(EnumLeadStatus.Qualified.ToString(), qualifiedLead?.Value ?? 0));
            var quotedLead = filteredLeads.FirstOrDefault(p => p.Key == EnumLeadStatus.Quoted.ToString());
            data.Add(new ChartData(EnumLeadStatus.Quoted.ToString(), quotedLead?.Value ?? 0));
            var closedLead = filteredLeads.FirstOrDefault(p => p.Key == EnumLeadStatus.Closed.ToString());
            data.Add(new ChartData(EnumLeadStatus.Closed.ToString(), closedLead?.Value ?? 0));
            return data;
        }
        public async Task<IEnumerable<ChartData>> GetLeadsPerSource(string userId)
        {
            var teamMemberLeads = await _service.GetCollection<Lead>(FirestoreTableStore.Leads, "userId", userId);

            var filteredLeads = teamMemberLeads
                .GroupBy(p => p.LeadSource)
                .Select(p => new ChartData
                {
                    Key = p.Key,
                    Value = p.Count()
                }).ToList();
            return filteredLeads;
        }
        public async Task<IEnumerable<ChartData>> GetDailyLeads(string userId)
        {
            var teamMemberLeads = await _service.GetCollection<Lead>(FirestoreTableStore.Leads, "userId", userId);

            var now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var filteredLeads = teamMemberLeads.Where(p=> p.DateCreated.ToDateTime() >= startDate && p.DateCreated.ToDateTime() <= endDate)
                .GroupBy(p => p.DateCreated.ToDateTime().ToString("dd/MM/yyyy"))
                .Select(p => new ChartData
                {
                    Key = p.Key,
                    Value = p.Count()
                }).ToList();
            return filteredLeads;
        }
        public async Task<IEnumerable<ChartData>> GetLeadsForYear(string userId)
        {
            var teamMemberLeads = await _service.GetCollection<Lead>(FirestoreTableStore.Leads, "userId", userId);

            var now = DateTime.Now;
            var startDate = new DateTime(now.Year, 1, 1);
            var endDate = new DateTime(now.Year, 12, 31);
            var filteredLeads = teamMemberLeads.Where(p => p.DateCreated.ToDateTime() >= startDate && p.DateCreated.ToDateTime() <= endDate)
                .GroupBy(p => p.DateCreated.ToDateTime().ToString("MMMM", CultureInfo.InvariantCulture))
                .Select(p => new ChartData
                {
                    Key = p.Key,
                    Value = p.Count()
                }).ToList();
            return filteredLeads;
        }
    }
}

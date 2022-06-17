using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartASSWeb.Bll.Models;
using SmartASSWeb.Core.Service;
using DateTime = System.DateTime;

namespace SmartASSWeb.Bll.Services
{
    public interface IManagerChartDataService
    {
        Task<List<ChartData>> GetRevenueData(string userId, DateTime startDate, DateTime endDate);
        Task<ChartLeadsPerMember> GetLeadsPerTeam(string userId, DateTime startDate, DateTime endDate);
        Task<ChartClosedLeadsPerMember> GetClosedLeadsPerTeam(string userId, DateTime startDate, DateTime endDate);
        Task<List<ChartData>> GetLeadPerIndustry(string userId, DateTime startDate, DateTime endDate);
    }
    public class ManagerChartDataService
        : IManagerChartDataService
    {
        #region Ctor

        private readonly IFirebaseService _service;

        public ManagerChartDataService(IFirebaseService service)
        {
            _service = service;
        }

        #endregion

        public async Task<List<ChartData>> GetRevenueData(string userId, DateTime startDate, DateTime endDate)
        {
            var userProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, userId);
            var leads = new List<Lead>();
            foreach (var teamMember in userProfile.TeamMembers)
            {
                var teamMemberLeads = await _service.GetCollection<Lead>(FirestoreTableStore.Leads, "userId", teamMember);
                leads.AddRange(teamMemberLeads);
            }

            var filteredLeads = leads.Where(p => p.DateCreated.ToDateTime() >= startDate && p.DateCreated.ToDateTime() <= endDate).OrderBy(c=> c.DateMonthNumber)
                                                    .GroupBy(p => p.DateMonthString)
                                                    .Select(p => new ChartData
                                                    {
                                                        Key = p.Key,
                                                        Sum = Convert.ToInt32(p.Sum(c=> c.Revenue))
                                                    }).ToList();
            return filteredLeads;
        }

        public async Task<ChartLeadsPerMember> GetLeadsPerTeam(string userId, DateTime startDate, DateTime endDate)
        {
            var userProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, userId);
            var teamMembers = new List<UserProfile>();
            foreach (var userProfileTeamMember in userProfile.TeamMembers)
            {
                var teamMember = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, userProfileTeamMember);
                teamMembers.Add(teamMember);   
            }

            var orderedMembers = teamMembers.OrderBy(p=> p.FullName).ToList();

            var chartLeadsPerMember = new ChartLeadsPerMember
            {
                TeamMembers = orderedMembers.Select(p => p.FullName).ToArray()
            };
            foreach (var orderedMember in orderedMembers)
            {
                var memberLeads = await _service.GetCollection<Lead>(FirestoreTableStore.Leads, "userId", orderedMember.UserId);
                var col = memberLeads.Where(p=> p.DateCreated.ToDateTime() >= startDate && p.DateCreated.ToDateTime() <= endDate).ToList();
                chartLeadsPerMember.ConnectedLeads.Add(col.Count(p=> p.LeadStatus == EnumLeadStatus.Connected.ToString()));
                chartLeadsPerMember.QualifiedLeads.Add(col.Count(p=> p.LeadStatus == EnumLeadStatus.Qualified.ToString()));
                chartLeadsPerMember.QuotedLeads.Add(col.Count(p=> p.LeadStatus == EnumLeadStatus.Quoted.ToString()));
                chartLeadsPerMember.ClosedLeads.Add(col.Count(p=> p.LeadStatus == EnumLeadStatus.Closed.ToString()));
                chartLeadsPerMember.LostLeads.Add(col.Count(p=> p.LeadStatus == EnumLeadStatus.Lost.ToString()));
            }

            return chartLeadsPerMember;
        }

        public async Task<ChartClosedLeadsPerMember> GetClosedLeadsPerTeam(string userId, DateTime startDate, DateTime endDate)
        {
            var userProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, userId);
            var teamMembers = new List<UserProfile>();
            foreach (var userProfileTeamMember in userProfile.TeamMembers)
            {
                var teamMember = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, userProfileTeamMember);
                teamMembers.Add(teamMember);   
            }

            var orderedMembers = teamMembers.OrderBy(p=> p.FullName).ToList();

            var chartLeadsPerMember = new ChartClosedLeadsPerMember
            {
                TeamMembers = orderedMembers.Select(p => p.FullName).ToArray()
            };
            foreach (var orderedMember in orderedMembers)
            {
                var memberLeads = await _service.GetCollection<Lead>(FirestoreTableStore.Leads, "userId", orderedMember.UserId);
                var col = memberLeads.Where(p=> p.DateCreated.ToDateTime() >= startDate && p.DateCreated.ToDateTime() <= endDate).ToList();
                chartLeadsPerMember.ClosedLeads.Add(col.Count(p=> p.LeadStatus == EnumLeadStatus.Closed.ToString()));
            }

            return chartLeadsPerMember;
        }

        public async Task<List<ChartData>> GetLeadPerIndustry(string userId, DateTime startDate, DateTime endDate)
        {
            var userProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, userId);
            var leads = new List<Lead>();
            foreach (var userProfileTeamMember in userProfile.TeamMembers)
            {
                var teamMemberLeads = await _service.GetCollection<Lead>(FirestoreTableStore.Leads, "userId", userProfileTeamMember);
                leads.AddRange(teamMemberLeads);
            }
            var data = leads.Where(p => p.DateCreated.ToDateTime() >= startDate && p.DateCreated.ToDateTime() <= endDate).ToList()
                .GroupBy(p => p.Industry)
                .Select(p => new ChartData
                {
                    Key = p.Key,
                    Value = p.Count()
                }).ToList();

            return data;
        }

        public async Task<List<ChartData>> GetTotalLeadsForPeriod(string userId, DateTime startDate, DateTime endDate)
        {
            var userProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, userId);
            var leads = new List<Lead>();
            foreach (var userProfileTeamMember in userProfile.TeamMembers)
            {
                var teamMemberLeads = await _service.GetCollection<Lead>(FirestoreTableStore.Leads, "userId", userProfileTeamMember);
                leads.AddRange(teamMemberLeads);
            }
            var data = leads.Where(p => p.DateCreated.ToDateTime() >= startDate && p.DateCreated.ToDateTime() <= endDate).OrderBy(c => c.DateMonthNumber)
                .GroupBy(p => p.DateMonthString)
                .Select(p => new ChartData
                {
                    Key = p.Key,
                    Value = p.Count()
                }).ToList();

            return data;
        }
    }

        public class LeadsChartData
        {
            public string Month { get; set; }
            public string LeadStatus { get; set; }
            public int Total { get; set; }
        }
}

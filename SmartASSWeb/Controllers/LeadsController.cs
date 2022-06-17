using System;
using System.Collections.Generic;
using System.Globalization;
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
    [SmartAssAuthorize(Roles = Common.SMALLBUSINESS)]
    public class LeadsController 
        : ControllerBase
    {
        private readonly IFirebaseService _service;
        private readonly ILookupDictionary _dictionary;
        private readonly IManagerChartDataService _managerChartDataService;
        private readonly ITeamMemberChartDataService _teamMemberChartDataService;

        #region Ctor

        public LeadsController(IFirebaseService service, ILookupDictionary dictionary, IManagerChartDataService managerChartDataService, ITeamMemberChartDataService teamMemberChartDataService)
        {
            _service = service;
            _dictionary = dictionary;
            _managerChartDataService = managerChartDataService;
            _teamMemberChartDataService = teamMemberChartDataService;
        }

        #endregion

        #region Lead Management

        // GET: Leads
        public async Task<ActionResult> Index()
        {
            var leads = await _service.GetCollection<Lead>(FirestoreTableStore.Leads, "userId", User.UserId);
            return View(leads);
        }

        public async Task<ActionResult> Editor(string leadId)
        {
            if (string.IsNullOrEmpty(leadId))
            {
                var userProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, User.UserId);
                var newLead = new Lead
                {
                    LeadOwner = userProfile.FullName
                };
                return View(newLead);
            }
            else
            {
                var lead = await _service.Get<Lead>(FirestoreTableStore.Leads, leadId);
                return View(lead);
            }
        }

        [HttpPost]
        public ActionResult Save(Lead lead)
        {
            if (string.IsNullOrEmpty(lead.LeadId))
            {
                lead.UserId = User.UserId;
                _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Leads, lead);
            }
            else
            {
                _service.Update(lead.LeadId, FirestoreTableStore.Leads, lead.ToDictionary());
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(string leadId)
        {
            _service.Delete(leadId, FirestoreTableStore.Leads);

            return View("Index");
        }

        #endregion

        #region Lead View

        public async Task<ActionResult> ViewLead(string leadId)
        {
            var lead = await _service.Get<Lead>(FirestoreTableStore.Leads, leadId);
            var actions = await _service.GetCollection<LeadLogAction>(FirestoreTableStore.LeadLogActions, "leadId", leadId);
            var notes = await _service.GetCollection<LeadNote>(FirestoreTableStore.LeadNotes, "leadId", leadId);
            var model = new LeadManagementViewModel
            {
                Lead = lead,
                Actions = actions,
                Notes = notes,
                LogActionTypes = _dictionary.LogActions
            };
            return View("LeadView", model);
        }

        [HttpPost]
        public async Task<PartialViewResult> AddLeadAction(LeadLogAction model)
        {
            Timestamp? dateTime;
            if (string.IsNullOrEmpty(model.DueDateString))
            {
                dateTime = null;
            }
            else
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                var time = DateTime.ParseExact(model.DueDateString, "dd/MM/yyyy HH:mm", provider);
                var universalTime = time.ToUniversalTime();
                dateTime = Timestamp.FromDateTime(universalTime);
            }
            var action = new LeadLogAction
            {
                DateCreated = Timestamp.GetCurrentTimestamp(),
                LeadId = model.LeadId,
                LogAction = model.LogAction,
                Subject = model.Subject,
                Notes = model.Notes,
                DueDate = dateTime
            };
            await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.LeadLogActions, action);

            var actions = await _service.GetCollection<LeadLogAction>(FirestoreTableStore.LeadLogActions, "leadId", action.LeadId);
            return PartialView("LeadLogActionView", actions);
        }

        [HttpPost]
        public async Task<PartialViewResult> AddLeadNote(LeadNote model)
        {
            var note = new LeadNote
            {
                DateCreated = Timestamp.GetCurrentTimestamp(),
                Subject = model.Subject,
                LeadId = model.LeadId,
                Text = model.Text
            };
            await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.LeadNotes, note);

            var notes = await _service.GetCollection<LeadNote>(FirestoreTableStore.LeadNotes, "leadId", note.LeadId);
            return PartialView("LeadNotesView", notes);
        }

        #endregion

        #region LeadManager

        [HttpPost]
        public async Task<PartialViewResult> Filter(ManagerDashboardViewModel model)
        {
            var dates = model.DateRange.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            var startDate = DateTime.Parse(dates[0], CultureInfo.CurrentCulture);
            var endDate = DateTime.Parse(dates[1], CultureInfo.CurrentCulture);

            var leadsPerIndustry = await _managerChartDataService.GetLeadPerIndustry(User.UserId, startDate, endDate);
            var totalLeadsForPeriod = await _managerChartDataService.GetClosedLeadsPerTeam(User.UserId, startDate, endDate);
            var filteredModel = new ManagerDashboardViewModel
            {
                DateRange = $"{startDate} - {endDate}",
                TotalRevenueForPeriod = await _managerChartDataService.GetRevenueData(User.UserId, startDate, endDate),
                //ClosedLeadsOverPeriod = totalLeadsForPeriod,
                LeadsPerTeamMember = await _managerChartDataService.GetLeadsPerTeam(User.UserId, startDate, endDate),
                LeadsPerIndustry = new LeadsPerIndustry
                {
                    Industries = leadsPerIndustry.Select(c => c.Key).ToArray(),
                    Data = leadsPerIndustry.Select(c => c.Value).ToArray()
                }
            };
            return PartialView("ManagerDashboardPanel", filteredModel);
        }

        public async Task<ActionResult> TeamDashboard()
        {
            var userProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, User.UserId);
            var teamMemberGoal = await _service.Get<TeamMemberGoal>(FirestoreTableStore.TeamMemberGoals, User.UserId);
            if (teamMemberGoal == null)
            {
                await _service.Add(User.UserId, FirestoreTableStore.TeamMemberGoals, new TeamMemberGoal {UserId = User.UserId}); //Initialize Team Member Goal
                teamMemberGoal = await _service.Get<TeamMemberGoal>(FirestoreTableStore.TeamMemberGoals, User.UserId);
            }
            var teamMembers = new List<UserProfile>();
            foreach (var teamMemberId in userProfile.TeamMembers)
            {
                var teamMember = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, teamMemberId);
                teamMembers.Add(teamMember);
            }
            var model = new LeadManagementViewModel
            {
                TeamMemberGoal = teamMemberGoal,
                TeamMembers = teamMembers.Select(p=>
                {
                    var totalConnectedLeads = Task.Run(() => GetTotalCount(p.UserId, EnumLeadStatus.Connected)).Result;
                    var totalQualifiedLeads = Task.Run(() => GetTotalCount(p.UserId, EnumLeadStatus.Qualified)).Result;
                    var totalQuotedLeads = Task.Run(() => GetTotalCount(p.UserId, EnumLeadStatus.Quoted)).Result;
                    var totalClosedLeads = Task.Run(() => GetTotalCount(p.UserId, EnumLeadStatus.Closed)).Result;
                    var totalLostLeads = Task.Run(() => GetTotalCount(p.UserId, EnumLeadStatus.Lost)).Result;
                    return new TeamMemberViewModel
                    {
                        Designation = p.Designation,
                        EmailAddress = p.Email,
                        PhotoURL = p.PhotoUrl,
                        FullName = $"{p.FirstName} {p.LastName}",
                        UserId = p.UserId,
                        ConnectedLeads = totalConnectedLeads,
                        QualifiedLeads = totalQualifiedLeads,
                        QuotedLeads = totalQuotedLeads,
                        ClosedLeads = totalClosedLeads,
                        LostLeads = totalLostLeads,
                        ConnectedLeadsPercentile = GetPercentile(p.UserId, teamMemberGoal.ConnectedLeads, EnumLeadStatus.Connected, totalConnectedLeads),
                        QualifiedLeadsPercentile = GetPercentile(p.UserId, teamMemberGoal.QualifiedLeads, EnumLeadStatus.Qualified, totalQualifiedLeads),
                        QuotedLeadsPercentile = GetPercentile(p.UserId, teamMemberGoal.QuotedLeads, EnumLeadStatus.Quoted, totalQuotedLeads),
                        ClosedLeadsPercentile = GetPercentile(p.UserId, teamMemberGoal.ClosedLeads, EnumLeadStatus.Closed, totalClosedLeads),
                        LostLeadsPercentile = GetPercentile(p.UserId, teamMemberGoal.LostLeads, EnumLeadStatus.Lost, totalLostLeads)
                    };
                }).ToList()
            };
            return View(model);
        }

        private async Task<int> GetTotalCount(string userId, EnumLeadStatus status)
        {
            var leadCounts = await _service.GetCollection<Lead>(FirestoreTableStore.Leads, "userId", userId);

            return leadCounts.Count(c => c.LeadStatus == status.ToString());
        }
        private string GetPercentile(string userId, int teamMemberGoal, EnumLeadStatus status, int total)
        {
            var target = teamMemberGoal == 0 ? Defaults.DefaultLeadCount : teamMemberGoal;
            var percentile = ((double)total / (double)target) * 100;
            if (percentile > 100) return "100%";
            return $"{Math.Round(percentile)}%";
        }

        public async Task<ActionResult> ManageTeam()
        {
            var userProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, User.UserId);

            var teamMembers = new List<UserProfile>();
            foreach (var teamMemberId in userProfile.TeamMembers)
            {
                var teamMember = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, teamMemberId);
                teamMembers.Add(teamMember);
            }

            var model = new TeamMembersViewModel
            {
                TeamMembers = teamMembers,
                CurrentUserProfile = userProfile
            };
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> Search(string searchText)
        {
            var model = new TeamMembersViewModel();
            if (string.IsNullOrEmpty(searchText)) return PartialView("ManageTeamSearchPanel", model);
            var userProfiles = await _service.GetCollection<UserProfile>(FirestoreTableStore.UserProfiles);

            model.SearchText = searchText;

            model.CurrentUserProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, "userId", User.UserId);

            model.SearchedUserProfiles = userProfiles.Where(p => (!string.IsNullOrEmpty(p.FirstName) && p.FirstName.ToLower().Contains(searchText.ToLower()))
                                                                 || (!string.IsNullOrEmpty(p.LastName) && p.LastName.ToLower().Contains(searchText.ToLower()))
            ).ToList();

            return PartialView("ManageTeamSearchPanel", model);
        }

        [HttpPost]
        public async Task<PartialViewResult> AddTeamMember(string id)
        {
            await _service.Update(User.UserId, FirestoreTableStore.UserProfiles, "teamMembers", FieldValue.ArrayUnion(id));

            var userProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, User.UserId);

            var teamMembers = new List<UserProfile>();
            foreach (var teamMemberId in userProfile.TeamMembers)
            {
                var teamMember = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, teamMemberId);
                teamMembers.Add(teamMember);
            }

            var model = new TeamMembersViewModel
            {
                TeamMembers = teamMembers,
                CurrentUserProfile = userProfile
            };
            return PartialView("ManageTeamGrid", model);

            //return await RefreshPartialView();
        }

        [HttpPost]
        public async Task<PartialViewResult> RemoveTeamMember(string id)
        {
            await _service.Update(User.UserId, FirestoreTableStore.UserProfiles, "teamMembers", FieldValue.ArrayRemove(id));

            return await RefreshPartialView();
        }

        private async Task<PartialViewResult> RefreshPartialView()
        {
            var userProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, User.UserId);

            var teamMembers = new List<UserProfile>();
            foreach (var teamMemberId in userProfile.TeamMembers)
            {
                var teamMember = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, teamMemberId);
                teamMembers.Add(teamMember);
            }

            var model = new TeamMembersViewModel
            {
                TeamMembers = teamMembers,
                CurrentUserProfile = userProfile
            };
            return PartialView("ManageTeamGrid", model);
        }

        public async Task<ActionResult> TeamMemberDetail(string id)
        {
           var model = new TeamMemberViewModel{};
            return View("TeamMemberDetail", model);
        }

        #endregion

        #region Charts

        public async Task<ActionResult> Dashboard(string userId)
        {
            var leadStatusRatio = await  _teamMemberChartDataService.GetLeadStatusRatio(userId ?? User.UserId, DateTime.Now.AddDays(-30), DateTime.Now);
            var leadsPerSource = await _teamMemberChartDataService.GetLeadsPerSource(userId ?? User.UserId);
            var leadsForMonth = await _teamMemberChartDataService.GetDailyLeads(userId ?? User.UserId);
            var leadsForYear = await _teamMemberChartDataService.GetLeadsForYear(userId ?? User.UserId);
            var colors = new Queue<string>(LeadDashboardViewModel.ColorBasket);

            var model = new LeadDashboardViewModel
            {
                LeadSourceData = leadsPerSource.Select(c => new Pie{color = colors.Dequeue(), data = c.Value, label = c.Key}),
                LeadStatusData = leadStatusRatio.Select(c => new Funnel{ y = c.Value, label = c.Key}),
                LeadsForYear = leadsForYear.Select(c => new NightingGale{ name = c.Key, value = c.Value}),
                LeadsForMonth = leadsForMonth.Select(c => new long[] { DateTime.Parse(c.Key).ToJavascriptTimestamp(), c.Value })
            };
            return View("Dashboard", model);
        }

        #endregion

        #region Facets
        
        #endregion
    }
}
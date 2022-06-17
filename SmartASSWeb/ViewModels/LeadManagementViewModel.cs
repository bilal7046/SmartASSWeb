using System.Collections.Generic;
using SmartASSWeb.Bll;

namespace SmartASSWeb.ViewModels
{
    public class LeadManagementViewModel
    {
        public LeadManagementViewModel()
        {
            Notes = new List<LeadNote>();
            Actions = new List<LeadLogAction>();
            LogActionTypes = new List<LookupDataItem>();
            TeamMembers = new List<TeamMemberViewModel>();
        }

        public Lead Lead { get; set; }
        public IEnumerable<LeadNote> Notes { get; set; }
        public IEnumerable<LeadLogAction> Actions { get; set; }

        public string GetLeadStatusClass(string title, string leadStatus)
        {
            switch (title)
            {
                case "Connected" when leadStatus == "Connected":
                case "Qualified" when leadStatus == "Qualified":
                case "Quoted" when leadStatus == "Quoted":
                case "Lost" when leadStatus == "Lost":
                case "OvercameObjections" when leadStatus == "OvercameObjections":
                case "Closed" when leadStatus == "Closed":
                    return "active";
                default:
                    return "disable";
            }
        }

        public string GetCssBar(int leadPercentile)
        {
            if (leadPercentile < 20) return "css-bar-20";
            else if (leadPercentile < 40) return "css-bar-40";
            else if (leadPercentile < 60) return "css-bar-60";
            else if (leadPercentile < 80) return "css-bar-80";
            else return "css-bar-100";
        }

        public IEnumerable<LookupDataItem> LogActionTypes { get; set; }

        public string LogActionType { get; set; }
        public string ActionsNotes { get; set; }
        public string ActionSubject { get; set; }
        public string DueDateString { get; set; }

        public string NotesSubject { get; set; }
        public string NotesText { get; set; }
        public TeamMemberGoal TeamMemberGoal { get; set; }

        public List<TeamMemberViewModel> TeamMembers { get; set; }
    }

    public class TeamMemberViewModel
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PhotoURL { get; set; }
        public string Designation { get; set; }

        public int ConnectedLeads { get; set; }
        public string ConnectedLeadsPercentile { get; set; }
        public int QualifiedLeads { get; set; }
        public string QualifiedLeadsPercentile { get; set; }
        public int QuotedLeads { get; set; }
        public string QuotedLeadsPercentile { get; set; }
        public int ClosedLeads { get; set; }
        public string ClosedLeadsPercentile { get; set; }
        public int LostLeads { get; set; }
        public string LostLeadsPercentile { get; set; }
    }
}
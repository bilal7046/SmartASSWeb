using System.Collections;
using System.Collections.Generic;
using SmartASSWeb.Bll;
using System.Linq;

namespace SmartASSWeb.ViewModels
{
    public class TeamMembersViewModel
    {
        public TeamMembersViewModel()
        {
            TeamMembers = new List<UserProfile>();
            SearchedUserProfiles = new List<UserProfile>();
        }

        public string SearchText { get; set; }
        public List<UserProfile> SearchedUserProfiles { get; set; }
        public List<UserProfile> TeamMembers { get; set; }
        public UserProfile CurrentUserProfile { get; set; }
        public bool IsAlreadyTeamMember(string[] teamMembers, string userId)
        {
            return teamMembers.Contains(userId);
        }
    }
}
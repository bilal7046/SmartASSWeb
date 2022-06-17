using System.Collections.Generic;
using System.Linq;
using SmartASSWeb.Bll;

namespace SmartASSWeb.ViewModels
{
    public class SearchResultViewModel
    {
        public SearchResultViewModel()
        {
            UserProfiles = new List<UserProfile>();
        }
        public UserProfile CurrentUserProfile { get; set; }
        public string SearchText { get; set; }
        public List<UserProfile> UserProfiles { get; set; }

        public bool IsConnected(string[] linkedUsers, string userId)
        {
            return linkedUsers.Contains(userId);
        }
    }
}
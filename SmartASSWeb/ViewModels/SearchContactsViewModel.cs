using System.Collections.Generic;
using System.Linq;
using SmartASSWeb.Bll;

namespace SmartASSWeb.ViewModels
{
    public class SearchContactsViewModel
    {
        public SearchContactsViewModel()
        {
            UserProfiles = new List<UserProfile>();
        }
        public UserProfile CurrentUserProfile { get; set; }
        public string SearchText { get; set; }
        public List<UserProfile> UserProfiles { get; set; }

        public bool IsFollowing(string[] linkedUsers, string userId)
        {
            return linkedUsers.Contains(userId);
        }
    }
}
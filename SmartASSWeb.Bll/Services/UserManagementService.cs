using System.Collections.Generic;
using System.Threading.Tasks;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Bll.Services
{
    public interface IUserManagementService
    {
        Task<IEnumerable<UserProfile>> GetLinkedUsers(string userId);
        Task<IEnumerable<UserProfile>> GetUserProfilesByIds(string[] userIds);
    }
    public class UserManagementService
         : IUserManagementService
    {
        #region Ctor

        private readonly IFirebaseService _service;

        public UserManagementService(IFirebaseService service)
        {
            _service = service;
        }

        #endregion

        public async Task<IEnumerable<UserProfile>> GetLinkedUsers(string userId)
        {
            var currentUser = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, userId);
            var userProfiles = new List<UserProfile>();
            foreach (var linkedUserId in currentUser.LinkedUsers)
            {
                var connectedUser = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, linkedUserId);
                userProfiles.Add(connectedUser);
            }

            return userProfiles;
        }
        public async Task<IEnumerable<UserProfile>> GetUserProfilesByIds(string[] userIds)
        {
            var userProfiles = new List<UserProfile>();
            foreach (var userId in userIds)
            {
                var connectedUser = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, userId);
                userProfiles.Add(connectedUser);
            }
            return userProfiles;
        }
    }
}

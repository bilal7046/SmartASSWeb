using System.Threading.Tasks;
using System.Web.Mvc;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Bll
{
    public static class ProfileImageHelper
    {
        public static string GetProfileImage(string userId)
        {
            IFirebaseService service = DependencyResolver.Current.GetService<IFirebaseService>();
            var userProfile = Task.Run(() => service.Get<UserProfile>(FirestoreTableStore.UserProfiles, userId)).Result;
            if (userProfile == null || string.IsNullOrEmpty(userProfile.PhotoUrl))
                return Defaults.DefaultProfileImage;
            return userProfile.PhotoUrl;
        }
    }
}

using System.Threading.Tasks;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Bll.Services
{
    public interface IPackageResolverService
    {
        Task<Response> SetPackage(string email, EnumPackage package);
        Task<Response<string>> GetPackage(string userId);
        Task<Response<string>> GetPackageByEmail(string email);
    }

    public class PackageResolverService
        : IPackageResolverService
    {
        private readonly IFirebaseService _service;

        public PackageResolverService(IFirebaseService service)
        {
            this._service = service;
        }
        public async Task<Response> SetPackage(string email, EnumPackage package)
        {
            var userProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, "email", email);
            if (userProfile == null)
            {
                return new Response
                {
                    Message = "User Profile not found",
                    IsSuccessful = false
                };
            }
            await _service.Update(userProfile.UserId, FirestoreTableStore.UserProfiles, "package", package);

            return new Response
            {
                Message = "Package successfully set",
                IsSuccessful = true
            };
        }

        public async Task<Response<string>> GetPackage(string userId)
        {
            var userProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, userId);

            if (userProfile == null || string.IsNullOrEmpty(userProfile.Package))
            {
                return new Response<string>
                {
                    IsSuccessful = false,
                    Message = "Package not found"
                };
            }
            return new Response<string>
            {
                Data = userProfile.Package,
                Message = "Package returned successfully",
                IsSuccessful = true
            };
        }
        public async Task<Response<string>> GetPackageByEmail(string email)
        {

            var userProfile = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, "email", email);
            if (userProfile == null)
            {
                return new Response<string>
                {
                    Message = "User Profile not found",
                    IsSuccessful = false
                };
            }
            return new Response<string>
            {
                Data = userProfile.Package,
                Message = "Package returned successfully",
                IsSuccessful = true
            };
        }
    }
}

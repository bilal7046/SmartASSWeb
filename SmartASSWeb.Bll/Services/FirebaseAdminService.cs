using System;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using SmartASSWeb.Bll.Core;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Bll.Services
{
    public interface IFirebaseAdminService
    {
        Task<Response<UserRecord>> RegisterUser(string email, string phone, string mobile, string firstName, string lastName, string gender, string employeeCode);
    }
    public class FirebaseAdminService
        : IFirebaseAdminService
    {
        private readonly IFirebaseService _firebaseService;
        private readonly IUniqueCodeGenerator _uniqueCodeGenerator;

        public FirebaseAdminService(IFirebaseService firebaseService, IKeyFileResolver keyFileResolver, IUniqueCodeGenerator uniqueCodeGenerator)
        {
            _firebaseService = firebaseService;
            _uniqueCodeGenerator = uniqueCodeGenerator;
        }
        public async Task<Response<UserRecord>> RegisterUser(string email, string phone, string mobile, string firstName, string lastName, string gender, string employeeCode)
        {
            // User Data with valid details
            var args = new UserRecordArgs()
            {
                Email = email,
                EmailVerified = true,
                PhoneNumber = phone,
                Password = _uniqueCodeGenerator.GeneratePassword(8),
                DisplayName = $"{firstName} {lastName}",
                Disabled = false,
            };
            try
            {
                UserRecord createdUser = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);
                
                var userProfile = new UserProfile
                {
                    UserId = createdUser.Uid,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Gender = gender == "M" ? "Male" : "Female",
                    Phone = phone,
                    Mobile = mobile,
                    EmployeeCode = employeeCode,
                    CreatedAt = Timestamp.FromDateTime(DateTime.UtcNow),
                    Package = EnumPackage.Individual.ToString()
                };
                await _firebaseService.Add(FirestoreTableStore.UserProfiles, userProfile);

                return new Response<UserRecord>
                {
                    IsSuccessful = true,
                    Message = "User created successfully",
                    Data = createdUser
                };
            }
            catch (Exception ex)
            {
                return new Response<UserRecord>
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }
    }
}

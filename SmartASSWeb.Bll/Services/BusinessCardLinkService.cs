using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Bll.Services
{
    public interface IBusinessCardLinkService
    {
        Task ConnectToCard(string businessCardId, string userId);
        Task UnconnectToCard(string businessCardId, string userId);
    }
    public class BusinessCardLinkService
        : IBusinessCardLinkService
    {
        private readonly IFirebaseService _service;
        private readonly IActivityService _activityService;
        private readonly INotificationService _notificationService;

        public BusinessCardLinkService(IFirebaseService service, IActivityService activityService, INotificationService notificationService)
        {
            _service = service;
            _activityService = activityService;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Connect to Business Card
        /// - Add user as a connected user to Business Card
        /// - Log activity of request acceptance
        /// - Send notification to user of acceptance
        /// - Link user as part of my linked users
        /// </summary>
        /// <param name="businessCardId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task ConnectToCard(string businessCardId, string userId)
        {
            var currentUser = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, userId);
            var currentBusinessCard = await _service.Get<BusinessCard>(FirestoreTableStore.BusinessCards, businessCardId);
            //
            // Add to list of connected users
            if (!currentBusinessCard.ConnectedUsers.Contains(userId))
            {
                await _service.Update(businessCardId, FirestoreTableStore.BusinessCards, "connectedUsers", FieldValue.ArrayUnion(userId));
            }
            // Add card to list of cards I can view
            if (!currentUser.ConnectedBusinessCards.Contains(businessCardId))
            {
                await _service.Update(userId, FirestoreTableStore.UserProfiles, "connectedBusinessCards", FieldValue.ArrayUnion(businessCardId));
            }
            //
            var activity = new Activity
            {
                ActivityTime = Timestamp.GetCurrentTimestamp(),
                ActivityType = EnumActivity.RequestAccepted.ToString(),
                ActivityCreator = userId,
                ActivityOwner = currentBusinessCard.UserId,
                ActivityMessage = "Request has been accepted by creator",
                ActivityBusiness = currentBusinessCard.BusinessCardId,
                ActivityCreatorImage = currentUser.PhotoUrl
            };
            await _activityService.LogActivity(activity);
            //
            await _notificationService.SendRequestAcceptNotification(currentBusinessCard.UserId, userId);
            //
            // If current user is not already connected to me, add them as my linked users
            if (!currentUser.LinkedUsers.Contains(currentBusinessCard.UserId))
            {
                await _service.Update(userId, FirestoreTableStore.UserProfiles, "linkedUsers", FieldValue.ArrayUnion(currentBusinessCard.UserId));
            }
                
        }

        /// <summary>
        /// Unconnect user from Business Card
        /// </summary>
        /// <param name="businessCardId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task UnconnectToCard(string businessCardId, string userId)
        {
            var currentUser = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, userId);
            var currentBusinessCard = await _service.Get<BusinessCard>(FirestoreTableStore.BusinessCards, businessCardId);
            //
            // Remove user from card user list
            if (currentBusinessCard.ConnectedUsers.Contains(userId))
            {
                await _service.Update(businessCardId, FirestoreTableStore.BusinessCards, "connectedUsers", FieldValue.ArrayRemove(userId));
            }
            // Remove card from card I can view
            if (currentUser.ConnectedBusinessCards.Contains(businessCardId))
            {
                await _service.Update(userId, FirestoreTableStore.UserProfiles, "connectedBusinessCards", FieldValue.ArrayRemove(businessCardId));
            }
            //
            // If current user is not already connected to me, add them as my linked users
            if (currentUser.LinkedUsers.Contains(currentBusinessCard.UserId))
            {
                await _service.Update(userId, FirestoreTableStore.UserProfiles, "linkedUsers", FieldValue.ArrayRemove(currentBusinessCard.UserId));
            }
        }
    }
}

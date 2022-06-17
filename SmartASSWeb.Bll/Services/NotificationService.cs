using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Bll.Services
{
    public interface INotificationService
    {
        Task<Response> SendAssociateRequestNotification(string requestorUserId, string receiverUserId);
        Task<Response> SendShareMyCardNotification(string requestorUserId, string receiverUserId);
        Task<Response> SendEmail(string subject, string toAddress, string fromAddress, string message);
        Task<Response> SendRequestAcceptNotification(string requestorUserId, string acceptedUserId);
        Task<Response> SendNotification(Notification notification);
    }
    public class NotificationService
        : INotificationService
    {
        private readonly IFirebaseService _firebaseService;

        public NotificationService(IFirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }
        public async Task<Response> SendAssociateRequestNotification(string requestorUserId, string receiverUserId)
        {
            var currentUser = await _firebaseService.Get<UserProfile>(FirestoreTableStore.UserProfiles, "userId", requestorUserId);
            var notification = new Notification
            {
                Id = Guid.NewGuid().ToString(),
                DateCreated = Timestamp.FromDateTime(DateTime.Now),
                Message = $"Request to link with '{currentUser.FirstName} {currentUser.LastName}'. Do you accept or decline?",
                UserId = receiverUserId,
                RequestorUserId = requestorUserId,
                RequestorName = $"{currentUser.FirstName} {currentUser.LastName}",
                RequestorPhotoUrl = currentUser.PhotoUrl,
                NotificationType = EnumNotificationType.REQUEST.ToString(),
                IsRead = false,
                Data = null
            };
            await _firebaseService.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Notifications, notification);

            return new Response{IsSuccessful = true, Message = "Notification sent successfully!"};
        }

        public async Task<Response> SendShareMyCardNotification(string requestorUserId, string receiverUserId)
        {
            var currentUser = await _firebaseService.Get<UserProfile>(FirestoreTableStore.UserProfiles, "userId", requestorUserId);
            var notification = new Notification
            {
                Id = Guid.NewGuid().ToString(),
                DateCreated = Timestamp.FromDateTime(DateTime.Now),
                Message = $"'{currentUser.FirstName} {currentUser.LastName}' has shared their card with you: \n\n Smart-ASS: <a href=\"{currentUser.ProfileIDUrl}\">{currentUser.ProfileIDUrl}</a>",
                UserId = receiverUserId,
                RequestorUserId = requestorUserId,
                RequestorName = $"{currentUser.FirstName} {currentUser.LastName}",
                RequestorPhotoUrl = currentUser.PhotoUrl,
                NotificationType = EnumNotificationType.MESSAGE.ToString(),
                IsRead = false,
                Data = null
            };
            await _firebaseService.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Notifications, notification);

            return new Response{IsSuccessful = true, Message = "Notification sent successfully!"};
        }
        public async Task<Response> SendRequestAcceptNotification(string requestorUserId, string receiverUserId)
        {
            var currentUser = await _firebaseService.Get<UserProfile>(FirestoreTableStore.UserProfiles, "userId", requestorUserId);
            var notification = new Notification
            {
                Id = Guid.NewGuid().ToString(),
                DateCreated = Timestamp.FromDateTime(DateTime.Now),
                Message = $"'{currentUser.FirstName} {currentUser.LastName}' has accepted your request.",
                UserId = receiverUserId,
                RequestorUserId = requestorUserId,
                RequestorName = $"{currentUser.FirstName} {currentUser.LastName}",
                RequestorPhotoUrl = currentUser.PhotoUrl,
                NotificationType = EnumNotificationType.MESSAGE.ToString(),
                IsRead = false,
                Data = null
            };
            await _firebaseService.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Notifications, notification);

            return new Response { IsSuccessful = true, Message = "Notification sent successfully!" };
        }

        public async Task<Response> SendNotification(Notification notification)
        {
            await _firebaseService.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Notifications, notification);
            return new Response { IsSuccessful = true, Message = "Notification sent successfully!" };
        }

        public async Task<Response> SendEmail(string subject, string toAddress, string fromAddress, string message)
        {
            var mailMessage = new MailMessage
            {
                Subject = subject,
                To = { new MailAddress(toAddress) },
                From = new MailAddress(fromAddress),
                IsBodyHtml = true,
                Body = message
            };
            using (var smtp = new SmtpClient())
            {
                smtp.Host = "mail.firsttech.net";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("noreply@ecert.co.za", "RepE123!");
                await smtp.SendMailAsync(mailMessage);
            }

            return new Response{IsSuccessful = true, Message = "Email sent successfully!"};
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Google.Cloud.Firestore;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Core;
using SmartASSWeb.Bll.Services;
using SmartASSWeb.Core.Service;
using SmartASSWeb.ViewModels;

namespace SmartASSWeb.Controllers
{
    public class ChatController 
        : ControllerBase
    {
        #region Ctor

        private readonly IFirebaseService _service;
        private readonly IChatService _chatService;
        private readonly IUserManagementService _userService;
        private readonly IUniqueCodeGenerator _uniqueCodeGenerator;
        private readonly INotificationService _notificationService;

        public ChatController(IFirebaseService service, IChatService chatService, IUserManagementService userService, IUniqueCodeGenerator uniqueCodeGenerator, INotificationService notificationService)
        {
            _service = service;
            _chatService = chatService;
            _userService = userService;
            _uniqueCodeGenerator = uniqueCodeGenerator;
            _notificationService = notificationService;
        }

        #endregion

        public async Task<ActionResult> Index()
        {
            //
            //var chats = await _service.GetCollection<Chat>(FirestoreTableStore.Chats, "userId", User.UserId);
            //TODO - Code smell
            var allChats = await _service.GetCollection<Chat>(FirestoreTableStore.Chats);
            var myChats = allChats.Where(c => c.UserId == User.UserId);
            var chatsIBelongTo = allChats.Where(c => c.Participants.Contains(User.UserId));
            var totalChats = myChats.Union(chatsIBelongTo).OrderByDescending(c=> c.DateCreated);

            var model = new ChatViewModel
            {
                Chats = totalChats
            };
            return View(model);
        }

        public async Task<ActionResult> Chat()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoadChat(string chatRoomId)
        {
            var chat = await _chatService.GetChat(chatRoomId);
            var participants = await _userService.GetUserProfilesByIds(chat.Participants);
            var model = new ChatViewModel
            {
                CurrentChat = chat,
                Contacts = await _userService.GetLinkedUsers(User.UserId),
                Participants = string.Join("; ", participants.Select(p=> p.FullName))
            };
            return PartialView("ChatPanel",model);
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage(string chatRoomId, string message)
        {
            //get chat, update chat room, add message
            var chat = await _service.Get<Chat>(FirestoreTableStore.Chats, chatRoomId);
            
            await _service.Update(chat.ChatId, FirestoreTableStore.Chats, chat.ToDictionary());

            var currentUser = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, User.UserId);
            var chatMessage = new ChatMessage
            {
                UserId = currentUser.UserId,
                PhotoUrl = currentUser.PhotoUrl,
                Message = message,
                DateCreated = Timestamp.GetCurrentTimestamp(),
                ChatMessageId = Guid.NewGuid().ToString(),
                FullName = currentUser.FullName
            };
            await _service.Update(chat.ChatId, FirestoreTableStore.Chats, "chatMessages", FieldValue.ArrayUnion(chatMessage));
            
            return Json("Message Sent");
        }

        //[HttpPost]
        public async Task<ActionResult> AddChat(string chatName)
        {
            if (string.IsNullOrEmpty(chatName)) return Json("No chat name entered");
            var chatId = _uniqueCodeGenerator.Generate(7);
            var chat = new Chat
            {
                ChatId = chatId,
                Name = chatName,
                DateCreated = Timestamp.GetCurrentTimestamp(),
                Participants = new [] { User.UserId },
                UserId = User.UserId,
                ChatMessages = new ChatMessage[] { }
            };

            await _service.Add(chatId, FirestoreTableStore.Chats, chat);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string chatId)
        {
            await _service.Delete(chatId, FirestoreTableStore.Chats);

            return Json("Chat deleted successfully!");
        }

        [HttpPost]
        public async Task<ActionResult> AddParticipant(ParticipantViewModel model)
        {
            var currentUser = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, User.UserId);
            var chat = await _service.Get<Chat>(FirestoreTableStore.Chats, model.CurrentChatId);

            if (Array.IndexOf(chat.Participants, model.ParticipantId) > -1) return Json("User already added");

            await _service.Update(chat.ChatId, FirestoreTableStore.Chats, "participants", FieldValue.ArrayUnion(model.ParticipantId));
            //
            var notification = new Notification
            {
                UserId = model.ParticipantId,
                Message = $"New chat request from '{currentUser.FullName}'",
                IsRead = false,
                DateCreated = Timestamp.GetCurrentTimestamp(),
                NotificationType = EnumNotificationType.ALERT.ToString(),
                RequestorName = currentUser.FullName,
                RequestorUserId = currentUser.UserId,
                RequestorPhotoUrl = currentUser.PhotoUrl
            };
            await _notificationService.SendNotification(notification);

            var chat2 = await _service.Get<Chat>(FirestoreTableStore.Chats, model.CurrentChatId);
            var participants = await _userService.GetUserProfilesByIds(chat2.Participants);
            var chatViewModel = new ChatViewModel
            {
                CurrentChat = chat2,
                Contacts = await _userService.GetLinkedUsers(User.UserId),
                Participants = string.Join("; ", participants.Select(p => p.FullName))
            };
            return Json(chatViewModel);
        }
    }
}
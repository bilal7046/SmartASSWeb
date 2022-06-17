using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Bll.Services
{
    public interface IChatService
    {
        Task<Chat> GetChat(string chatId);
    }
    public class ChatService
        : IChatService
    {
        #region Ctor

        private readonly IFirebaseService _service;

        public ChatService(IFirebaseService service)
        {
            _service = service;
        }

        #endregion
        public async Task<Chat> GetChat(string chatId)
        {
            var chat = await _service.Get<Chat>(FirestoreTableStore.Chats, chatId);
            var participants = new List<UserProfile>();
            foreach (var chatParticipant in chat.Participants)
            {
                var chatPerson = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, chatParticipant);
                participants.Add(chatPerson);
            }

            chat.ChatMessages = chat.ChatMessages.Select(c => new ChatMessage
            {
                UserId = c.UserId,
                PhotoUrl = c.PhotoUrl,
                FullName = participants.FirstOrDefault(i => i.UserId == c.UserId)?.FullName,
                DateCreated = c.DateCreated,
                Message = c.Message,
                ChatMessageId = c.ChatMessageId
            }).ToArray();

            return chat;
        }
    }
}

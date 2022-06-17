using System.Collections.Generic;
using SmartASSWeb.Bll;

namespace SmartASSWeb.ViewModels
{
    public class ChatViewModel
    {
        public ChatViewModel()
        {
            CurrentChat = new Chat();
            Contacts = new List<UserProfile>();
            Chats = new List<Chat>();
        }

        public Chat CurrentChat { get; set; }
        public string ChatName { get; set; }
        public string ParticipantId { get; set; }
        public IEnumerable<UserProfile> Contacts { get; set; }
        public IEnumerable<Chat> Chats { get; set; }
        public string Participants { get; set; }
    }

    public class ParticipantViewModel
    {
        public string CurrentChatId { get; set; }
        public string ParticipantId { get; set; }
    }
}
using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class Chat
    {
        public Chat()
        {
            ChatMessages = new ChatMessage[] { };
            Participants = new string[] { };
        }

        [FirestoreDocumentId]
        public string ChatId { get; set; }
        [FirestoreProperty("name")]
        public string Name { get; set; }
        [FirestoreProperty("userId")]
        public string UserId { get; set; }
        [FirestoreProperty("dateCreated")]
        public Timestamp DateCreated { get; set; }

        [FirestoreProperty("chatMessages")]
        public ChatMessage[] ChatMessages { get; set; }
        [FirestoreProperty("participants")]
        public string[] Participants { get; set; }
    }

    [FirestoreData]
    public class ChatMessage
    {
        [FirestoreDocumentId]
        public string ChatMessageId { get; set; }
        [FirestoreProperty("message")]
        public string Message { get; set; }
        [FirestoreProperty("dateCreated")]
        public Timestamp DateCreated { get; set; }
        [FirestoreProperty("userId")]
        public string UserId { get; set; }
        [FirestoreProperty("fullName")]
        public string FullName { get; set; }
        [FirestoreProperty("photoUrl")]
        public string PhotoUrl { get; set; }
        //
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace SmartASSWeb.Dal.Entities
{
    public class ChatMessage : EntityBase
    {

        [Key]
        public int ChatMessageId { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
        //
    }
}
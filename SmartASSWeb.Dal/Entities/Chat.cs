using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace SmartASSWeb.Dal.Entities
{
    public class Chat : EntityBase
    {
        [Key]
        public string ChatId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string UserId { get; set; }
        
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage> { };

    [NotMapped]
        public ICollection<string> Participants { get; set; } = new List<string>();
        /// <summary> <see cref="Participants"/> for database persistence. </summary>
        [Obsolete("Only for Persistence by EntityFramework")]
        public string ParticipantList
        {
            get => Participants == null || !Participants.Any()
                ? null
                : JsonConvert.SerializeObject(Participants);

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    Participants.Clear();
                else
                    Participants = JsonConvert.DeserializeObject<ICollection<string>>(value);
            }
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SmartASSWeb.Dal.Entities
{
    
    public class CalendarEvent : EntityBase
    {
        [Key]
        public string CalendarEventId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("start")]
        public DateTime Start { get; set; }
        [JsonProperty("end")]
        public DateTime End { get; set; }
        [JsonProperty("className")]
        public string ClassName { get; set; }
        [JsonProperty("allDay")]
        public bool AllDay { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}

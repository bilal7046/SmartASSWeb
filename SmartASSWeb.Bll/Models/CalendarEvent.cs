using System;
using Google.Cloud.Firestore;
using Google.Protobuf.WellKnownTypes;
//using Kendo.Mvc.UI;
using Timestamp = Google.Cloud.Firestore.Timestamp;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class CalendarEvent
   //     : ISchedulerEvent
    {
        #region Fields

        private DateTime _start;
        private DateTime _end;

        #endregion

        [FirestoreDocumentId]
        public string CalendarEventId { get; set; }
        [FirestoreProperty("title")]
        public string Title { get; set; }
        [FirestoreProperty("description")]
        public string Description { get; set; }

        [FirestoreProperty("startTimestamp")]
        public Timestamp StartTimestamp { get; set; }

        [FirestoreProperty("endTimestamp")]
        public Timestamp EndTimestamp { get; set; }
        [FirestoreProperty("isAllDay")]
        public bool IsAllDay { get; set; }
        [FirestoreProperty("userId")]
        public string UserId { get; set; }
        public DateTime Start
        {
            get => _start == default(DateTime) ? StartTimestamp.ToDateTime() : _start;
            set
            {
                _start = value;
                this.StartTimestamp = Timestamp.FromDateTime(_start.ToUniversalTime());
            }
        }
        public DateTime End
        {
            get => _end == default(DateTime) ? EndTimestamp.ToDateTime() : _end;
            set
            {
                _end = value;
                this.EndTimestamp = Timestamp.FromDateTime(_end.ToUniversalTime());
            }
        }
        [FirestoreProperty("startTimezone")]
        public string StartTimezone { get; set; }
        [FirestoreProperty("endTimezone")]
        public string EndTimezone { get; set; }
        [FirestoreProperty("recurrenceRule")]
        public string RecurrenceRule { get; set; }
        [FirestoreProperty("recurrenceException")]
        public string RecurrenceException { get; set; }

    }
}

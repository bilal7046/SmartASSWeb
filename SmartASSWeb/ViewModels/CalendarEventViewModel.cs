using System;
using System.Collections.Generic;
using SmartASSWeb.Bll;

namespace SmartASSWeb.ViewModels
{
    public class CalendarEventViewModel
    {
        public CalendarEventViewModel()
        {
            CalendarEvents = new List<CalendarEvent>();
        }

        public IEnumerable<CalendarEvent> CalendarEvents { get; set; }

    }
}
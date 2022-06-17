
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.Mvc;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll._Mock;
using SmartASSWeb.Bll.Core;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Controllers
{
    public class ControllerBase
        : Controller
    {
        public ControllerBase()
        {
            //var notifications = Task.Run(GetNotifications).Result;
            ViewBag.NotificationCount = 0; //notifications.Count();
            ViewBag.Notifications = new List<Notification>(); //notifications.Where(c=> !c.IsRead).OrderByDescending(c => c.DateCreated).Take(5);
        }

        private Task<IEnumerable<Notification>> GetNotifications()
        {
            IFirebaseService service = new FirebaseService(new KeyFileResolver());
            return service.GetCollection<Notification>(FirestoreTableStore.Notifications, "userId", User.UserId);
        }

        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
        
        protected void GetDateRange(string dateRange, out DateTime startDate, out DateTime endDate)
        {
            //Demo User
            if (User.UserId == MockDataGenerator.DemoUser)
            {
                startDate = new DateTime(2020, 10, 1);
                endDate = new DateTime(2020, 12, 31);
            }
            else if (string.IsNullOrEmpty(dateRange))
            {
                startDate = DateTime.Now.AddDays(-30);
                endDate = DateTime.Now;
            }
            else
            {
                var dates = dateRange.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                startDate = DateTime.Parse(dates[0], CultureInfo.CurrentCulture);
                endDate = DateTime.Parse(dates[1], CultureInfo.CurrentCulture);
            }
        }
    }
}
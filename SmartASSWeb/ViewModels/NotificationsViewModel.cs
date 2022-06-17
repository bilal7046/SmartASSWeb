using System.Collections.Generic;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Core;

namespace SmartASSWeb.ViewModels
{
    public class NotificationsViewModel
    {
        public NotificationsViewModel()
        {
            Notifications = new List<Notification>();
        }
        public string SearchText { get; set; }
        public Notification CurrentNotification { get; set; } = new Notification();
        public IEnumerable<Notification> Notifications { get; set; }

        public string GetBadge(string notificationType)
        {
            string badge = string.Empty;
            if (notificationType.Equals(EnumNotificationType.REQUEST.GetDescription()))
            {
                badge = "badge-success";
            }else if (notificationType.Equals(EnumNotificationType.ALERT.GetDescription()))
            {
                badge = "badge-danger";
            }
            else if (notificationType.Equals(EnumNotificationType.MESSAGE.GetDescription()))
            {
                badge = "badge-warning";
            }
            return badge;
        }
    }
}
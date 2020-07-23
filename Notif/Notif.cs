using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using System.Runtime;

namespace Notif
{
    public class NotifC
    {
        public void createNotif(string title, string subtitle, string AppName)
        {
            var notificationxml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            var toastele = notificationxml.GetElementsByTagName("text");
            toastele[0].AppendChild(notificationxml.CreateTextNode(title));
            toastele[1].AppendChild(notificationxml.CreateTextNode(subtitle));


            var toastnote = new ToastNotification(notificationxml);
            ToastNotificationManager.CreateToastNotifier(AppName).Show(toastnote);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
namespace iApprove.Interface
{
    public interface IPushNotificationHandler
    {
        int NotificationId { get; set; }

        string RegistrationToken { get; set; }

        void RegisterNotification(Action<object> callback);

        void UnRegisterNotification(Action<object> callback);
    }
}

/**
 * Description: This class is used for handling Push Notificaiton in Android Platform, 
 * it utilizes Android GCM push notification upon Microsofot provided library
 * Created By: TCS
 * Creatd Date: 23-03-2017
 * CopyRightto: Cummins
 * */
using System;
using Android.App;
using Android.Content;
using Gcm.Client;
using System.Threading.Tasks;
using Android.Util;
using AndroidContext = Android.Content.Context;
using iApprove.Interface;

#region "Manifest Settings"
[assembly: Permission(Name = "com.iApprove.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.iApprove.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
#endregion
[assembly: Xamarin.Forms.Dependency(typeof(iApprove.Droid.DependencyService.PushNotificationHandler))]
namespace iApprove.Droid.DependencyService
{
    [Service(Exported = false)]
    public class PushNotificationHandler : GcmServiceBase, IPushNotificationHandler
    {
        #region "Global Members, Constants"
        public const string LOG_TAG = "PushNotificationHandler";
        public const string ClientId = "708649545724";
        Action<bool> CallBack;
        private int _NotificationId;
        private string _RegistrationToken;
        private static IPushNotificationHandler myPushHandler;
        #endregion

        #region "Constructor"
        /// <summary>
        /// Default Constructor, we need to set AppContext through property
        /// </summary>
        public PushNotificationHandler() : base(ClientId)
        {
        }

        /// <summary>
        /// Get Single instance of the Pushnotification Listener
        /// </summary>
        /// <param name="appContext"></param>
        /// <returns></returns>
        public static IPushNotificationHandler GetInstance()
        {
            if (myPushHandler == null)
            {
                myPushHandler = new PushNotificationHandler();
                App.PushNotifiHandler = myPushHandler;
            }
            return myPushHandler;
        }
        #endregion

        #region "Properties"
        public int NotificationId
        {
            get
            {
                return _NotificationId;
            }

            set
            {
                _NotificationId = value;
            }
        }


        public string RegistrationToken
        {
            get
            {
                return _RegistrationToken;
            }

            set
            {
                _RegistrationToken = value;
            }
        }
        #endregion

        #region "Methods"
        /// <summary>
        /// Used to Call this method to start register push notification
        /// </summary>
        /// <param name="callback">Pass function pointer to receive result of successful registration</param>
        public void RegisterNotification(Action<object> callback)
        {
            Log.Info(LOG_TAG, "Request for Register Notification");
            Task.Run(() =>
            {
                try
                {
                    Log.Info(LOG_TAG, "Registering...");
                    GcmClient.CheckDevice(Application.Context);
                    GcmClient.CheckManifest(Application.Context);
                    GcmClient.Register(Application.Context, PushNotificationHandler.ClientId);
                    if(callback!=null)
                    {
                        callback("Registration Success");
                    }
                }
                catch (Exception ex)
                {

                }
            });
        }

        /// <summary>
        /// Used to call this method to unregister push notification
        /// </summary>
        /// <param name="callback">Pass function pointer to receive result of successful unregistration</param>
        public async void UnRegisterNotification(Action<object> callback)
        {
            Log.Info(LOG_TAG, "Request for Unregister Notification");
            var notificationManager = (NotificationManager)Application.Context.GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CancelAll();
            GcmClient.UnRegister(Application.Context);

            if (myPushHandler != null)
            {
                //Write code her to unregister notification id to app server
            }
            if (callback != null)
            {
                callback("Unegister Success");
            }
        }

        /// <summary>
        /// Used to handle all GCM client related errors
        /// </summary>
        /// <param name="context"></param>
        /// <param name="errorId"></param>
        protected override void OnError(Context context, string errorId)
        {
            Log.Info(LOG_TAG, "GCM Message Error!");
        }

        /// <summary>
        /// Used to handle all incomming push notifications and process it
        /// </summary>
        /// <param name="context"></param>
        /// <param name="intent"></param>
        protected override void OnMessage(Context context, Intent intent)
        {
            Log.Info(LOG_TAG, "GCM Message Received!");

            //Read Content from intent
            //example : Id = intent.Extras.GetString("id"),
            CreateNotification(null);
        }

        /// <summary>
        /// Used to handle successful GCM registration and extract registration ID
        /// </summary>
        /// <param name="context"></param>
        /// <param name="registrationId"></param>
        protected async override void OnRegistered(Context context, string registrationId)
        {
            Log.Info(LOG_TAG, "GCM Registration successful!");

            _RegistrationToken = registrationId;
            if (myPushHandler == null)
            {
                myPushHandler = new PushNotificationHandler();
            }
            myPushHandler.RegistrationToken = registrationId;


            //Subcribe application Notification
            //Code her to register notification id to app server
        }

        /// <summary>
        /// Used to handle unregister notificaiton scenario
        /// </summary>
        /// <param name="context"></param>
        /// <param name="registrationId"></param>
        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Verbose(LOG_TAG, "GCM Unregistered: " + registrationId);
        }
        #endregion

        #region "Supporting Methods"
        private void CreateNotification(Object e)
        {
            try
            {
                string subject, body;
                if (!string.IsNullOrEmpty("Subject"))
                {
                    subject = "Subject";
                    body = "Message";
                }
                else
                {
                    subject = "Cummins Cirrus";
                    body = "Message";
                }

                var resultIntent = new Intent(this, typeof(MainActivity));
                resultIntent.PutExtra("fragment", "NotificationFragment");
                resultIntent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
                var pendingIntent = PendingIntent.GetActivity(this, 0, resultIntent, PendingIntentFlags.UpdateCurrent);
                CreateNotification(subject, body, pendingIntent);
            }
            catch (Exception ex)
            {

            }
        }

        private void CreateNotification(string subject, string body, PendingIntent next)
        {

            #region BigNotification

            Notification.BigTextStyle textStyle = new Notification.BigTextStyle();

            textStyle.BigText(body);
            // Set the summary text:
            textStyle.SetSummaryText("Cummins Cirrus");

            var notificationBuilder = new Notification.Builder(Application.Context)

                .SetContentTitle(subject)
                .SetContentText(body)
                .SetAutoCancel(true)
                .SetContentIntent(next)
                    .SetPriority((int)NotificationPriority.Max)
                .SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate | NotificationDefaults.Lights)
                .SetStyle(textStyle);

            if ((int)Android.OS.Build.VERSION.SdkInt >= 21)
            {
                notificationBuilder.SetVisibility(NotificationVisibility.Public);
                notificationBuilder.SetCategory(Notification.CategoryAlarm);
                //notificationBuilder.SetSmallIcon(Resource.Drawable.transparentWhiteNotification); //TODO: Change small notificaiotn icon here
                notificationBuilder.SetColor(Resources.GetColor(Resource.Color.red));
                notificationBuilder.SetVibrate(new long[] { 100, 200, 300 });
            }
            else
            {
                //notificationBuilder.SetSmallIcon(Resource.Drawable.notificationIcon); //TODO: Change small notification icon here
            }
            var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
            notificationManager.Notify(NotificationId++, notificationBuilder.Build());

            #endregion BigNotification
        }
    }
    #endregion

    #region "Manifest Settings"
    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE },
     Categories = new[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
     Categories = new[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
     Categories = new[] { "@PACKAGE_NAME@" })]
    #endregion
    public class PushNotificationListener : GcmBroadcastReceiverBase<PushNotificationHandler>
    {
        public static string[] SenderIds = new string[] { PushNotificationHandler.ClientId };
    }
}

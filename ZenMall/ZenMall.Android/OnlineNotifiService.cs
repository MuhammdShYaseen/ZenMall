using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ZenMall.Droid
{
    [Service]
    class OnlineNotifiService : Service
    {
        bool IsExist=false;
        int notyid = 0;
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public override void OnTaskRemoved(Intent rootIntent)
        {
            base.OnTaskRemoved(rootIntent);
            new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        StartServiceInForeground();
                    }
                    catch
                    {
                    }
                    System.Threading.Thread.Sleep(20000);
                }
            }).Start();
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        StartServiceInForeground();
                    }
                    catch
                    {

                    }
                    System.Threading.Thread.Sleep(20000);
                }
            }).Start();
        }
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {          
            new Task(() =>
            {
                while (true)
                {
                    try
                    {
                      StartServiceInForeground();
                    }
                    catch 
                    {                        
                    }
                    System.Threading.Thread.Sleep(20000);
                }
            }).Start();
            return StartCommandResult.Sticky;
        }
        async void StartServiceInForeground()
        {
            try
            {           
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var intent = new Intent(this, typeof(MainActivity));
                var channel = new NotificationChannel(CHANNEL_ID, "Service Channel", NotificationImportance.High)
                {
                    Description = "Foreground Service Channel"
                };
                SQLLiteQ ss = new SQLLiteQ();
                IEnumerable<NotificationM> notilist = await  NotiSource();
                foreach (var a in notilist)
                {
                IsExist =ss.Select(a.NotiDiscraption.ToString());
                    if (IsExist == true)
                {                   
                    foreach(var t in notilist)
                    {
                        Intent intent2 = new Intent(this, typeof(MainActivity));
                        const int pendingIntentId = 0;
                        PendingIntent pendingIntent = PendingIntent.GetActivity(this, pendingIntentId, intent2, PendingIntentFlags.OneShot);
                        string TitelSQL = t.Notititel.ToString();
                        string disSQL = t.NotiDiscraption.ToString();
                        var notification = new Notification.Builder(this, CHANNEL_ID)
                            .SetContentTitle(TitelSQL)
                            .SetContentText(disSQL)
                            .SetContentIntent(pendingIntent)
                            .SetSmallIcon(Resource.Drawable.shoppingcart)
                            .Build();
                    NotificationManager notificationManager =
                        GetSystemService(Context.NotificationService) as NotificationManager;                
                    notyid += 1;
                    notificationManager.Notify(notyid, notification);
                            ss.Insert(t.Notititel, t.NotiDiscraption, t.NotiImage);
                    }
                            IsExist = false;                  
                }
                }
            }
            else
            {
                var intent1 = new Intent(this, typeof(MainActivity));
                SQLLiteQ ss = new SQLLiteQ();
                IEnumerable<NotificationM> notilist = await NotiSource();
                foreach (var a in notilist)
                {
                    IsExist = ss.Select(a.NotiDiscraption.ToString());
                    if (IsExist == true)
                    {
                        foreach (var t in notilist)
                        {
                            Intent intent2 = new Intent(this, typeof(MainActivity));
                            const int pendingIntentId = 0;
                            PendingIntent pendingIntent = PendingIntent.GetActivity(this, pendingIntentId, intent2, PendingIntentFlags.OneShot);
                            string TitelSQL = t.Notititel.ToString();
                            string disSQL = t.NotiDiscraption.ToString();
                            var notification = new Notification.Builder(this)
                                .SetContentTitle(TitelSQL)
                                .SetContentText(disSQL)
                                .SetContentIntent(pendingIntent)
                                .SetSmallIcon(Resource.Drawable.shoppingcart)
                                .Build();
                            NotificationManager notificationManager =
                                GetSystemService(Context.NotificationService) as NotificationManager;
                            notyid += 1;
                            notificationManager.Notify(notyid, notification);
                            ss.Insert(t.Notititel, t.NotiDiscraption, t.NotiImage);
                        }
                    }
                }
            }
            Device.StartTimer(TimeSpan.FromSeconds(300), () =>
            {
                try
                {                 
                }
                catch 
                {
                }
                return true;
            });
            }
            catch
            {
            }
    }
        public async Task<IEnumerable<NotificationM>> NotiSource()
        {
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Add("ApiKey", "ReadAllProdactsAndNotificationAndPostClientRec");
            var resultJson = await httpclient.GetStringAsync("https://zainmall.sy/api/Notifacations");
            List<NotificationM> resultforecast;
            resultforecast = JsonConvert.DeserializeObject<List<NotificationM>>(resultJson);
            return resultforecast;
        }
    }
}
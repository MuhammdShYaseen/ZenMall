using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using Xamarin.Forms;
using Android.Content;

namespace ZenMall.Droid
{//Label = "ZenMall",

    [Activity( Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            CreateNotificationChannel();
            loadservice();

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                Window.SetStatusBarColor(Android.Graphics.Color.Purple);
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            //RequestPermissions(new string[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation }, 0);
            
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

           
        }
        protected override void OnStart()
        {
            base.OnStart();
            

        }
        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification 
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(CHANNEL_ID, "FCM Notifications", NotificationImportance.Default)
            {
                Description = "Firebase Cloud Messages appear in this channel",
            };
            channel.EnableVibration(true);
            channel.EnableLights(true);

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
        private void loadservice()
        {

            MessagingCenter.Subscribe<Object>(this, "StartLongRunningTaskMessage", (sender) => {
                Intent myIntent = new Intent(this, typeof(OnlineNotifiService));
                this.StartService(myIntent);
            });

            MessagingCenter.Subscribe<Object>(this, "StopLongRunningTaskMessage", (sender) => {
                Intent myIntent = new Intent(this, typeof(OnlineNotifiService));
                this.StopService(myIntent);
            });
        }

    }
}
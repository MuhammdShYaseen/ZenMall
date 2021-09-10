using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMall.Views.Foods;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Essentials;
using ZenMall.Views.MainPages;

namespace ZenMall
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            string AgreeM = Preferences.Get("AgreeMent", "");
            if (AgreeM != "IAgree")
            {
                MainPage = new NavigationPage(new AgreeMeent());
            }
            else
            {
              MainPage = new NavigationPage(new MainPage());
            }
                
        }
        
        protected override void OnStart()
        {
            AppCenter.Start("d23a7b8f-289b-4957-9f54-a113cfb33d2f",
                   typeof(Analytics), typeof(Crashes));
            MessagingCenter.Send<Object>(new Object(), "StopLongRunningTaskMessage");
        }


        protected override void OnSleep()
        {
            MessagingCenter.Send<Object>(new Object(), "StartLongRunningTaskMessage");
        }

        protected override void OnResume()
        {
            MessagingCenter.Send<Object>(new Object(), "StopLongRunningTaskMessage");
        }
    }
}

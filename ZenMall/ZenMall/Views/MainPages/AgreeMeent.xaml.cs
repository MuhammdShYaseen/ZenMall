using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZenMall.Views.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgreeMeent : ContentPage
    {
        public AgreeMeent()
        {
            InitializeComponent();
        }

      async  private void BtnAgree_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("AgreeMent", "IAgree");
            await DisplayAlert("زين مول", "شكرا لموافقتك على سياسة الاستخدام الخاصة بتطبيق زين مول نسعد جدا بانضمامك الينا", "حسنا");
            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

       async private void BtnDisAgree_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("زين مول", "للأسف لن تتمكن من استخدام خدماتنا الا بعد الموافقة على سياسة الاستخدام نسعد جدا بانضمامك الينا", "حسنا");
        }

        private async void BtnMenu_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Browser.OpenAsync("https://1drv.ms/b/s!AltgvNEDaW1zgZc8glNwg4zFHJv09g?e=7KoajT", BrowserLaunchMode.SystemPreferred);
            }
            catch { }
        }
    }
}
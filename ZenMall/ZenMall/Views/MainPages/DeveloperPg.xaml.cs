using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZenMall.Views.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeveloperPg : ContentPage
    {
        public DeveloperPg()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void BtnDevWhatsApp_Clicked(object sender, EventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                Device.OpenUri(new Uri("https://wa.me/+963958404199/?text=مرحبا"));
            });

        }

       async private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Browser.OpenAsync("https://1drv.ms/b/s!AltgvNEDaW1zgZc8glNwg4zFHJv09g?e=7KoajT", BrowserLaunchMode.SystemPreferred);
            }
            catch { }
        }
    }
}
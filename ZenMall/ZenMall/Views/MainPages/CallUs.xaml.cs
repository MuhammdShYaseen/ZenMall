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
    public partial class CallUs : ContentPage
    {
        public CallUs()
        {
            InitializeComponent();
        }
        
        async private void btnNontif_Clicked(object sender, EventArgs e)
        {
            await btnNontif.ScaleTo(1.2, 100);
            await btnNontif.ScaleTo(1, 100);
            await Navigation.PushAsync(new Notif());
        }
        async private void btnHome_Clicked(object sender, EventArgs e)
        {
            
            await btnHome.ScaleTo(1.2, 100);
            await btnHome.ScaleTo(1, 100);
            await Navigation.PopToRootAsync();
        }
        async private void btnCallus_Clicked(object sender, EventArgs e)
        {
            await btnCallus.ScaleTo(1.2, 100);
            await btnCallus.ScaleTo(1, 100);
            
        }

      async  private void btnBach_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        [Obsolete]
        private void BtnHelp_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
               Device.OpenUri(new Uri("https://wa.me/+963988430560/?text=مرحبا"));
            });
        }

    }
}
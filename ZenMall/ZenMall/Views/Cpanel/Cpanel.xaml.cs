using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZenMall.Views.Cpanel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cpanel : ContentPage
    {
        public Cpanel()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            txtConnString.Text = Preferences.Get("Conn", null);
        }

       async private void BtnSave_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("Conn", txtConnString.Text);
            await DisplayAlert("تم", "تم تحديث بيانات الاتصال بالسيرفر", "حسنا");
            await Navigation.PopAsync();
        }
    }
}
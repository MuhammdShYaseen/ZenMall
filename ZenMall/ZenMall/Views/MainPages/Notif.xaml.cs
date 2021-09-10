using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMall.ViewModels.SQLite;
using ZenMall.Views.MainPages;

namespace ZenMall
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notif : ContentPage
    {
        public Notif()
        {
            InitializeComponent();
        }
        async private void btnBach_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        async private void btnNontif_Clicked(object sender, EventArgs e)
        {
            await btnNontif.ScaleTo(1.2, 100);
            await btnNontif.ScaleTo(1, 100);
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
            await Navigation.PushAsync(new CallUs());
        }

       async private void btnDelAll_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("الاشعارات", "سيتم حذف كافة الاشعارات ماعدا اخر اشعار", "حسناً");
            SQLQuery ss = new SQLQuery();
            ss.DeleteAllRows();
            DeletItem();
            
        }
        private void DeletItem()
        {
            SQLQuery ss = new SQLQuery();
            NotiColl.IsVisible = false;
            Thread.Sleep(1000);
            NotiColl.ItemsSource = ss.NotiSource;
            LblNoNoti.IsVisible = true;
            //NotiColl.IsVisible = true;

        }
    }
}
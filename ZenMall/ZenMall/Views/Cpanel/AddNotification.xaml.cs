using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMall.Models.SQLite.Tables;
using ZenMall.ViewModels.SQLite;
using ZenMall.ViewModels.SqlServerADO;

namespace ZenMall.Views.Cpanel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNotification : ContentPage
    {
       
        public AddNotification()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {  
            base.OnAppearing();
            SQLQuery ss = new SQLQuery();
            foreach (var s in ss.NotiSource)
            {
                txtSub.Text = s.NotiDiscraption;
                txtTitel.Text = s.Notititel;
               
            }
        }

       async private void BtnSend_Clicked(object sender, EventArgs e)
        {
            try 
            {
                BtnSend.Text = "يرجى الانتظار ...";
                var ss = new NotificationADOMain();
                var Notis = new NotificationM();
                Notis.NotiDiscraption = txtSub.Text;
                Notis.Notititel = txtTitel.Text;
                Notis.NotiImage = "shoppingcart.png";
                ss.UpdateNoti(Notis);
                await DisplayAlert("التنبيهات", "تم ارسال تنبيه لكافة مستخدمين التطبيق", "حسنا");
                BtnSend.Text = "ارسال تنبيه";
                await Navigation.PopAsync();
            }catch(Exception ex)
            {
                await DisplayAlert("خطأ", ex.Message.ToString(), "حسنا");
            }
            
        }
    }
}
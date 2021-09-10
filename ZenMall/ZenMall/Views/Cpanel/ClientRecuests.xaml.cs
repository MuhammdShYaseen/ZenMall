using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMall.Models.SqlServerModels;
using ZenMall.ViewModels.SqlServerADO;

namespace ZenMall.Views.Cpanel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientRecuests : ContentPage
    {
        public ClientRecuests()
        {
            InitializeComponent();
        }
       async protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
            var ss = new ClientDetsADOMain();

            CollClients.ItemsSource = ss.ListCRec().OrderByDescending(c => c.RecDateTime).ToList();
            }
            catch(Exception ex)
            {
               await DisplayAlert("خطأ",ex.Message,"OK");
            }
            
            
            
           
        }

       async private void BtnClientDets_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int ClientID = int.Parse(button.BindingContext.ToString());
            await Navigation.PushAsync(new ClientDetails(ClientID));
        }

        [Obsolete]
        private void BtnCWhasApp_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Device.BeginInvokeOnMainThread(() =>
            {
                Device.OpenUri(new Uri("https://wa.me/"+button.Text+"/?text=مرحبا"));
            });
        }

       async private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("تأكيد الحذف !!", "هل تريد فعلا حذف هذا الطلب ؟؟", "نعم", "لا");
            if (answer == true)
                try
                {
                Button button = sender as Button;
                var ss = new ClientDetsADOMain();
                ss.DeleteClientRec(int.Parse(button.BindingContext.ToString()));
                    CollClients.ItemsSource = null;
                   CollClients.ItemsSource = ss.ListCRec().OrderByDescending(c => c.RecDateTime).ToList();
                    await DisplayAlert("الحذف", "تم حذف الطلب بنجاح !", "حسنا");
                }
            catch(Exception ex)
            {
                await DisplayAlert("خطأ",ex.Message, "OK");
            }
        }
    }
}
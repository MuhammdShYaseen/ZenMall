using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMall.Views.MainPages;

using ZenMall.ViewModels.SQLite;

namespace ZenMall.Views.Pharmacy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pharmacy : ContentPage
    {
        public Pharmacy()
        {
            InitializeComponent();
            SQLQuery SL = new SQLQuery();
            LbCount.Text = SL.BasketSource.Count.ToString();
        }
        async private void btnBasket_Clicked(object sender, EventArgs e)
        {
            await btnBasket.ScaleTo(1.2, 100);
            await btnBasket.ScaleTo(1, 100);
            await Navigation.PushAsync(new Basket());
        }
        async private void btnNontif_Clicked(object sender, EventArgs e)
        {
            await btnNontif.ScaleTo(1.2, 100);
            await btnNontif.ScaleTo(1, 100);
            await Navigation.PushAsync(new Notif());
        }
        async private void btnHome_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
            await btnHome.ScaleTo(1.2, 100);
            await btnHome.ScaleTo(1, 100);
        }
        async private void btnCallus_Clicked(object sender, EventArgs e)
        {
            
            await btnCallus.ScaleTo(1.2, 100);
            await btnCallus.ScaleTo(1, 100);
            await Navigation.PushAsync(new CallUs());
        }

       private void btntoBasket_Clicked(object sender, EventArgs e)
        {
            btntoBasket.IsEnabled = false;
            SQLQuery ss = new SQLQuery();
            bool IsTrue = (txtMName.Text == null || txtMName.Text == "");
            if (IsTrue==true)
            {
              txtMName.Focus();
            }
            else
            {
                ss.Insert(txtMName.Text, 0, 0, "vitamin.png", 1);
                txtMName.Text = "";
                btntoBasket.Text = "تم اضافة المنتج الى السلة";
                LbCount.Text = (int.Parse(LbCount.Text) + 1).ToString();

            }
           
            btntoBasket.IsEnabled = true;
        }

        [Obsolete]
        private void BtnCUs_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Device.OpenUri(new Uri("https://wa.me/+963988430560/?text=مرحبا"));
            });
        }

        async private void MainSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            if (MainSearch.Text == "CpanelZmall")
            {
                await Navigation.PushAsync(new Cpanel.Cpanel());
            }
            else
            {
                await Navigation.PushAsync(new SearchRes(MainSearch.Text));
            }
        }
    }
}
using Newtonsoft.Json;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZenMall.Models.SQLite.CreateConnect;
using ZenMall.Models.SqlServerModels;
using ZenMall.ViewModels.SQLite;
using ZenMall.ViewModels.SqlServerViewModels;
using ZenMall.Views.Accessories;
using ZenMall.Views.Car;
using ZenMall.Views.Clean_Clear;
using ZenMall.Views.Clothes;
using ZenMall.Views.Cosmetics;
using ZenMall.Views.Cpanel;
using ZenMall.Views.Foods;
using ZenMall.Views.Gifts;
using ZenMall.Views.HouseholdTools;
using ZenMall.Views.MainPages;
using ZenMall.Views.Perfumes;
using ZenMall.Views.Pharmacy;
using ZenMall.Views.ProdactDetails;
using ZenMall.Views.Smokers;
using ZenMall.Views.Stationery;

namespace ZenMall
{
    public partial class MainPage : ContentPage
    {
        //private bool isZoomed;
        public MainPage()
        {
          InitializeComponent();
          SetDatasSource();
          AutoSlider();
            IsEnabled = true;
        }

        public async void SetDatasSource()
        {
          
            StkCollView.IsVisible = false;
            StkLoading.IsVisible = true;
            SQLdata ss = new SQLdata();
            
                    
              
           
            try
            {


                var AssList1 = await ss.Datasourcee("");
                var AssList2 = AssList1.Where(c => c.PDiscraption.Contains("*"));

                Mycollaction.ItemsSource = AssList2;



            }
            catch
            {
               await DisplayAlert("الانترنت", "لا يوجد اتصال بالانترنت","حسناً");
                return;
            }
           
             
             StkLoading.IsVisible = false;
             StkCollView.IsVisible = true;
             
             Gotolast();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //IsAgree();
            IsEnabled = true;
            SetDatasSource();
            Gotolast();
            SQLQuery ss = new SQLQuery();
            LbCount.Text = ss.BasketSource.Count.ToString();
            string IsConnStr = Preferences.Get("Conn", null);
           
            if (IsConnStr =="" || IsConnStr ==null)
            {
                BtnAdd.IsVisible = false;
                BtnNoti.IsVisible = false;
                BtnClientR.IsVisible = false;
            }
            else
            {
                BtnAdd.IsVisible = true;
                BtnNoti.IsVisible = true;
                BtnClientR.IsVisible = true;
            }
            
        }
       async private void  AutoSlider()
        {
            try
            {
                var SS = new SQLdata();
                CrsAdver.ItemsSource = await SS.AdvetismentSource("اعلانات", "#");
                int SlidePosition = 0;
                Device.StartTimer(TimeSpan.FromSeconds(4), () =>
                {
                    SlidePosition = SlidePosition + 1;
                    if (SlidePosition == 4) SlidePosition = 0;
                    CrsAdver.Position = SlidePosition;
                    return true;
                });
            }
            catch
            {

            }
           
        }
       async private void IsAgree()
        {
            string AgreeM = Preferences.Get("AgreeMent", "");
            if (AgreeM != "IAgree")
            {
               IsEnabled = false;
               await Navigation.PushAsync(new AgreeMeent());
            }
            else
            {
                IsEnabled = true;
            }
        }



        private void Gotolast()
        {
            
            var lastChild = stkMain.Children.LastOrDefault();

            Categores.ScrollToAsync(lastChild, ScrollToPosition.End, false);
           
        }

        

        async private void btnFood_Clicked(object sender, EventArgs e)
        {

           

           this.IsEnabled = false;
           await btnFood.ScaleTo(1.2, 100);
           await btnFood.ScaleTo(1, 100);
           await Navigation.PushAsync(new Foods());
           
        }

        async private void btnCleans_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await btnCleans.ScaleTo(1.2, 100);
            await btnCleans.ScaleTo(1, 100);
            await Navigation.PushAsync(new Cleans());
            
        }

        async private void btnHouseTools_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await btnHouseTools.ScaleTo(1.2, 100);
            await btnHouseTools.ScaleTo(1, 100);
            await Navigation.PushAsync(new HouseholdTools());
            
        }

        async private void btnGifts_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await btnGifts.ScaleTo(1.2, 100);
            await btnGifts.ScaleTo(1, 100);
            await Navigation.PushAsync(new Gifts());
           
        }

        async private void btnPharma_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await btnPharma.ScaleTo(1.2, 100);
            await btnPharma.ScaleTo(1, 100);
            await Navigation.PushAsync(new Pharmacy());
        }

        async private void btnAccesury_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await btnAccesury.ScaleTo(1.2, 100);
            await btnAccesury.ScaleTo(1, 100);
            await Navigation.PushAsync(new Accessories());
            
        }

        async private void btnStationery_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await btnStationery.ScaleTo(1.2, 100);
            await btnStationery.ScaleTo(1, 100);
            await Navigation.PushAsync(new Stationery());
        }

        async private void btnSmokers_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await btnSmokers.ScaleTo(1.2, 100);
            await btnSmokers.ScaleTo(1, 100);
            await Navigation.PushAsync(new Smokers());
         
        }

        async private void btnPerphiom_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await btnPerphiom.ScaleTo(1.2, 100);
            await btnPerphiom.ScaleTo(1, 100);
            await Navigation.PushAsync(new Perfumes());
            
        }

        async private void btnbeauty_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await btnbeauty.ScaleTo(1.2, 100);
            await btnbeauty.ScaleTo(1, 100);
            await Navigation.PushAsync(new Cosmetics());
            
        }

        async private void btnCar_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await btnCar.ScaleTo(1.2, 100);
            await btnCar.ScaleTo(1, 100);
            await Navigation.PushAsync(new Cars());
            
        }

        async private void btnNontif_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await btnNontif.ScaleTo(1.2, 100);
            await btnNontif.ScaleTo(1, 100);
            await Navigation.PushAsync(new Notif());
        }

        async private void btnBasket_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await btnBasket.ScaleTo(1.2, 100);
            await btnBasket.ScaleTo(1, 100);
            await Navigation.PushAsync(new Basket());
        }



        async private void btnClothes_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await btnClothes.ScaleTo(1.2, 100);
            await btnClothes.ScaleTo(1, 100);
            await Navigation.PushAsync(new Clothes());
            
        }

        async private void btnCallus_Clicked(object sender, EventArgs e)
        {                    
            this.IsEnabled = false;
            await btnCallus.ScaleTo(1.2, 100);
            await btnCallus.ScaleTo(1, 100);
            await Navigation.PushAsync(new CallUs());           
        }

      async  private void MainSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            if (MainSearch.Text == "CpanelZmall")
            {
                await Navigation.PushAsync(new Cpanel());
            }
            else if (MainSearch.Text.Contains("اعلان") || MainSearch.Text.Contains("اعلانا") || MainSearch.Text.Contains("اعلانات"))
            {
                return;
            }
            else
            {
                await Navigation.PushAsync(new SearchRes(MainSearch.Text));
            }

        }

     

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            Button button = sender as Button;
            try
            {
                button.IsEnabled = false;
            SQLdata ss = new SQLdata();
            SQLQuery LiteQ = new SQLQuery();            
            button.Text = "الرجاء الانتظار..";
            string ppname;
            string pdisc;
            int PDprice;
            string Pimageu;
            var idd = button.BindingContext;
            int pidd = Int32.Parse(idd.ToString());           
            var table = await ss.Datasourcee("");
            var searchresult = table.Where(c => c.ProdactID.Equals(pidd));
                foreach (var s in searchresult)
                {
                    ppname = s.PName;
                    pdisc = s.PDiscraption;
                    PDprice = (int)s.PDiscount;
                    Pimageu = s.PImage;
                           if (LiteQ.Select(ppname + " " + pdisc) == true)
                              {                             
                               LiteQ.Insert(ppname + " " + pdisc,PDprice,PDprice,Pimageu,1);
                        // await DisplayAlert("السلة", "تم اضافة "+ppname, "حسنا");
                        CrossToastPopUp.Current.ShowToastMessage("تم اضافة "+ ppname+" الى السلة ",Plugin.Toast.Abstractions.ToastLength.Short);
                        button.Text = "اضافة الى السلة";
                        LbCount.Text = (int.Parse(LbCount.Text) + 1).ToString();
                    }
                           else
                              {
                        CrossToastPopUp.Current.ShowToastMessage("المنتج "+ ppname + " موجود في السلة ", Plugin.Toast.Abstractions.ToastLength.Short);
                                button.Text = "اضافة الى السلة";
                    }
                    button.IsEnabled = true;
                }
            }
            catch
            {
                await DisplayAlert("خطأ", "تأكد من الاتصال بالانترنت", "حسنا");
                button.IsEnabled = true;
                button.Text = "اضافة الى السلة";
            }
           

        }

       async private void ImageButton_Clicked(object sender, EventArgs e)
        {
            ImageButton button = sender as ImageButton;
            await button.ScaleTo(1.2, 100);
            await button.ScaleTo(1, 100);
            await Navigation.PushAsync(new ProdactsDetailTry(button.Source.ToString()));
        }

       async private void BtnNoti_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new AddNotification());
        }

       async private void BtnClientR_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClientRecuests());
        }

       async private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPrd());
        }

       async private void btnAbout_Clicked(object sender, EventArgs e)
        {
            IsEnabled = false;
            await btnAbout.ScaleTo(1.2, 100);
            await btnAbout.ScaleTo(1, 100);
            await Navigation.PushAsync(new DeveloperPg());
        }

       async private void BtnFast_Clicked(object sender, EventArgs e)
        {
            IsEnabled = false;
            await BtnFast.ScaleTo(1.2, 100);
            await BtnFast.ScaleTo(1, 100);
            await Navigation.PushAsync(new FastMarket());
        }
    }
}

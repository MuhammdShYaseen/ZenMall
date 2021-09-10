﻿using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMall.Models.SqlServerModels;
using ZenMall.ViewModels.SQLite;
using ZenMall.ViewModels.SqlServerViewModels;
using ZenMall.Views.MainPages;
using ZenMall.Views.ProdactDetails;

namespace ZenMall.Views.HouseholdTools
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HouseholdTools : ContentPage
    {
        public HouseholdTools()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            SetDSMain();
        }
        async private void SetDSMain()
        {
            try
            {
            CollHTools.IsVisible = false;
            loading.IsVisible = true;
            SQLdata ss = new SQLdata();
            List<ProdactsM> AssList;
            AssList = (List<ProdactsM>)await ss.Datasourcee("");
            var AssList1 = AssList.Where(c => c.PSaction.Contains("تحف") || c.PSaction.Contains("المطبخ") || c.PSaction.Contains("كهربائيات"));
            CollHTools.ItemsSource = AssList1;
            loading.IsVisible = false;
            CollHTools.IsVisible = true;
            LblSaction.Text = "كل المنتوجات";
                SQLQuery SL = new SQLQuery();
                LbCount.Text = SL.BasketSource.Count.ToString();
            }
            catch
            {
                await DisplayAlert("الانترنت", "لا يوجد اتصال بالانترنت", "حسناً");
            }
        }
        async private void SetSactionDs(string Saction)
        {
            try
            {
            CollHTools.IsVisible = false;
            loading.IsVisible = true;
            SQLdata ss = new SQLdata();
            List<ProdactsM> AssList;
            AssList = (List<ProdactsM>)await ss.Datasourcee("");
            var AssList1 = AssList.Where(c => c.PSaction.Contains(Saction));
            CollHTools.ItemsSource = AssList1;
            loading.IsVisible = false;
            CollHTools.IsVisible = true;
            LblSaction.Text = Saction;
            }
            catch
            {
                await DisplayAlert("الانترنت", "لا يوجد اتصال بالانترنت", "حسناً");
            }
           
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
        async private void Button_Clicked_2(object sender, EventArgs e)
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
                        LiteQ.Insert(ppname + " " + pdisc, PDprice, PDprice, Pimageu, 1);
                        // await DisplayAlert("السلة", "تم اضافة "+ppname, "حسنا");
                        CrossToastPopUp.Current.ShowToastMessage("تم اضافة " + ppname + " الى السلة ", Plugin.Toast.Abstractions.ToastLength.Short);
                        button.Text = "اضافة الى السلة";
                        LbCount.Text = (int.Parse(LbCount.Text) + 1).ToString();
                    }
                    else
                    {
                        CrossToastPopUp.Current.ShowToastMessage("المنتج " + ppname + " موجود في السلة ", Plugin.Toast.Abstractions.ToastLength.Short);
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


        private void btnAntiques_Clicked(object sender, EventArgs e)
        {
            SetSactionDs("تحف");
        }

        private void btnKichen_Clicked(object sender, EventArgs e)
        {
            SetSactionDs("مطبخ");
        }

        private void btnElectreic_Clicked(object sender, EventArgs e)
        {
            SetSactionDs("كهربائيات");
        }

        private void btnAll_Clicked(object sender, EventArgs e)
        {
            SetDSMain();
        }

        async private void ImageButton_Clicked(object sender, EventArgs e)
        {
            ImageButton button = sender as ImageButton;

            await Navigation.PushAsync(new ProdactsDetailTry(button.Source.ToString()));
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
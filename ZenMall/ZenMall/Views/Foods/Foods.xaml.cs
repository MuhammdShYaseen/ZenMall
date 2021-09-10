using Plugin.Toast;
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

namespace ZenMall.Views.Foods
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Foods : ContentPage
    {
        
        public Foods()
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
            CollFoods.IsVisible = false;
            loading.IsVisible = true;
            SQLdata ss = new SQLdata();
            List<ProdactsM> AssList;
            AssList = (List<ProdactsM>)await ss.Datasourcee("");
            var AssList1 = AssList.Where(c => c.PSaction.Contains("يومي") || c.PSaction.Contains("دجاج") || c.PSaction.Contains("توابل") || c.PSaction.Contains("لحوم") || c.PSaction.Contains("البان") || c.PSaction.Contains("مخبوزات") || c.PSaction.Contains("خضار") || c.PSaction.Contains("معلبات") || c.PSaction.Contains("حلويات") || c.PSaction.Contains("مشروبات") || c.PSaction.Contains("حبوب") || c.PSaction.Contains("مفرزات") || c.PSaction.Contains("نقرشات") || c.PSaction.Contains("فوط"));
            CollFoods.ItemsSource = AssList1;
            loading.IsVisible = false;
            CollFoods.IsVisible = true;
            Lblsection.Text = "كل المنتوجات :";
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
            CollFoods.IsVisible = false;
            loading.IsVisible = true;
            SQLdata ss = new SQLdata();
            List<ProdactsM> AssList;
            AssList = (List<ProdactsM>)await ss.Datasourcee("");
            var AssList1 = AssList.Where(c => c.PSaction.Contains(Saction));
            CollFoods.ItemsSource = AssList1;
            loading.IsVisible = false;
            CollFoods.IsVisible = true;
            Lblsection.Text = Saction;
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

        private void btndaily_Clicked(object sender, EventArgs e)
        {
            SetSactionDs("يومي");
        }

        private void btnMeat_Clicked(object sender, EventArgs e)
        {
            SetSactionDs("لحوم");
        }

        private void btnVeg_Clicked(object sender, EventArgs e)
        {
            SetSactionDs("خضار");
        }

        private void btnBaking_Clicked(object sender, EventArgs e)
        {
            SetSactionDs("مخبوزات");
        }

        private void btnCheese_Clicked(object sender, EventArgs e)
        {
            SetSactionDs("البان");
        }

        private void btnDrinks_Clicked(object sender, EventArgs e)
        {
            SetSactionDs("مشروبات");
        }

        private void btnSweets_Clicked(object sender, EventArgs e)
        {
            SetSactionDs("حلويات");
        }

        private void btnCanned_Clicked(object sender, EventArgs e)
        {
            SetSactionDs("معلبات");
        }

        private void btnFrozen_Clicked(object sender, EventArgs e)
        {
            SetSactionDs("مفرزات");
        }

        private void btnGrain_Clicked(object sender, EventArgs e)
        {
            SetSactionDs("حبوب");
        }

        private void btnharbls_Clicked(object sender, EventArgs e)
        {
            SetSactionDs("توابل");
        }

        private void btnCleanics_Clicked(object sender, EventArgs e)
        {
            SetSactionDs("فوط");
        }

        private void btnCrackers_Clicked(object sender, EventArgs e)
        {
            SetSactionDs("نقرشات");
        }

        private void BtnAll_Clicked(object sender, EventArgs e)
        {
            SetDSMain();
        }

        private void btnChicken_Clicked(object sender, EventArgs e)
        {
            SetSactionDs("دجاج");
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
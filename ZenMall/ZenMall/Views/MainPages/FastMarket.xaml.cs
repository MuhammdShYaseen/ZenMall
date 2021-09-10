using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMall.Models.SqlServerModels;
using ZenMall.ViewModels.SQLite;
using ZenMall.ViewModels.SqlServerViewModels;
using ZenMall.Views.ProdactDetails;

namespace ZenMall.Views.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FastMarket : ContentPage
    {
        public FastMarket()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            SQLQuery ss = new SQLQuery();
            LbCount.Text = ss.BasketSource.Count.ToString();
            await DisplayAlert("توضيح !", "في هذا القسم بامكانك التسوق بسرعة دون الحاجة الى تصفح اقسام البرنامج و طلب المنتجات الغير موجودة !!", "حسناً");
        }
        private void btntoBasket_Clicked(object sender, EventArgs e)
        {
            btntoBasket.IsEnabled = false;
            SQLQuery ss = new SQLQuery();
            bool IsTrue = (SrchFast.Text == null || SrchFast.Text == "");
            if (IsTrue == true)
            {
                SrchFast.Focus();
            }
            else
            {
                ss.Insert(SrchFast.Text, 0, 0, "fast.png", 1);
                
                CrossToastPopUp.Current.ShowToastMessage(" تمت اضافة "+SrchFast.Text);
                LbCount.Text = (int.Parse(LbCount.Text) + 1).ToString();
                SrchFast.Text = "";
            }

            btntoBasket.IsEnabled = true;
        }
        async private void SetDSearch(string SearchWD)
        {
            CollSearch.IsVisible = false;
            loading.IsVisible = true;
            try
            {
                SQLdata ss = new SQLdata();
                CollSearch.ItemsSource = await ss.DsSearchResults(SearchWD);
                if (await ss.ItemCount(SearchWD)==0)
                {
                    btntoBasket.IsEnabled = true;
                }else
                {
                    btntoBasket.IsEnabled = false;
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("خطأ", ex.Message, "حسنا");
            }
            CollSearch.IsVisible = true;
            loading.IsVisible = false;
        }

       private void SrchFast_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetDSearch(SrchFast.Text);
        }
        async private void ImageButton_Clicked(object sender, EventArgs e)
        {
            ImageButton button = sender as ImageButton;
            await button.ScaleTo(1.2, 100);
            await button.ScaleTo(1, 100);
            await Navigation.PushAsync(new ProdactsDetailTry(button.Source.ToString()));
        }

        async private void Button_Clicked(object sender, EventArgs e)
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
        async private void btnBasket_Clicked(object sender, EventArgs e)
        {
            
            await btnBasket.ScaleTo(1.2, 100);
            await btnBasket.ScaleTo(1, 100);
            await Navigation.PushAsync(new Basket());
        }
    }
}
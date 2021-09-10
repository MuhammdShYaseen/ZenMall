using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMall.ViewModels.SQLite;
using ZenMall.ViewModels.SqlServerViewModels;
using ZenMall.Views.ProdactDetails;

namespace ZenMall.Views.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchRes : ContentPage
    {
        string SearchWD;
        public SearchRes(string SearchW)
        {
            InitializeComponent();
            SearchWD = SearchW;
        }
        protected override void OnAppearing()
        {
            SetDSearch();
        }
       async private void SetDSearch()
        {
            try
            {
                CollSearch.IsVisible = false;
                loading.IsVisible = true;
                base.OnAppearing();
                SQLdata ss = new SQLdata();
                CollSearch.ItemsSource = await ss.DsSearchResults(SearchWD);
                loading.IsVisible = false;
                CollSearch.IsVisible = true;
            }
            catch
            {
                await DisplayAlert("خطأ", "لا يمكن الوصول الى الانترنت", "حسنا");
            }
           
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
                        button.Text = "تمت الاضافة";
                       
                    }
                    else
                    {
                        await DisplayAlert("السلة", "هذا المنتج موجود ضمن السلة ", "حسنا");
                        button.Text = "موجود في السلة";
                        button.BackgroundColor = Color.Red;
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
    }
}
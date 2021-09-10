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

namespace ZenMall.Views.ProdactDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProdactsDts : ContentPage
    {
        int pids;
        public ProdactsDts(int pidd)
        {
            InitializeComponent();
            pids = pidd;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            SetDatasSource();

        }
        public async void SetDatasSource()
        {
            try
            {
                SCLMain.IsVisible = false;
                AILoading.IsVisible = true;
                SQLdata ss = new SQLdata();
                
                var table = await ss.Datasourcee("");
                var searchresult = table.Where(c => c.ProdactID.Equals(pids));
                foreach (var s in searchresult)
                {
                    txtpname.Text = s.PName;
                    txtpdis.Text = s.PDiscraption;
                    txtpdiscunt.Text = s.PDiscount.ToString()+" ل.س " ;
                    txtpmarke.Text = s.PMarks;
                    txtpprice.Text = s.PPrice.ToString() + " ل.س ";
                    imgPimage.Source = s.PImage;



                }
                AILoading.IsVisible = false;
                SCLMain.IsVisible = true;
            }
            catch
            {
                await DisplayAlert("الانترنت", "لا يوجد اتصال بالانترنت", "حسناً");
            }


        }

       async private void BtnAddToBasket_Clicked(object sender, EventArgs e)
        {
            IsEnabled = false;

            SQLdata ss = new SQLdata();
            SQLQuery LiteQ = new SQLQuery();
            
            BtnAddToBasket.Text = "الرجاء الانتظار..";
            string ppname;
            string pdisc;
            int PDprice;
            string Pimageu;
            
           


            var table = await ss.Datasourcee("");
            var searchresult = table.Where(c => c.ProdactID.Equals(pids));
            foreach (var s in searchresult)
            {
                ppname = s.PName;
                pdisc = s.PDiscraption;
                PDprice = (int)s.PDiscount;
                Pimageu = s.PImage;

                if (LiteQ.Select(ppname + " " + pdisc) == true)
                {

                    LiteQ.Insert(ppname + " " + pdisc, PDprice, PDprice, Pimageu, 1);
                    await DisplayAlert("السلة", "تم اضافة " + ppname, "حسنا");
                    BtnAddToBasket.Text = "تمت الاضافة";
                }
                else
                {
                    await DisplayAlert("السلة", "هذا المنتج موجود ضمن السلة ", "حسنا");
                    BtnAddToBasket.Text = "موجود في السلة";
                    BtnAddToBasket.BackgroundColor = Color.Red;
                }
                IsEnabled = true;
            }

        }
    }
}
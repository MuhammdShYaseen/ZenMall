using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMall.ViewModels.SQLite;

namespace ZenMall.Views.ProdactDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCounties : ContentPage
    {
        string imgSs;
        int pids;
        public EditCounties(int pidd)
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
                SQLQuery ss = new SQLQuery();

                var table = ss.BasketSource;
                var searchresult = table.Where(c => c.ID.Equals(pids));
                foreach (var s in searchresult)
                {
                    txtpname.Text = s.ProdactName;


                    txtpprice.Text = s.Prise.ToString();
                    txtcount.Text = s.NumberOfProdacts.ToString();
                    imgPimage.Source = s.Pimage;
                    imgSs = s.Pimage;


                }
                AILoading.IsVisible = false;
                SCLMain.IsVisible = true;
            }
            catch
            {
                await DisplayAlert("الانترنت", "لا يوجد اتصال بالانترنت", "حسناً");
            }

        }

      async  private void Button_Clicked(object sender, EventArgs e)
        {

           
            int TotalPrice;
            int a, b;

            bool isAValid = int.TryParse(txtpprice.Text, out a);
            bool isBValid = int.TryParse(txtcount.Text, out b);
            if (isAValid && isBValid)
            {
                if (b == 0)
                    b = 1;
             TotalPrice = a * b;
            
            SQLQuery ss = new SQLQuery();
            ss.Update(txtpname.Text,a,TotalPrice,imgSs,b,pids);
            }
            await Navigation.PopAsync();
            
        }

        private void BtnIncrace_Clicked(object sender, EventArgs e)
        {
            txtcount.Text = (int.Parse(txtcount.Text) + 1).ToString();
        }

        private void BtnDiscrace_Clicked(object sender, EventArgs e)
        {
            txtcount.Text = (int.Parse(txtcount.Text) - 1).ToString();
            if (int.Parse(txtcount.Text) < 0) 
            {
                txtcount.Text = "1";
            }
        }
    }
}
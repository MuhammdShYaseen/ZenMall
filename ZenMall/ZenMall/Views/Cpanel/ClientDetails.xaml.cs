using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMall.Models.SQLite.Tables;
using ZenMall.Models.SqlServerModels;
using ZenMall.ViewModels.SqlServerADO;

namespace ZenMall.Views.Cpanel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientDetails : ContentPage
    {
        int ClientIDs;
        double Longtit;
        double Litit;
        public ClientDetails(int ClientID)
        {
            InitializeComponent();
            ClientIDs = ClientID;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetData();
        }
       async private void SetData()
        {
            try
            {
            string ProdactsDS;
            var ss = new ClientDetsADOMain();
            List<ClientRec> MySD = new List<ClientRec>();
            MySD = ss.ListCRec();
            var table = MySD;
            var searchresult = table.Where(c => c.RecuestID.Equals(ClientIDs));
            foreach (var s in searchresult)
            {
                txtCAdress.Text = s.Adress;
                txtCname.Text = s.FullName;
                txtCPhone.Text = s.PhoneNumber;
                Longtit =double.Parse (s.Long);
                Litit =double.Parse (s.litit);
                ProdactsDS = s.Prodacts;
                List<BasketTB> resultforecast;
                resultforecast = JsonConvert.DeserializeObject<List<BasketTB>>(ProdactsDS);
                CollClients.ItemsSource = resultforecast;
            }
            }catch(Exception ex)
            {
                await DisplayAlert("",ex.Message, "");
            }
            

        }

       async private void BtnDisplayLocation_Clicked(object sender, EventArgs e)
        {
            try
            {
            var location = new Location(Litit, Longtit);
            var options = new MapLaunchOptions { NavigationMode = NavigationMode.None };
                options.Name = txtCname.Text;
            await Map.OpenAsync(location, options);
            }
            catch(Exception ex)
            {
                await DisplayAlert("", ex.Message, "");
            }
            
        }
    }
}
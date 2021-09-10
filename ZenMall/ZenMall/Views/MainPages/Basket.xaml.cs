using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMall.Models.SqlServerModels;
using ZenMall.ViewModels.SQLite;
using ZenMall.ViewModels.SqlServerViewModels;
using ZenMall.Views.MainPages;
using ZenMall.Views.ProdactDetails;

namespace ZenMall
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Basket : ContentPage
    {
        
        public Basket()
        {
            
            InitializeComponent();
           // BasketColl.ItemsSource = Squiry.BasketSource;
       
        }
        protected override void OnAppearing()
        {
            IsEnabled = true;
            // BasketColl.ItemsSource = Squiry.BasketSource;
            StkInfo.IsVisible = false;
            btnShowHide.TextColor = Color.DarkBlue;
            btnShowHide.Text = "➕";
            btnShowHide.BackgroundColor = Color.Transparent;
            DeletItem();
           txtName.Text= Preferences.Get("UName", null);
           txtAdress.Text= Preferences.Get("UAdress",null);
           txtPhone.Text= Preferences.Get("UPhone", null);
           lblLongtude.Text= Preferences.Get("ULong", "0");
           lblLatitude.Text= Preferences.Get("ULitit","0");
           
        }
        async private void btnBach_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        async private void btnNontif_Clicked(object sender, EventArgs e)
        {
            await btnNontif.ScaleTo(1.2, 100);
            await btnNontif.ScaleTo(1, 100);
            await Navigation.PushAsync(new Notif());
        }
        async private void btnHome_Clicked(object sender, EventArgs e)
        {

            await btnHome.ScaleTo(1.2, 100);
            await btnHome.ScaleTo(1, 100);
            await Navigation.PopToRootAsync();
        }
        async private void btnCallus_Clicked(object sender, EventArgs e)
        {
            await btnCallus.ScaleTo(1.2, 100);
            await btnCallus.ScaleTo(1, 100);
            await Navigation.PushAsync(new CallUs());
        }

       async private void btnLocation_Clicked(object sender, EventArgs e)
        {
            btnLocation.Text = "يرجى الانتظار ..";
            try
            {
                var permissions = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>() ;
                if (permissions != PermissionStatus.Granted)
                {
                    permissions = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }
                if (permissions != PermissionStatus.Granted)
                { return; }
                double Latitudee=0;
                double Longtudee=0;
                CrossToastPopUp.Current.ShowToastMessage("قم بتشغيل الموقع و انتظر الى ان يصبح زر تحديد الموقع باللون الاخضر ",Plugin.Toast.Abstractions.ToastLength.Long);
           var locator =  CrossGeolocator.Current;
         
            locator.DesiredAccuracy = 50;
                
                    var position = await locator.GetPositionAsync();

                    Latitudee = position.Latitude;
                    Longtudee = position.Longitude;
                lblLatitude.Text = Latitudee.ToString();
                lblLongtude.Text = Longtudee.ToString();

                btnLocation.BackgroundColor = Color.Green;
                btnLocation.Text = "تم تحديد موقعك بنجاح";
            }catch
            {
               //await DisplayAlert("لا يوجد سماحيات الوصول الى الموقع",  " افتح اعدادات الجهاز>> ثم التطبيقات >> اختر تطبيق Zain Mall ثم >> السماحيات ثم >> قم بتفعيل الوصول الى الموقع ", "متابعة");

            }
            btnLocation.IsEnabled = true;
            

        }

      async  private void Button_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;

            Button button = sender as Button;
            var idd = button.BindingContext;

            int pidd = Int32.Parse(idd.ToString());
            await Navigation.PushAsync(new EditCounties(pidd));
        }

        private void DeletItem()
        {
            int Totals=0;
            SQLQuery ss = new SQLQuery();
            BasketColl.IsVisible = false;
            //Thread.Sleep(1000);
            BasketColl.ItemsSource = ss.BasketSource;
            
            BasketColl.IsVisible = true;
            foreach (var e in ss.BasketSource)
            {
                Totals +=  e.TotalPrice;
                
                
            }
            LblTotal.Text = "مجموع الفاتورة : "+Totals.ToString()+" ل.س ";

            
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            BasketColl.ItemsSource = null;
            Button button = sender as Button;
            var idd = button.BindingContext;
            int pidd = Int32.Parse(idd.ToString());
            SQLQuery ss = new SQLQuery();
         
            ss.Delete(pidd);

            DeletItem();
          


        }

        private async Task<bool> IsEmpty()
        {
            if (txtAdress.Text == "" || txtAdress.Text==null|| txtName.Text == "" || txtName.Text==null ||txtPhone.Text == "" ||txtPhone.Text==null ||txtPhone.Text.Length != 12)
            {
                StkInfo.IsVisible = true;
                btnShowHide.TextColor = Color.DarkBlue;
                btnShowHide.BackgroundColor = Color.Transparent;
                btnShowHide.Text = "➖";
                var StartChild = stkMain.Children.FirstOrDefault();
                await SCRbasket.ScrollToAsync(StartChild, ScrollToPosition.Start, true);

                if (txtName.Text == ""||txtName.Text==null)
                {
                    txtName.Focus();
                    return await Task.FromResult(false);
                }
                else if (txtAdress.Text == ""||txtAdress.Text==null)
                {
                    txtAdress.Focus();
                    return await Task.FromResult(false);

                }
                else if (txtPhone.Text == "" || txtPhone.Text.Length != 12 ||txtPhone.Text==null)
                {
                    txtPhone.Focus();
                    return await Task.FromResult(false);
                }
                return await Task.FromResult(false);
            }else
            return await Task.FromResult(true);
        }

        private void btnShowHide_Clicked(object sender, EventArgs e)
        {
            ShowHideSub();

        }
        private void ShowHideSub()
        {
            if (StkInfo.IsVisible == true)
            {
                StkInfo.IsVisible = false;
                btnShowHide.TextColor = Color.DarkBlue;
                btnShowHide.Text = "➕";
                btnShowHide.BackgroundColor = Color.Transparent;

            }
            else
            {
                StkInfo.IsVisible = true;
                btnShowHide.TextColor = Color.DarkBlue;
                btnShowHide.BackgroundColor = Color.Transparent;
                btnShowHide.Text = "➖";

            }
        }
       
       async private void btnRec_Clicked(object sender, EventArgs e)
        {
            btnRec.IsEnabled = false;
            SQLQuery ss = new SQLQuery();
            //var SRecCount = new SQLdata();
            string ProdactsAll= JsonConvert.SerializeObject(ss.BasketSource);    
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Add("ApiKey", "ReadAllProdactsAndNotificationAndPostClientRec");
            string SPostTime =Preferences.Get("PostDateTime",DateTime.Now.AddHours(-2).ToString());
            DateTime PostTime = DateTime.Parse(SPostTime);
            await IsEmpty();
            if (await IsEmpty() == false || ProdactsAll=="[]")
            {
                if (ProdactsAll == "[]")
                {
                    await DisplayAlert("السلة", "لا يمكن ان تكون السلة فارغة", "حسنا");
                    btnRec.IsEnabled = true;
                    return;
                }
                btnRec.IsEnabled = true;
                return;
            }
            else
            {
                PostTime = PostTime.AddMinutes(10);
              if (SPostTime !="" && PostTime<DateTime.Now)
                {
                try
                {
                List<ClientRec> ClientList = new List<ClientRec>();
                ClientList.Add(new ClientRec
                {
                    //RecuestID= await SRecCount.ClientRecCounts()+1,
                    RecDateTime = DateTime.Now,
                    FullName = txtName.Text,
                    Adress = txtAdress.Text,
                    Long = lblLongtude.Text,
                    litit = lblLatitude.Text,
                    PhoneNumber="+963"+txtPhone.Text.Substring(1),
                    Prodacts = ProdactsAll


                });


                string DataToPost = JsonConvert.SerializeObject(ClientList);
                DataToPost = DataToPost.Substring(1);
                DataToPost = DataToPost.Remove(DataToPost.Length - 1);
                var Containtss = new  StringContent(DataToPost,Encoding.UTF8,"application/json");
                
                var Rezulte = await httpclient.PostAsync("https://zainmall.sy/api/ClientRecuests", Containtss);
                if ((int)Rezulte.StatusCode == 200 || (int)Rezulte.StatusCode<300 || (int)Rezulte.StatusCode==201)
                {
                 await DisplayAlert("الطلبات", " تم استلام طلبك سوف نتصل بك هاتفيا خلال دقائق لمزيد من المعلومات نسعد باتصالك معنا", "شكرا لاستعمالك خدماتنا");
                 //ss.DeleteAllRowsBasket();
                 Preferences.Set("PostDateTime", DateTime.Now.ToString());
                            // await Navigation.PopAsync();

                        }
                        else
                        {
                            await DisplayAlert("الطلبات", Rezulte.RequestMessage.ToString(), "OK");
                        }
                }
                catch 
                {
                    await DisplayAlert("خطأ", "تاكد من اتصالك بالانترنت ثم حاول مرة اخرى","حسنا");
                    btnRec.IsEnabled = true;   
                }
                }
                else if(PostTime > DateTime.Now)
                {
                    await DisplayAlert("الطلبات", "لم يمضِ الوقت الكافي لارسال طلب اخر يرجى الانتظار لمدة "+ (PostTime - DateTime.Now).ToString().Remove(8, 8) + " دقائق ثم حاول مرة اخرى "+" او قم بالاتصال بخدمة الزبائن ", "حسناَ");
                    btnRec.IsEnabled = true;
                }
               
            }
            btnRec.IsEnabled = true;
            
           
        }

       async private void BtnSave_Clicked(object sender, EventArgs e)
        {
            if (await IsEmpty() == false)
            {
                return;
            }
            else
            {
                Preferences.Set("UName", txtName.Text);
                Preferences.Set("UAdress",txtAdress.Text);
                Preferences.Set("UPhone",txtPhone.Text);
                Preferences.Set("ULong", lblLongtude.Text);
                Preferences.Set("ULitit", lblLatitude.Text);
                ShowHideSub();
                LblInfo.Text = "تم تحديث معلومات التوصيل ✅";
                LblInfo.TextColor = Color.Green;
                var StartChild = stkMain.Children.FirstOrDefault();
                await SCRbasket.ScrollToAsync(StartChild, ScrollToPosition.Start, true);
            }

        }

       async private void Button_Clicked_2(object sender, EventArgs e)
        {
            SQLQuery ss = new SQLQuery();

            bool answer = await DisplayAlert("تأكيد الحذف !!", "هل تريد فعلا تفريغ السلة و حذف محتوياتها ؟؟", "نعم", "لا");
            if (answer == true) 
            {
             ss.DeleteAllRowsBasket();
             DeletItem();
            }
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMall.Models.SqlServerModels;
using ZenMall.ViewModels.SQLite;
using ZenMall.ViewModels.SqlServerADO;
using ZenMall.ViewModels.SqlServerViewModels;

namespace ZenMall.Views.ProdactDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProdactsDetailTry : ContentPage
    {
        string ImgURL;
        int ProID;
        public ProdactsDetailTry(string ImageUrl)
        {
            InitializeComponent();
            ImgURL = ImageUrl.Remove(0,5);

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
          
            SetDatasSource();
            string IsConnStr = Preferences.Get("Conn", null);

            if (IsConnStr == "" || IsConnStr == null)
            {
                BtnSave.IsVisible = false;
                BtnDelete.IsVisible = false;
                txtImgUrl.IsVisible = false;
                LblImgUrl.IsVisible = false;
                StkMainPDet.IsEnabled = false;
                
            }
            else
            {
                BtnDelete.IsVisible = true;
                BtnSave.IsVisible = true;
                txtImgUrl.IsVisible = true;
                LblImgUrl.IsVisible = true;
                StkMainPDet.IsEnabled = true;
               
            }
            
        }
        public async void SetDatasSource()
        {
            SqlADOMain ssAdo = new SqlADOMain();
            PckSaction.ItemsSource = ssAdo.LstSactionAll;
            try
            {
                SCLMain.IsVisible = false;
                AILoading.IsVisible = true;
                SQLdata ss = new SQLdata();

                var table = await ss.Datasourcee("");
                var searchresult =  table.Where(c => c.PImage.Contains(ImgURL));
                foreach (var s in searchresult)
                {
                    txtpname.Text = s.PName;
                    txtpdiss.Text = s.PDiscraption;
                    txtpdiscuntt.Text = s.PDiscount.ToString();
                    txtpmarke.Text = s.PMarks;
                    txtppricee.Text = s.PPrice.ToString() ;
                    imgPimage.Source = s.PImage;
                    PckSaction.SelectedItem = s.PSaction;
                    txtImgUrl.Text = s.PImage;
                    ProID =  int.Parse(s.ProdactID.ToString());

                }
                AILoading.IsVisible = false;
                SCLMain.IsVisible = true;
                BtnAddToBasket.IsVisible = true;
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
            var searchresult = table.Where(c => c.PImage.Contains(ImgURL));
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

       async private void BtnSave_Clicked(object sender, EventArgs e)
        {
            
            try
            {
                if (await IsEmpty() == true)
                {
                
                StkMainPDet.IsEnabled = false;
                BtnSave.IsEnabled = false;
                var ss = new ProdactsAdoMain();
                ProdactsM ProM = new ProdactsM();
                ProM.PDiscraption = txtpdiss.Text;
                ProM.PName = txtpname.Text;
                ProM.PImage = txtImgUrl.Text;
                ProM.PMarks = txtpmarke.Text;
                ProM.PDiscount = int.Parse(txtpdiscuntt.Text);
                ProM.PPrice = int.Parse(txtppricee.Text);
                ProM.PSaction = PckSaction.SelectedItem.ToString();
                ss.UpdateProdact(ProID, ProM);
                BtnSave.IsEnabled = true;
                StkMainPDet.IsEnabled = true;
                await DisplayAlert("حفظ", "تم حفظ التعديلات على المنتوج بنجاح !", "حسنا");
                BtnSave.Text = "حفظ التعديلات";
                }
            }
            catch
            {

            }
           
        }
        private async Task<bool> IsEmpty()
        {
            BtnSave.Text = "يرجى الانتظار ...";
            if (txtImgUrl.Text == "" || txtImgUrl.Text == null || txtpmarke.Text == "" || txtpmarke.Text == null || txtpdiscuntt.Text == "" || txtpdiscuntt.Text == null || txtppricee.Text == "" || txtppricee.Text == null || txtpdiss.Text == "" || txtpdiss.Text == null || txtpname.Text == "" || txtpname.Text == null)
            {

                if (txtpname.Text == "" || txtpname.Text == null)
                {
                    txtpname.Focus();
                    return await Task.FromResult(false);
                }
                else if (txtpmarke.Text == "" || txtpmarke.Text == null)
                {
                    txtpmarke.Focus();
                    return await Task.FromResult(false);

                }
                else if (txtpdiscuntt.Text == "" || txtpdiscuntt.Text == null)
                {
                    txtpdiscuntt.Focus();
                    return await Task.FromResult(false);
                }
                else if (txtppricee.Text == "" || txtppricee.Text == null)
                {
                    txtppricee.Focus();
                    return await Task.FromResult(false);
                }
                else if (txtpdiss.Text == "" || txtpdiss.Text == null)
                {
                    txtpdiss.Focus();
                    return await Task.FromResult(false);
                }
                else if (txtImgUrl.Text == "" || txtImgUrl.Text == null)
                {
                    txtImgUrl.Focus();
                    return await Task.FromResult(false);
                }


                return await Task.FromResult(false);
            }
            else
                return await Task.FromResult(true);
        }

        async private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            


                bool answer = await DisplayAlert("تأكيد الحذف !!", "هل تريد فعلا حذف هذا المنتوج ؟؟", "نعم", "لا");
                if (answer == true)
                      try
                       {
                          BtnDelete.IsEnabled = false;
                          var ss = new ProdactsAdoMain();
                          ss.DeleteProdacts(ProID);
                          await DisplayAlert("الحذف", "تم حذف المنتج بنجاح !", "حسنا");
                          await Navigation.PopAsync();
                       }
                       catch(Exception ex)
                        {
                          await DisplayAlert("", ex.Message, "OK");
                        }


                
                    
           
           
        }
    }
}
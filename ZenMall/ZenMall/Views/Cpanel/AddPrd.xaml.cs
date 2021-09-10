using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMall.Models.SqlServerModels;
using ZenMall.ViewModels.SqlServerADO;
using ZenMall.ViewModels.SqlServerViewModels;

namespace ZenMall.Views.Cpanel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPrd : ContentPage
    {
        public AddPrd()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            SqlADOMain ss = new SqlADOMain();
            PckMainSac.ItemsSource = ss.LstMainSaction;
        }
        private async Task<bool> IsEmpty()
        {
            if (txtImgURL.Text == "" || txtImgURL.Text == null || txtMark.Text == "" || txtMark.Text == null || txtOfer.Text == "" || txtOfer.Text == null || txtPrice.Text == "" || txtPrice.Text == null || txtSub.Text == "" || txtSub.Text == null || txtTitel.Text == "" ||txtTitel.Text==null )
            {
               
                if (txtTitel.Text == "" || txtTitel.Text == null)
                {
                    txtTitel.Focus();
                    return await Task.FromResult(false);
                }
                else if (txtMark.Text == "" || txtMark.Text == null)
                {
                    txtMark.Focus();
                    return await Task.FromResult(false);

                }
                else if (txtOfer.Text == "" || txtOfer.Text== null)
                {
                    txtOfer.Focus();
                    return await Task.FromResult(false);
                }
                else if (txtPrice.Text == "" || txtPrice.Text== null)
                {
                    txtPrice.Focus();
                    return await Task.FromResult(false);
                }
                else if (txtSub.Text == "" || txtSub.Text== null)
                {
                    txtSub.Focus();
                    return await Task.FromResult(false);
                }
                else if (txtImgURL.Text == "" || txtImgURL.Text== null)
                {
                    txtImgURL.Focus();
                    return await Task.FromResult(false);
                }


                return await Task.FromResult(false);
            }
            else
                return await Task.FromResult(true);
        }

       async private void BtnAdd_Clicked(object sender, EventArgs e)
        {

            try
            {
                if (await IsEmpty() == true)
                {
                //var ProdCount = new SQLdata();
                
                var ss = new ProdactsAdoMain();
                ProdactsM ProM = new ProdactsM();
                ProM.PDiscraption = txtSub.Text;
                ProM.PName = txtTitel.Text;
                ProM.PImage = txtImgURL.Text;
                ProM.PMarks = txtMark.Text;
                ProM.PDiscount = int.Parse(txtOfer.Text);
                ProM.PPrice = int.Parse(txtPrice.Text);
                ProM.PSaction = PckSaction.SelectedItem.ToString();
                //ProM.ProdactID = await ProdCount.ProdactsCounts()+1;
                txtImgURL.Text = "";
                txtMark.Text = "";
                txtOfer.Text = "";
                txtPrice.Text = "";
                txtSub.Text = "";
                txtTitel.Text = "";
                PckMainSac.ItemsSource = null;
                PckSaction.ItemsSource = null;
                ss.AddProdact(ProM);
                    await DisplayAlert("الاضافة", "تمت اضافة منتوج بنجاح", "حسنا");
                }
                
            }
            catch(Exception ex)
            {
                await DisplayAlert("خطأ", ex.Message, "OK");
            }
            
        }

        private void PckMainSac_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemSelectedP();
        }
        void ItemSelectedP()
        {
            SqlADOMain ss = new SqlADOMain();
            try
            {
            string Sel = PckMainSac.SelectedItem.ToString();
            switch (Sel)
            {
                case "السوبر ماركت":
                    PckSaction.ItemsSource = ss.LstSactionFoods;
                    break;
                case "المنظفات":
                    PckSaction.ItemsSource = ss.LstSactionCleans;
                    break;
                case "الهدايا":
                    PckSaction.ItemsSource = ss.LstSactionGifts;
                    break;
                case "تجميل":
                    PckSaction.ItemsSource = ss.LstSactionBeauty;
                    break;
                case "ادوات منزلية":
                    PckSaction.ItemsSource = ss.LstSactionHTools;
                    break;
                case "الملابس":
                    PckSaction.ItemsSource = ss.LstSactionCloths;
                    break;
                case "عطورات":
                    PckSaction.ItemsSource = ss.LstSactionProfiom;
                    break;
                case "المدخنين":
                    PckSaction.ItemsSource = ss.LstSactionSmoke;
                    break;
                case "قرطاسية":
                    PckSaction.ItemsSource = ss.LstSactionStationery;
                    break;
                case "اكسسوارات":
                    PckSaction.ItemsSource = ss.LstSactionAss;
                    break;
                case "السيارة":
                    PckSaction.ItemsSource = ss.LstSactionCar;
                    break;
                default:
                    PckSaction.ItemsSource = null;
                    break;
            }
            }
            catch
            {

            }
           
        }
    }
}
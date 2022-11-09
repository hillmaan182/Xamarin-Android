using MobileApp.Models;
using MobileApp.Services;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class ProductUserDetailPage : ContentPage
    {
        ProductService ps;
        VendorService vs;

        public ProductUserDetailPage()
        {
            InitializeComponent();
            ps = new ProductService();
            vs = new VendorService();
            this.BindingContext = new CardDataViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            showProduct();
            showVendor();
            
        }
        private void showProduct()
        {
            var res = ps.GetProductById(1).Result;
            var newRes = from q in res
                         select new { q.ID, ProductImage = q.ProductImage == "" ? null : q.ProductImage, q.ProductName, ProductPrice = "Rp. " + q.ProductPrice.ToString(), ProductSisa = "Sisa Produk : " + q.ProductSisa.ToString() , q.ProductDescription , q.ProductSpecification };
            lstData.ItemsSource = res;
        }

        private void showVendor()
        {
            var res = vs.GetVendorById(1).Result;
            lstDataVendor.ItemsSource = res;
        }

        private void Visit_Vendor(object sender, EventArgs e)
        {
            var getParam = ((Button)sender).CommandParameter;
            int idVendor = Convert.ToInt32(getParam);
            Shell.Current.GoToAsync($"{nameof(CompanyProfilePage)}?Param={idVendor}");
        }

    }
}
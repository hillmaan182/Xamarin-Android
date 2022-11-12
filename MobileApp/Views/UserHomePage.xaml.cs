using MobileApp.Services;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserHomePage : ContentPage 
    {
        VendorService vs;
        ProductService ps;
        public UserHomePage()
        {
            vs = new VendorService();
            InitializeComponent();
            this.BindingContext = new CardDataViewModel();
            showData();
        }

        private void showData()
        {
            var res = vs.GetAllVendorByCategory("Product").Result;
            lstProductVendor.ItemsSource = res;

            var res2 = vs.GetAllVendorByCategory("Service").Result;
            lstServiceVendor.ItemsSource = res2;

            var db = getContext();

            var query = from q in db.Product
                        join x in db.Vendor on q.VendorID equals x.ID
                        where q.ProductCategory == "Product"
                        select new { q.ID, q.ProductImage, q.ProductName, q.ProductPrice, x.VendorName };

            lstProduct.ItemsSource = query.ToList();

            var query2 = from q in db.Product
                         join x in db.Vendor on q.VendorID equals x.ID
                         where q.ProductCategory == "Service"
                         select new { q.ID, q.ProductImage, q.ProductName, q.ProductPrice, x.VendorName };

            lstService.ItemsSource = query2.ToList();
        }

        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }

        private async void btnProduct_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//VendorProductPage");
        }

        private async void btnService_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//VendorListPage");
            //await Shell.Current.GoToAsync("//ServiceUserPage");
        }
    }
}
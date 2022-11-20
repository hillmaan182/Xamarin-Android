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
    public partial class VendorServicePage : ContentPage
    {
        ProductService ps;
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }
        public VendorServicePage()
        {
            InitializeComponent();
            ps = new ProductService();
            showProduct();
        }
        private void showProduct()
        {
            var db = getContext();
            var query = from q in db.Product
                        join x in db.Vendor on q.VendorID equals x.ID
                        where q.ProductCategory == "Service"
                        select new { q.ID, q.ProductImage, q.ProductName, q.ProductPrice, x.VendorName };

            lstData.ItemsSource = query.ToList();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;
            TapGestureRecognizer tapGesture = (TapGestureRecognizer)frame.GestureRecognizers[0];

            var getParam = tapGesture.CommandParameter;
            int id = Convert.ToInt32(getParam);
            await Shell.Current.GoToAsync($"{nameof(ProductUserDetailPage)}?Param1={id}");
        }

        private async void serviceBtn_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(VendorServicePage)}");
        }

        private async void vendorBtn_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(VendorServiceListPage)}");
        }

        private void searchService_TextChanged(object sender, TextChangedEventArgs e)
        {
            var db = getContext();
            var query = from q in db.Product
                        join x in db.Vendor on q.VendorID equals x.ID
                        where q.ProductCategory == "Service" && q.ProductName.ToLower().Contains(searchService.Text.ToLower())
                        select new { q.ID, q.ProductImage, q.ProductName, q.ProductPrice, x.VendorName };

            lstData.ItemsSource = query.ToList();
        }
    }
}
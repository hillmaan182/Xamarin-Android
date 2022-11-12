using MobileApp.Models;
using MobileApp.Services;
using MobileApp.ViewModels;
using SQLite;
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
    public partial class ProductPage : ContentPage
    {
        ProductService services;
        int? VendorID;
        public ProductPage()
        {
            InitializeComponent();
            services = new ProductService();
            this.BindingContext = new CardDataViewModel();
            VendorID = ((App)App.Current).vendorID;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            showProduct();
        }
        private void showProduct()
        {
            var res = services.GetAllProductsVendor("Product", VendorID).Result;
            var newRes = from q in res
                         select new { q.ID, ProductImage = q.ProductImage == "" ? null : q.ProductImage , q.ProductName, ProductPrice = "Rp. " + q.ProductPrice.ToString() , ProductSisa = "Sisa Produk : " + q.ProductSisa.ToString() };
            lstData.ItemsSource = res;
        }
        private async void btnAddRecord_Clicked(object sender, EventArgs e)
        {
            //this.Navigation.PushAsync(new AddProductPage());
            await Shell.Current.GoToAsync("//AddProductPage");
        }

        private async void lstData_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem != null)
            {
                Products obj = (Products)e.SelectedItem;
                string res = await DisplayActionSheet("Operation", "Cancel", null, "Delete");

                switch (res)
                {
                    case "Delete":
                        services.DeleteProduct(obj);
                        showProduct();
                        break;
                }
                lstData.SelectedItem = null;
            }
        }
    }
}
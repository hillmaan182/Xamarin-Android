using MobileApp.Models;
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
    public partial class TestPage : ContentPage
    {
        ProductService services;
        public TestPage()
        {
            InitializeComponent();
            services = new ProductService();
            this.BindingContext = new CardDataViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            showProduct();
        }
        private void showProduct()
        {
            var res = services.GetAllProductsVendor("Product", 1).Result;
            var newRes = from q in res
                         select new { q.ID, ProductImage = q.ProductImage == "" ? null : q.ProductImage, q.ProductName, ProductPrice = "Rp. " + q.ProductPrice.ToString(), ProductSisa = "Sisa Produk : " + q.ProductSisa.ToString() };
            lstData.ItemsSource = res;
        }

        private async  void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //var param = ((TappedEventArgs)e).Parameter;
            Frame frame = (Frame)sender;
            TapGestureRecognizer tapGesture = (TapGestureRecognizer)frame.GestureRecognizers[0];
            await DisplayAlert("Info", "You selected " + tapGesture.CommandParameter.ToString(), "Ok");
        }

        //private async void lstData_ItemSelected(object sender, CurrentItemChangedEventArgs e)
        //{

        //    if (e.CurrentItem != null)
        //    {
        //        Products obj = (Products)e.CurrentItem;
        //        string res = await DisplayActionSheet("Operation", "Cancel", null, "Delete");

        //        switch (res)
        //        {
        //            case "Delete":
        //                services.DeleteProduct(obj);
        //                showProduct();
        //                break;
        //        }
        //        lstData.SelectedItem = null;
        //    }
        //}
    }
}
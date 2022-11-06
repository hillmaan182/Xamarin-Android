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
        public ProductPage()
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
            var res = services.GetAllProducts("Product").Result;
            lstData.ItemsSource = res;
        }
        private void btnAddRecord_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AddProductPage());
        }

        private async void lstData_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           
            if (e.SelectedItem != null)
            {
              
                Products obj = (Products)e.SelectedItem;
                string res = await DisplayActionSheet("Operation", "Cancel", null,"Delete");

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
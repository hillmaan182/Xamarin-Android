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
    public partial class ProductUserPage : ContentPage
    {
       
        public ProductUserPage()
        {
            InitializeComponent();
        }

        private void searchProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            //var res = vs.GetAllVendorProductByName(searchProduct.Text).Result;
            //lstData.ItemsSource = res;
        }
    }
}
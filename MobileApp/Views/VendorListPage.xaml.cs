using MobileApp.Services;
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
    public partial class VendorListPage : ContentPage
    {
        VendorService vs;
        public VendorListPage()
        {
            InitializeComponent();
            vs = new VendorService();
            showVendor();
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    showVendor();
        //}
        private void showVendor()
        {
            var res = vs.GetAllVendor().Result;
            lstData.ItemsSource = res;
        }

        private void searchVendor_TextChanged(object sender, TextChangedEventArgs e)
        {
            var res = vs.GetAllVendorByName(searchVendor.Text).Result;
            lstData.ItemsSource = res;
        }
    }
}
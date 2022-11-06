using MobileApp.Services;
using MobileApp.Models;
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
    public partial class CompanyProfilePage : ContentPage
    {
        VendorService vs;
        int? id;
        public CompanyProfilePage()
        {
            id = ((App)App.Current).vendorID;
            InitializeComponent();
            vs = new VendorService();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            showVendorProfile();
        }
        private void showVendorProfile()
        {
            var res = vs.GetVendorById(id).Result;
            lvVendor.ItemsSource = res;
        }
    }
}
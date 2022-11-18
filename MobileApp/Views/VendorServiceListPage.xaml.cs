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
    public partial class VendorServiceListPage : ContentPage
    {
        VendorService vs;
        public VendorServiceListPage()
        {
            InitializeComponent();
            vs = new VendorService();
            showVendor();
        }
        private void showVendor()
        {
            var res = vs.GetAllVendorByCategory("Service").Result;
            lstData.ItemsSource = res;
        }

        private void searchVendor_TextChanged(object sender, TextChangedEventArgs e)
        {
            var res = vs.GetAllVendorServiceByName(searchVendor.Text).Result;
            lstData.ItemsSource = res;
        }

        private void btnVisit_Clicked(object sender, EventArgs e)
        {
            var getParam = ((Button)sender).CommandParameter;
            int idVendor = Convert.ToInt32(getParam);
            Shell.Current.GoToAsync($"{nameof(CompanyProfilePage)}?Param={idVendor}");
        }
        private async void serviceBtn_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(VendorServicePage)}");
        }

        private async void vendorBtn_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(VendorServicePage)}");
        }
    }
}
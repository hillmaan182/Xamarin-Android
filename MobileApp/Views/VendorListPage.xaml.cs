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
            var res = vs.GetAllVendorByCategory("Product").Result;
            lstData.ItemsSource = res;
        }

        private void searchVendor_TextChanged(object sender, TextChangedEventArgs e)
        {
            var res = vs.GetAllVendorProductByName(searchVendor.Text).Result;
            lstData.ItemsSource = res;
        }

        private void btnVisit_Clicked(object sender, EventArgs e)
        {
            var getParam = ((Button)sender).CommandParameter;
            int idVendor = Convert.ToInt32(getParam);
            Shell.Current.GoToAsync($"//{nameof(CompanyCataloguePage)}?Param={idVendor}");
            //Shell.Current.GoToAsync($"{nameof(CompanyProfilePage)}?Param={idVendor}");
        }
        private async void productBtn_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(VendorProductPage)}");
        }

        private async void vendorBtn_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(VendorListPage)}");
        }

    }
}
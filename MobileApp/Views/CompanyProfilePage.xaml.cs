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
    [QueryProperty("Param", "Param")]
    public partial class CompanyProfilePage : ContentPage
    {
        VendorService vs;
        int? id;

        string param;
        public string Param
        {
            set
            {
                param = Uri.UnescapeDataString(value ?? string.Empty);
            }
            get
            {
                return param;
            }
        }

        public CompanyProfilePage()
        {
            id = ((App)App.Current).vendorID;
            InitializeComponent();
            vs = new VendorService();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            int? vendorId;
            if (id != null)
            {
                vendorId = id;
            }else
            {
                vendorId = Convert.ToInt32(param);
            }
            showVendorProfile(vendorId);
        }
        private void showVendorProfile(int? idvendor)
        {
            var res = vs.GetVendorById(idvendor).Result;
            lvVendor.ItemsSource = res;
        }
    }
}
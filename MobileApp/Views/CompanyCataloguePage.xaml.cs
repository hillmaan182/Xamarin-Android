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
    public partial class CompanyCataloguePage : ContentPage
    {
        ProductService ps;
        int? id;
        int? vendorId;
        string VendorName;

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
        public CompanyCataloguePage()
        {
            id = ((App)App.Current).vendorID;
            InitializeComponent();
            ps = new ProductService();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (id != null)
            {
                vendorId = id;
            }
            else
            {
                vendorId = Convert.ToInt32(param);
            }

            showProductVendor();
        }

        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }

        private void showProductVendor()
        {
            var db = getContext();
            var query = from q in db.Product
                        join x in db.Vendor on q.VendorID equals x.ID
                        where q.VendorID == vendorId
                        select new { q.ID, x.VendorName, q.ProductName, q.ProductImage, q.ProductPrice };

            lstProduct.ItemsSource = query.ToList();

            var query2 = from q in db.Vendor
                         where q.ID == vendorId
                         select new { q.VendorName, q.VendorImage };

            lvVendor.ItemsSource = query2.ToList();
        }

        private void btnProfile_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"//{nameof(CompanyProfilePage)}?Param={vendorId}");
        }
    }
}
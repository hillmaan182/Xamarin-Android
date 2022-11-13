using MobileApp.Services;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    public partial class VendorHomePage : ContentPage
    {
        ProductService ps;
        int? vendorId;
        public VendorHomePage()
        {
            //User = ((App)App.Current).userLoggedIn;
            InitializeComponent();
            ps = new ProductService();
            vendorId = ((App)App.Current).vendorID;
            this.BindingContext = new VendorHomeViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            showAll();
        }

        private void showAll()
        {
            var db = getContext();
            var query = (from q in db.Transaction
                        join x in db.Vendor on q.VendorID equals x.ID
                        where q.Status == "New Order"
                        group q by new { q.Status }
                        into g
                        select new { Total = g.Count() == 0 ? 0 : g.Count() });

            var query2 = from q in db.Transaction.DefaultIfEmpty()
                        join x in db.Vendor on q.VendorID equals x.ID
                        where q.Status == "Ready to Ship"
                        group q by new { q.Status }
                        into g
                        select new { Total = g.Count() };

            var res = ps.GetAllProductsByIdVendor(vendorId);

            var seen = from q in db.Product
                       where q.VendorID == vendorId
                       group q by new { q.VendorID }
                       into g
                       select new { ID = g.Key.VendorID, Total =  g.Sum(x=> x.ProductSeen) };

            lstData.ItemsSource = query.ToList();
            lstData2.ItemsSource = query2.ToList();
            lstData7.ItemsSource = seen.ToList();

        }

        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }
    }
}
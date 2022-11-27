using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Models;
using MobileApp.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("Param", "Param")]
    public partial class HistoryTransaction : ContentPage
    {
        TransactionService ts;
        string param;
        int? VendorID;
        int UserID;
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

        public HistoryTransaction()
        {
            ts = new TransactionService();
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            VendorID = ((App)App.Current).vendorID;
            UserID = ((App)App.Current).userID;
            showTransaction(param);
        }

        private void showTransaction(string status)
        {
            var db = getContext();

            if (status == "Finished")
            {
                var query = from q in db.Transaction
                            join x in db.Product on q.ProductID equals x.ID
                            join z in db.User on q.UserID equals z.ID
                            join y in db.Shipyard on z.ID equals y.UserID
                            where q.Status == status && q.VendorID == VendorID
                            select new { q.ID, x.ProductName, x.ProductImage, q.BuyDate, q.TotalItem, q.TotalPrice, y.ShipyardName };

                lstData.ItemsSource = query.ToList();
            }
            else
            {
                var query = from q in db.Transaction
                            join x in db.Product on q.ProductID equals x.ID
                            join z in db.User on q.UserID equals z.ID
                            join y in db.Shipyard on z.ID equals y.UserID
                            where q.Status != status && q.VendorID == VendorID
                            select new { q.ID, x.ProductName, x.ProductImage, q.BuyDate, q.TotalItem, q.TotalPrice, y.ShipyardName };

                lstData.ItemsSource = query.ToList();
            }

        }

        private void searchTrans_TextChanged(object sender, TextChangedEventArgs e)
        {
            var db = getContext();
            string paramSearch = searchTrans.Text.ToLower();

            var query = from q in db.Transaction
                        join x in db.Product on q.ProductID equals x.ID
                        join z in db.User on q.UserID equals z.ID
                        join y in db.Shipyard on z.ID equals y.UserID
                        where x.ProductName.ToLower().ToString().Contains(paramSearch) && q.Status == param && q.VendorID == VendorID
                        select new { q.ID, x.ProductName, x.ProductImage, q.BuyDate, q.TotalItem, q.TotalPrice, y.ShipyardName };

            lstData.ItemsSource = query.ToList();
        }
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }
    }
}
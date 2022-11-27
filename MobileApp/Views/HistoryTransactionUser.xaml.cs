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
    public partial class HistoryTransactionUser : ContentPage
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
        public HistoryTransactionUser()
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
                            join z in db.Vendor on q.VendorID equals z.ID
                            where q.Status == status && q.UserID == UserID
                            select new { q.ID, x.ProductName, x.ProductImage, q.BuyDate, q.TotalItem, q.TotalPrice, z.VendorName };

                lstData.ItemsSource = query.ToList();
            }
            else
            {
                var query = from q in db.Transaction
                            join x in db.Product on q.ProductID equals x.ID
                            join z in db.Vendor on q.VendorID equals z.ID
                            where q.Status != status && q.UserID == UserID
                            select new { q.ID, x.ProductName, x.ProductImage, q.BuyDate, q.TotalItem, q.TotalPrice, z.VendorName };

                lstData.ItemsSource = query.ToList();
            }
        }

        private void searchTrans_TextChanged(object sender, TextChangedEventArgs e)
        {
            var db = getContext();
            string param = searchTrans.Text.ToLower();

            var query = from q in db.Transaction
                        join x in db.Product on q.ProductID equals x.ID
                        join z in db.Vendor on q.VendorID equals z.ID
                        where x.ProductName.ToLower().ToString().Contains(param)
                        select new { q.ID, x.ProductName, x.ProductImage, q.BuyDate, q.TotalItem, q.TotalPrice, z.VendorName };

            lstData.ItemsSource = query.ToList();
        }
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }
    }
}
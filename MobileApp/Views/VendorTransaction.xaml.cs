using MobileApp.Models;
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
    public partial class VendorTransaction : ContentPage
    {
        TransactionService ts;
        private int? vendorId;
        public VendorTransaction()
        {
            InitializeComponent();
            this.BindingContext = new CardDataViewModel();
            ts = new TransactionService();
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vendorId = ((App)App.Current).vendorID;
            showData();
        }

        private void showData()
        {
            var db = getContext();
            var query = from q in db.Transaction
                        join x in db.Vendor on q.VendorID equals x.ID
                        join z in db.Product on q.ProductID equals z.ID
                        where q.VendorID == vendorId && q.Status == "New Order"
                        select new {q.ID , z.ProductImage, z.ProductName , x.VendorName , q.TotalPrice , q.TotalItem , q.BuyDate};

            var query2 = from q in db.Transaction
                        join x in db.Vendor on q.VendorID equals x.ID
                        join z in db.Product on q.ProductID equals z.ID
                        where q.VendorID == vendorId && q.Status == "Ready to Ship"
                        select new { q.ID, z.ProductImage, z.ProductName, x.VendorName, q.TotalPrice, q.TotalItem, q.BuyDate };

            var query3 = from q in db.Transaction
                        join x in db.Vendor on q.VendorID equals x.ID
                        join z in db.Product on q.ProductID equals z.ID
                        where q.VendorID == vendorId && q.Status == "Finished"
                        select new { q.ID, z.ProductImage, z.ProductName, x.VendorName, q.TotalPrice, q.TotalItem, q.BuyDate };

            lstNew.ItemsSource = query.ToList();
            lstShip.ItemsSource = query2.ToList();
            lstDone.ItemsSource = query3.ToList();

        }

        private void btnNew_Clicked(object sender, EventArgs e)
        {
            btnNew.BackgroundColor = Color.Gray;
            btnShip.BackgroundColor = Color.Beige;
            btnDone.BackgroundColor = Color.Beige;
            lstNew.IsVisible = true;
            lstShip.IsVisible = false;
            lstDone.IsVisible = false;
        }

        private void btnShip_Clicked(object sender, EventArgs e)
        {
            btnNew.BackgroundColor = Color.Beige;
            btnShip.BackgroundColor = Color.Gray;
            btnDone.BackgroundColor = Color.Beige;
            lstNew.IsVisible = false;
            lstShip.IsVisible = true;
            lstDone.IsVisible = false;
        }

        private void btnDone_Clicked(object sender, EventArgs e)
        {
            btnNew.BackgroundColor = Color.Beige;
            btnShip.BackgroundColor = Color.Beige;
            btnDone.BackgroundColor = Color.Gray;
            lstNew.IsVisible = false;
            lstShip.IsVisible = false;
            lstDone.IsVisible = true;
        }

        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }

        private void btnShipProduct_Clicked(object sender, EventArgs e)
        {
            var getParam = ((Button)sender).CommandParameter;
            int idTrans = Convert.ToInt32(getParam);

            var db = getContext();
            var res = db.Transaction.Where(x => x.ID == idTrans);
            foreach (var x in res)
            {
                Transaction obj = new Transaction();
                obj.ID = x.ID;
                obj.VendorID = x.VendorID;
                obj.UserID = x.UserID;
                obj.ProductID = x.ProductID;
                obj.BuyDate = x.BuyDate;
                obj.Price = x.Price;
                obj.TotalPrice = x.TotalPrice;
                obj.TotalItem = x.TotalItem;
                obj.Status = "Ready to Ship";
                ts.UpdateStatus(obj);
            }
            showData();
        }

        private void btnFinishOrder_Clicked(object sender, EventArgs e)
        {
            var getParam = ((Button)sender).CommandParameter;
            int idTrans = Convert.ToInt32(getParam);
            var db = getContext();
            var res = db.Transaction.Where(x => x.ID == idTrans);
            foreach (var x in res)
            {
                Transaction obj = new Transaction();
                obj.ID = x.ID;
                obj.VendorID = x.VendorID;
                obj.UserID = x.UserID;
                obj.ProductID = x.ProductID;
                obj.BuyDate = x.BuyDate;
                obj.Price = x.Price;
                obj.TotalPrice = x.TotalPrice;
                obj.TotalItem = x.TotalItem;
                obj.Status = "Finished";
                ts.UpdateStatus(obj);
            }
            showData();
        }
    }
}
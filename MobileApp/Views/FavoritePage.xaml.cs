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
    public partial class FavoritePage : ContentPage
    {
        FavoriteProductService fp;
        int userID;
        public FavoritePage()
        {
            InitializeComponent();
            fp = new FavoriteProductService();
            this.BindingContext = new CardDataViewModel();
            userID = ((App)App.Current).userID;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            showAll();

        }
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }
        private void showAll()
        {
            var db = getContext();

            var favoriteProduct = from q in db.FavoriteProduct
                                  join x in db.Product on q.ProductId equals x.ID
                                  join z in db.Vendor on x.VendorID equals z.ID
                                  where q.UserId == userID && q.Type == "Product"
                                  select new { x.ID, x.ProductImage, x.ProductName, x.ProductPrice, z.VendorName };

            lstProduct.ItemsSource = favoriteProduct.ToList();

            var favoriteService = from q in db.FavoriteProduct
                                  join x in db.Product on q.ProductId equals x.ID
                                  join z in db.Vendor on x.VendorID equals z.ID
                                  where q.UserId == userID && q.Type == "Service"
                                  select new { x.ID, x.ProductImage, x.ProductName, x.ProductPrice, z.VendorName };

            lstService.ItemsSource = favoriteService.ToList();

            var favoriteProductVendor = from q in db.FavoriteVendor
                                        join z in db.Vendor on q.VendorId equals z.ID
                                        where q.UserId == userID && q.Type == "Product"
                                        select new { z.ID, z.VendorImage, z.VendorName, z.VendorAddress, z.VendorPhone };

            lstProductVendor.ItemsSource = favoriteProductVendor.ToList();

            var favoriteServiceVendor = from q in db.FavoriteVendor
                                  join z in db.Vendor on q.VendorId equals z.ID
                                  where q.UserId == userID && q.Type == "Service"
                                  select new { z.ID, z.VendorImage, z.VendorName, z.VendorAddress, z.VendorPhone };

            lstServiceVendor.ItemsSource = favoriteServiceVendor.ToList();

        }

    }
}
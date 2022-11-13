using MobileApp.Models;
using MobileApp.Services;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("Param1", "Param1")]
    public partial class ProductUserDetailPage : ContentPage
    {
        ProductService ps;
        VendorService vs;
        TransactionService ts;
        UserService us;
        FavoriteProductService fp;
        int productID;
        int vendorID;
        string vendorName;
        int userID;
        int total = 0;
        int favoriteId = 0;
        string type;
        string userName;
        string param1;
        public string Param1
        {
            set
            {
                param1 = Uri.UnescapeDataString(value ?? string.Empty);
            }
            get
            {
                return param1;
            }
        }


        public ProductUserDetailPage()
        {
            InitializeComponent();
            ps = new ProductService();
            vs = new VendorService();
            ts = new TransactionService();
            us = new UserService();
            fp = new FavoriteProductService();
            this.BindingContext = new CardDataViewModel();
            userID = ((App)App.Current).userID;
            userName = ((App)App.Current).userLoggedIn;
            btnFavorite2.IsVisible = false;

            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            productID = Convert.ToInt32(param1);
            showAll(productID);
            updateSeen();
        }

        private void showAll(int id)
        {
            var res = ps.GetProductById(id).Result;
            vendorID = res.FirstOrDefault().VendorID;
            type = res.FirstOrDefault().ProductCategory;

            var getVendorName = us.GetDataUserByVendorId(vendorID).Result;
            vendorName = getVendorName.FirstOrDefault().Username;

            var getFav = fp.GetFavoriteProducts(productID, userID).Result;
            if (getFav.Count > 0)
            {
                favoriteId = getFav.FirstOrDefault().ID;
            }

            showProduct(id);
            showVendor(vendorID);
        }

        private void showProduct(int id)
        {
            var res = ps.GetProductById(id).Result;
            var newRes = from q in res
                         select new { q.ID, ProductImage = q.ProductImage == "" ? null : q.ProductImage, q.ProductName, ProductPrice = "Rp. " + q.ProductPrice.ToString(), ProductSisa = "Sisa Produk : " + q.ProductSisa.ToString(), q.ProductDescription, q.ProductSpecification };
            lstData.ItemsSource = res;
        }

        private void showVendor(int id)
        {
            var res = vs.GetVendorById(id).Result;
            lstDataVendor.ItemsSource = res;
        }

        private void Visit_Vendor(object sender, EventArgs e)
        {
            var getParam = ((Button)sender).CommandParameter;
            int idVendor = Convert.ToInt32(getParam);
            Shell.Current.GoToAsync($"{nameof(CompanyProfilePage)}?Param={idVendor}");
        }

        private void insertTransaction()
        {
            try
            {
                Transaction obj = new Transaction();
                obj.VendorID = vendorID;
                obj.ProductID = productID;
                obj.UserID = userID;
                obj.Status = "New Order";
                obj.BuyDate = DateTime.Now;
                obj.TotalItem = Convert.ToInt32(totalCnt.Text);

                var getPrice = ps.GetProductById(productID).Result;
                obj.Price = getPrice.FirstOrDefault().ProductPrice;
                obj.TotalPrice = obj.TotalItem * obj.Price;

                ts.InsertTransaction(obj);
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.ToString(), "OK");
            }

        }

        private void btnChat_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new ChatsPage(userName, vendorName));
        }

        private void btnBuy_Clicked(object sender, EventArgs e)
        {
            try
            {
                insertTransaction();
                DisplayAlert("Success", "You've just buy the product", "OK");
                Shell.Current.GoToAsync("//ProductUserPage");
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.ToString(), "OK");
            }
        }

        private void btnMinusTotal_Clicked(object sender, EventArgs e)
        {
            if (total != 0)
            {
                total -= 1;
            }
            totalCnt.Text = total.ToString();
        }

        private void btnAddTotal_Clicked(object sender, EventArgs e)
        {
            total += 1;
            totalCnt.Text = total.ToString();
        }
        
        private void btnFavorite_Clicked(object sender, EventArgs e)
        {
            try
            {
                btnFavorite.IsVisible = false;
                btnFavorite2.IsVisible = true;

                FavoriteProduct obj = new FavoriteProduct();
                obj.ProductId = productID;
                obj.UserId = userID;
                obj.Type = type;
                fp.InsertFavoriteProduct(obj);
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.ToString(), "OK");
            }
        }

        private void btnFavorite2_Clicked(object sender, EventArgs e)
        {
            btnFavorite.IsVisible = true;
            btnFavorite2.IsVisible = false;
            if (favoriteId != 0)
            {
               int c = fp.DelFavoriteProduct(favoriteId);
            }
        }

        private void updateSeen()
        {
            var db = getContext();
            var res = db.Product.Where(x => x.ID == productID);
            foreach (var x in res)
            {
                Products obj = new Products();
                obj.ID = x.ID;
                obj.ProductName = x.ProductName;
                obj.ProductPrice = x.ProductPrice;
                obj.ProductSisa = x.ProductSisa;
                obj.ProductCategory = x.ProductCategory;
                obj.ProductDescription = x.ProductDescription;
                obj.ProductImage = x.ProductImage;
                obj.ProductSeen = x.ProductSeen + 1;
                obj.VendorID = x.VendorID;
                ps.UpdateProduct(obj);
            }
        }

        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }
    }
}
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
        FavoriteVendorService fv;
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
            fv = new FavoriteVendorService();
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

            //var getVendorName = us.GetDataUserByVendorId(vendorID).Result;
            //vendorName = getVendorName.FirstOrDefault().Username;

            var getFav = fp.GetFavoriteProducts(productID, userID).Result;
            if (getFav.Count > 0)
            {
                btnFavorite.IsVisible = false;
                btnFavorite2.IsVisible = true;
                favoriteId = getFav.FirstOrDefault().ID;
            }

            showProduct(id);
            showVendor(vendorID);
            showReview(id);
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

        private void showReview(int id)
        {
            var db = getContext();
            var query = from q in db.Review
                        join z in db.Shipyard on q.UserId equals z.UserID
                        where q.ProductId == id
                        select new { q.ID, q.Rating, q.Description, z.ShipyardName };

            lstReview.ItemsSource = query.ToList();
        }

        private void Visit_Vendor(object sender, EventArgs e)
        {
            var getParam = ((Button)sender).CommandParameter;
            int idVendor = Convert.ToInt32(getParam);
            Shell.Current.GoToAsync($"//{nameof(CompanyCataloguePage)}?Param={idVendor}");
            //Shell.Current.GoToAsync($"{nameof(CompanyProfilePage)}?Param={idVendor}");
        }

        private void insertTransaction()
        {
            try
            {
                var res = ps.GetProductById(productID).Result;
                int sisa = res.FirstOrDefault().ProductSisa;
                if (Convert.ToInt32(totalCnt.Text) > sisa)
                {
                    throw new InvalidOperationException("Item amount exceeded");
                }

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

                var db = getContext();
                var resProd = db.Product.Where(x => x.ID == productID);
                foreach (var x in res)
                {
                    Products p = new Products();
                    p.ID = x.ID;
                    p.ProductName = x.ProductName;
                    p.ProductPrice = x.ProductPrice;
                    p.ProductSisa = x.ProductSisa - obj.TotalItem;
                    p.ProductCategory = x.ProductCategory;
                    p.ProductDescription = x.ProductDescription;
                    p.ProductImage = x.ProductImage;
                    p.ProductSeen = x.ProductSeen;
                    p.VendorID = x.VendorID;
                    ps.UpdateProduct(p);
                }

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

        private async void btnBuy_Clicked(object sender, EventArgs e)
        {
            try
            {
                insertTransaction();
                await DisplayAlert("Success", "You've just buy the product", "OK");
                totalCnt.Text = "0";
                //await Shell.Current.GoToAsync("//ProductUserPage");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
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

                FavoriteVendor objV = new FavoriteVendor();
                objV.VendorId = vendorID;
                objV.UserId = userID;
                objV.Type = type;
                fv.InsertFavoriteVendor(objV);

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
using MobileApp.Services;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VendorProductPage : ContentPage
    {
        ProductService ps;
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }

        //private const string url = "https://637958d97419b414df8dca03.mockapi.io/product/product";
        //private HttpClient _Client = new HttpClient();
        //private ObservableCollection<Products> _product;

        public VendorProductPage()
        {
            InitializeComponent();
            ps = new ProductService();
            showProduct();
        }
        //protected override void OnAppearing()
        //{
        //    try
        //    {
        //        base.OnAppearing();
        //        showProduct();
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.Write(ex.ToString());
        //    }

        //}
        private  void showProduct()
        {
            var db = getContext();
            var query = from q in db.Product
                        join x in db.Vendor on q.VendorID equals x.ID
                        where q.ProductCategory == "Product"
                        select new { q.ID, q.ProductImage, q.ProductName, q.ProductPrice, x.VendorName };

            lstData.ItemsSource = query.ToList();

            //var content = await  _Client.GetStringAsync(url);
            //var post = JsonConvert.DeserializeObject<List<Products>>(content);
            //_product = new ObservableCollection<Products>(post);

            //var query = from q in _product
            //            join x in db.Vendor on q.VendorID equals x.ID
            //            select new { q.ID, q.ProductImage, q.ProductName, q.ProductPrice, x.VendorName };

            //lstData.ItemsSource = query.ToList();

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;
            TapGestureRecognizer tapGesture = (TapGestureRecognizer)frame.GestureRecognizers[0];

            var getParam = tapGesture.CommandParameter;
            int id = Convert.ToInt32(getParam);
            await Shell.Current.GoToAsync($"{nameof(ProductUserDetailPage)}?Param1={id}");
        }

        private async void productBtn_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(VendorProductPage)}");
        }

        private async void vendorBtn_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(VendorListPage)}");
        }

        private void searchProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            var db = getContext();
            var query = from q in db.Product
                        join x in db.Vendor on q.VendorID equals x.ID
                        where q.ProductCategory == "Product" && q.ProductName.ToLower().Contains(searchProduct.Text.ToLower())
                        select new { q.ID, q.ProductImage, q.ProductName, q.ProductPrice, x.VendorName };

            lstData.ItemsSource = query.ToList();
        }
    }
}
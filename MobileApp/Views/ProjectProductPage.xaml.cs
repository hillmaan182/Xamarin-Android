using MobileApp.Services;
using MobileApp.ViewModels;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("Param1", "Param1")]
    [QueryProperty("Param2", "Param2")]
    public partial class ProjectProductPage : ContentPage
    {
        private int ProjectID;
        ProjectProductService pps;
        ProjectProductViewModel ppvm;
        ProductService ps;
        private string param1;
        private string param2;
        public DataTable newDT;

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

        public string Param2
        {
            set
            {
                param2 = Uri.UnescapeDataString(value ?? string.Empty);
            }
            get
            {
                return param2;
            }
        }

        public ProjectProductPage()
        {
            InitializeComponent();
            pps = new ProjectProductService();
            ps= new ProductService();
            newDT = new DataTable();

            newDT.Columns.Add("ID", typeof(Int32));
            newDT.Columns.Add("ProductId", typeof(Int32));
            newDT.Columns.Add("ProjectId", typeof(Int32));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ProjectID = Convert.ToInt32(param1);
            string category = "";
            if (param2 != null)
            {
                category = param2;
            }
            showProduct(param2);
        }

        private void showProduct(string category)
        {
            var db = getContext();

            var query = from q in db.Product
                        join x in db.Vendor on q.VendorID equals x.ID
                        where q.ProductCategory == category
                        select new { q.ID, q.ProductImage, q.ProductName, q.ProductPrice, x.VendorName };

            var res = ps.GetAllProducts("Product").Result;
            lstData.ItemsSource = query.ToList();
        }

        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }

        private async void addProjectBtn_Clicked(object sender, EventArgs e)
        {
            var getParam = ((Button)sender).CommandParameter;
            int idProduct = Convert.ToInt32(getParam);
            newDT.Rows.Add(-1, idProduct, 0);
            ((App)App.Current).globalDT = newDT;
            await Shell.Current.GoToAsync($"//{nameof(AddShipyardProjectPage)}");

            ////ProjectProduct pp = new ProjectProduct();
            ////pp.ProductID = 1;
            ////pp.ProjectID = ProjectID;
            ////pps.InsertProjectProduct(pp);

            ////await Shell.Current.GoToAsync($"//{nameof( AddShipyardProjectPage)}");
            //Navigation.RemovePage(this);
            //await Navigation.PushAsync(new AddShipyardProjectPage());
        }

        private async void addServiceBtn_Clicked(object sender, EventArgs e)
        {
            var getParam = ((Button)sender).CommandParameter;
            int idProduct = Convert.ToInt32(getParam);
            newDT.Rows.Add(-1, idProduct, 0);
            ((App)App.Current).globalDT = newDT;
            await Shell.Current.GoToAsync($"//{nameof(AddShipyardProjectPage)}");
        }
    }
}
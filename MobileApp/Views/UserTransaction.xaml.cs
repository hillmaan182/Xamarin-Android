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
    public partial class UserTransaction : ContentPage
    {
        private int userId;
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }
        public UserTransaction()
        {
            InitializeComponent();
            this.BindingContext = new CardDataViewModel();
            userId = ((App)App.Current).userID;
            showData();
        }

        private void showData()
        {
            var db = getContext();
            var query = from q in db.Transaction
                        join x in db.Vendor on q.VendorID equals x.ID
                        join z in db.Product on q.ProductID equals z.ID
                        where q.UserID == userId && q.Status != "Finished"
                        select new { q.ID, z.ProductImage, z.ProductName, x.VendorName, q.TotalPrice, q.TotalItem, q.BuyDate };

            var query3 = from q in db.Transaction
                         join x in db.Vendor on q.VendorID equals x.ID
                         join z in db.Product on q.ProductID equals z.ID
                         where q.UserID == userId && q.Status == "Finished"
                         select new { q.ID, z.ProductImage, z.ProductName, x.VendorName, q.TotalPrice, q.TotalItem, q.BuyDate };

            lstNew.ItemsSource = query.ToList();
            lstDone.ItemsSource = query3.ToList();

        }

        private void btnAddReview_Clicked(object sender, EventArgs e)
        {
            var getParam = ((Button)sender).CommandParameter;
            int idProduct = Convert.ToInt32(getParam);
            Shell.Current.GoToAsync($"{nameof(ReviewAddPage)}?Param={idProduct}");
        }

        private void btnNew_Clicked(object sender, EventArgs e)
        {
            btnNew.BackgroundColor = Color.Gray;
            btnDone.BackgroundColor = Color.Beige;
            lstNew.IsVisible = true;
            lstDone.IsVisible = false;
        }

        private void btnDone_Clicked(object sender, EventArgs e)
        {
            btnNew.BackgroundColor = Color.Beige;
            btnDone.BackgroundColor = Color.Gray;
            lstNew.IsVisible = false;
            lstDone.IsVisible = true;
        }
    }
}
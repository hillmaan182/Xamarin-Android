using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Services;
using MobileApp.Models;
using System.Data;
using System.Data.SqlTypes;
using System.Configuration;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.IO;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddShipyardProjectPage : ContentPage
    {
        private int ShipyardID = -1;
        private int PPId;
        ProjectProductService pps;
        ProjectService ps;
        public AddShipyardProjectPage()
        {
            InitializeComponent();
            pps = new ProjectProductService();
            ps = new ProjectService();
            //showProduct();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var res = pps.GetProjectProductByShipyard(ShipyardID);
            var db = getContext();
            var query = from q in db.ProjectProduct
                        join x in db.Product on q.ProductID equals x.ID
                        where q.ProjectID == ShipyardID && x.ProductCategory == "Product"
                        select new { x.ID , x.ProductName , x.ProductImage , x.ProductPrice };

            var query2 = from q in db.ProjectProduct
                        join x in db.Product on q.ProductID equals x.ID
                        join z in db.Vendor on x.VendorID equals z.ID
                        where q.ProjectID == ShipyardID && x.ProductCategory == "Service"
                        select new { x.ID, x.ProductName, x.ProductImage, x.ProductPrice , z.VendorName };

            //lstDataProduct.ItemsSource = query.ToList();
            //lstDataService.ItemsSource = query2.ToList();

            var product = from q in db.Product
                          join x in db.Vendor on q.VendorID equals x.ID
                          where q.ProductCategory == "Product"
                          select new { q.ID, Product = q.ProductName.ToString() + " - " + q.ProductPrice.ToString()  + " - " + x.VendorName.ToString()};
            
            //picker.ItemsSource = product.ToList();

            if (((App)App.Current).globalDT != null)
            {
                DataTable dt = new DataTable();
                dt = ((App)App.Current).globalDT;

                List<ProjectProduct> lst = new List<ProjectProduct> ();
                int id = -1;
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new ProjectProduct { ID = id, ProductID = Convert.ToInt32(row["ProductId"]), ProjectID = Convert.ToInt32(row["ProjectId"]) });
                    //ProjectProduct obj = new ProjectProduct();
                    //lst.ID = id;
                    //obj.ProductID = Convert.ToInt32(row["ProductId"]);
                    //obj.ProjectID = Convert.ToInt32(row["ProjectId"]);
                    id--;
                }

                var queryProduct = from q in lst
                        join x in db.Product on q.ProductID equals x.ID
                         join z in db.Vendor on x.VendorID equals z.ID
                         where  x.ProductCategory == "Product"
                        select new { x.ID, x.ProductName, x.ProductImage, x.ProductPrice , z.VendorName };

                lstDataProduct.ItemsSource = queryProduct.ToList();


                var queryService = from q in lst
                                   join x in db.Product on q.ProductID equals x.ID
                                   join z in db.Vendor on x.VendorID equals z.ID
                                   where x.ProductCategory == "Service"
                                   select new { x.ID, x.ProductName, x.ProductImage, x.ProductPrice, z.VendorName };

                lstDataService.ItemsSource = queryService.ToList();
            }
           
        }

        protected override bool OnBackButtonPressed()
        {
            //base.OnBackButtonPressed();
            //((App)App.Current).globalDT = null;
            return true;
        }

        private void showProduct()
        {
            var db = getContext();
            var query = from q in db.ProjectProduct
                        join x in db.Product on q.ProductID equals x.ID
                        where q.ProjectID == ShipyardID && x.ProductCategory == "Product"
                        select new { x.ID, x.ProductName, x.ProductImage, x.ProductPrice };

            lstDataProduct.ItemsSource = query.ToList();
        }

        private async void btnAddProduct_Clicked(object sender, EventArgs e)
        {
            int id = -1;
            Navigation.RemovePage(this);
            await Shell.Current.GoToAsync($"{nameof(ProjectProductPage)}?Param1={id}&Param2=Product");
        }
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }

        private async void btnSaveUpdate_Clicked(object sender, EventArgs e)
        {
            try
            {
                //var a = PPId;
                //getAll ProductProject Where ShipyardID = -1
                Project p = new Project();
                p.ProjectName = txtShipName.Text;
                p.ProjectOwner = txtOwnerName.Text;
                if (rb1.IsChecked)
                {
                    p.ProjectCategory = "Ship Building";
                }
                else
                {
                    p.ProjectCategory = "Ship Repair";
                }
                var picker = sender as DatePicker;
                p.ProjectStartTime = startDate.Date;
                p.ProjectEndTime = endDate.Date;
                p.UserID = ((App)App.Current).userID;
                p.ProjectImage = PhotoPath;
                p.ProjectStatus = "OnGoing";
                ps.InsertProject(p);

                var db = getContext();

                DataTable insertDT = new DataTable();
                insertDT = ((App)App.Current).globalDT;
                if (insertDT != null)
                {
                    foreach (DataRow row in insertDT.Rows)
                    {
                        ProjectProduct pp = new ProjectProduct();
                        pp.ProductID = Convert.ToInt32(row["ProductId"]);
                        pp.ProjectID = p.ID;
                        pps.InsertProjectProduct(pp);
                    }
                }
                ((App)App.Current).globalDT = null;
                await Shell.Current.GoToAsync($"//{nameof(OtherPage)}");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error" , ex.ToString() , "OK");
            }
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            ((App)App.Current).globalDT = null;
            await Shell.Current.GoToAsync($"//{nameof(OtherPage)}");
        }

        private async void btnAddService_Clicked(object sender, EventArgs e)
        {
            int id = -1;
            Navigation.RemovePage(this);
            await Shell.Current.GoToAsync($"{nameof(ProjectProductPage)}?Param1={id}&Param2=Service");
        }
        void onRBChanged(object sender, CheckedChangedEventArgs e)
        {
            if (rb1.IsChecked)
            {
                rb2.IsChecked = false;
            }

            if (rb2.IsChecked)
            {
                rb1.IsChecked = false;
            }
        }
        private async void btnUplImg_Clicked(object sender, EventArgs e)
        {
            await PickerPhotoAsync();
        }

        string PhotoPath = null;
        async Task PickerPhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }
        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                PhotoPath = null;
                return;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (Stream stream = await photo.OpenReadAsync())

            using (var newStream = File.OpenWrite(newFile))
            {
                await stream.CopyToAsync(newStream);
            }
            PhotoPath = newFile;
            var stream2 = await photo.OpenReadAsync();
            //img.Source = ImageSource.FromStream(() => stream2);
            btnImage.Source = ImageSource.FromStream(() => stream2);
            btnImage.IsEnabled = false;
        }

    }
}
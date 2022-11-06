using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileApp.Services;
using MobileApp.Models;
using Xamarin.Essentials;
using System.IO;
using System.Threading.Tasks;

namespace MobileApp.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProductPage : ContentPage
    {
        ProductService _services;
        bool _isUpdate;
        int ProductID;
        public AddProductPage()
        {
            //btnImage.Source = "icon_image_add.png";
            InitializeComponent();
            _services = new ProductService();
            _isUpdate = false;
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
            //catch (FeatureNotSupportedException fnsEx)
            //{
            //    // Feature is not supported on the device
            //}
            //catch (PermissionException pEx)
            //{
            //    // Permissions not granted
            //}
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

        public AddProductPage(Products obj)
        {
            InitializeComponent();
            _services = new ProductService();
            if (obj != null)
            {
                string a = "Product";
                string b = "Service";
                ProductID = obj.ID;
                txtName.Text = obj.ProductName;
                txtPrice.Text = obj.ProductPrice;
                txtSisa.Text = obj.ProductSisa;
                txtDescription.Text = obj.ProductDescription;
                txtSpecification.Text = obj.ProductSpecification;
                PhotoPath = obj.ProductImage;
                if (rb1.IsChecked)
                {
                    a = obj.ProductCategory;
                }
                else
                {
                    b= obj.ProductCategory; 
                }
                _isUpdate = true;
            }
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

        private async void btnSaveUpdate_Clicked(object sender, EventArgs e)
        {
            Products obj = new Products();
            obj.ProductName = txtName.Text;
            obj.ProductPrice = txtPrice.Text;
            obj.ProductSisa = txtSisa.Text;
            obj.ProductImage = PhotoPath;
            obj.ProductSpecification = txtSpecification.Text;
            obj.ProductDescription = txtDescription.Text;
            if (rb1.IsChecked)
            {
                obj.ProductCategory = "Product";
            }
            else
            {
                obj.ProductCategory = "Service";
            }
            
            if (_isUpdate)
            {
                obj.ID = ProductID;
                await _services.UpdateProduct(obj);
            }
            else
            {
                _services.InsertProduct(obj);
            }
            //await this.Navigation.PopAsync();
            await this.Navigation.PushAsync(new ProductPage());
        }
        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new ProductPage());
        }
    }

}

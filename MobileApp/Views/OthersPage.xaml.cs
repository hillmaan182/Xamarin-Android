using MobileApp.Models;
using MobileApp.Services;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OthersPage : ContentPage
    {
        VendorService vs;
        int? VendorID;
        public OthersPage()
        {
            InitializeComponent();
            vs = new VendorService();
            VendorID = ((App)App.Current).vendorID;
            this.BindingContext = new OthersViewModel(false);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            showProfile();
        }

        private void showProfile()
        {
            var res = vs.GetVendorById(VendorID).Result;
            foreach(var x in res)
            {
                txtVendorName.Text = x.VendorName;
                txtVendorAddress.Text = x.VendorAddress;
                txtPhone.Text = x.VendorPhone;
                if (PhotoPath != null)
                {
                    vendorImage.Source = PhotoPath;
                }
                else
                {
                    if (x.VendorImage != null)
                    {
                        vendorImage.Source = ImageSource.FromFile(x.VendorImage);
                    }
                    else
                    {
                        //vendorImage.Source = x.VendorImage;
                        vendorImage.Source = ImageSource.FromFile("icon_noimage.png");
                    }
                   
                }
            }
            //lstData.ItemsSource = res;
        }

        private void Update_Clicked(object sender, EventArgs e)
        {
            this.BindingContext = new OthersViewModel(true);
            updateImg.IsVisible = false;
            txtVendorName.TextColor = Color.Gray;
            txtVendorAddress.TextColor = Color.Gray;
            txtVendorAddress.TextColor = Color.Gray;
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            this.BindingContext = new OthersViewModel(false);
            updateImg.IsVisible = true;
            showProfile();
        }

        private void Check_Clicked(object sender, EventArgs e)
        {
            try
            {
                var res = vs.GetVendorById(VendorID).Result;
                foreach (var x in res)
                {
                    Vendor obj = new Vendor();
                    obj.VendorName = txtVendorName.Text;
                    obj.VendorAddress = txtVendorAddress.Text;
                    obj.VendorEmail = x.VendorEmail;
                    obj.VendorPhone = x.VendorPhone;
                    obj.VendorAbout = x.VendorAbout;
                    obj.VendorMission = x.VendorMission;
                    obj.Type = x.Type;
                    obj.VendorImage = PhotoPath;
                    obj.ID = x.ID;
                    vs.UpdateVendor(obj);

                    this.BindingContext = new OthersViewModel(false);
                    updateImg.IsVisible = true;
                    showProfile();
                }
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException(ex.ToString());
            }
            
        }

        async Task PickerPhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                await LoadPhotoAsync(photo);
                //Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }
        string PhotoPath = null;
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
            vendorImage.Source = ImageSource.FromStream(() => stream2);
            //vendorImage.IsEnabled = false;
           
        }

        private async void btnUplImg_Clicked(object sender, EventArgs e)
        {
            await PickerPhotoAsync();
            //this.BindingContext = new OthersViewModel(true);
            //updateImg.IsVisible = false;
        }

        private async void OnGoing_Tapped(object sender, EventArgs e)
        {
            string status = "New Order";
            await Shell.Current.GoToAsync($"{nameof(HistoryTransaction)}?Param={status}");
        }

        private async void History_Tapped(object sender, EventArgs e)
        {
            //await this.Navigation.PushAsync(new HistoryTransaction());
            string status = "Finished";
            await Shell.Current.GoToAsync($"{nameof(HistoryTransaction)}?Param={status}");
        }

        private void Trusted_Tapped(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AddProductPage());
        }

        private void Account_Tapped(object sender, EventArgs e)
        {
            int? idVendor = ((App)App.Current).vendorID; 
            Shell.Current.GoToAsync($"//{nameof(CompanyCataloguePage)}?Param={idVendor}");
            //this.Navigation.PushAsync(new CompanyProfilePage());
        }

        private async void Legal_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(DocumentPage)}");
        }
        private async void Contact_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(ContactPage)}");
        }
    }
}
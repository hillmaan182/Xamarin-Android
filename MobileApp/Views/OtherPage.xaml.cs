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
    public partial class OtherPage : ContentPage
    {
        UserService us;
        ShipyardService ss;
        private string username;
        private int shipyardID;
        public OtherPage()
        {
            InitializeComponent();
            us = new UserService();
            ss = new ShipyardService();
            username = ((App)App.Current).userLoggedIn;
            shipyardID = ((App)App.Current).shipyardID;
            showProfile();
        }

        private void Update_Clicked(object sender, EventArgs e)
        {
            this.BindingContext = new OthersViewModel(true);
            updateImg.IsVisible = false;
            txtName.TextColor = Color.Gray;
            txtEmail.TextColor = Color.Gray;
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            this.BindingContext = new OthersViewModel(false);
            updateImg.IsVisible = true;
            showProfile();
        }

        private void showProfile()
        {
            var res = ss.GetShipyardById(shipyardID).Result;
            //var res = us.GetDataUser(username);
            foreach (var x in res)
            {
                txtName.Text = x.ShipyardName;
                txtEmail.Text = x.ShipyardEmail;
            }
            //lstData.ItemsSource = res;
        }

        private void Check_Clicked(object sender, EventArgs e)
        {

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
            btnUplImg.Source = ImageSource.FromStream(() => stream2);
        }

        private async void btnUplImg_Clicked(object sender, EventArgs e)
        {
            await PickerPhotoAsync();
            this.BindingContext = new OthersViewModel(true);
            updateImg.IsVisible = false;
        }

        private void Projects_Tapped(object sender, EventArgs e)
        {

        }

        private void History_Tapped(object sender, EventArgs e)
        {

        }

        private void Trusted_Tapped(object sender, EventArgs e)
        {

        }

        private void Chat_Tapped(object sender, EventArgs e)
        {

        }

        private void Account_Tapped(object sender, EventArgs e)
        {

        }

        private void Legal_Tapped(object sender, EventArgs e)
        {

        }

        private void Contact_Tapped(object sender, EventArgs e)
        {

        }
    }
}
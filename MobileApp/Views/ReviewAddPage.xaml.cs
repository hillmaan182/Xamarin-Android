using MobileApp.Models;
using MobileApp.Services;
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
    [QueryProperty("Param1", "Param1")]
    public partial class ReviewAddPage : ContentPage
    {
        private int star;
        private string param1;
        private int ProductId;
        ReviewService rs;
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

        public ReviewAddPage()
        {
            InitializeComponent();
            rs = new ReviewService();
            star1.IsVisible = true;
            star2.IsVisible = true;
            star3.IsVisible = true;
            star4.IsVisible = true;
            star5.IsVisible = true;
            star1b.IsVisible = false;
            star2b.IsVisible = false;
            star3b.IsVisible = false;
            star4b.IsVisible = false;
            star5b.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ProductId = Convert.ToInt32(param1);
        }

        private void star1_Clicked(object sender, EventArgs e)
        {
            star1.IsVisible = false;
            star2.IsVisible = true;
            star3.IsVisible = true;
            star4.IsVisible = true;
            star5.IsVisible = true;
            star1b.IsVisible = true;
            star2b.IsVisible = false;
            star3b.IsVisible = false;
            star4b.IsVisible = false;
            star5b.IsVisible = false;
            star = 1;
        }

        private void star2_Clicked(object sender, EventArgs e)
        {
            star1.IsVisible = false;
            star2.IsVisible = false;
            star3.IsVisible = true;
            star4.IsVisible = true;
            star5.IsVisible = true;
            star1b.IsVisible = true;
            star2b.IsVisible = true;
            star3b.IsVisible = false;
            star4b.IsVisible = false;
            star5b.IsVisible = false;
            star = 2;
        }

        private void star3_Clicked(object sender, EventArgs e)
        {
            star1.IsVisible = false;
            star2.IsVisible = false;
            star3.IsVisible = false;
            star4.IsVisible = true;
            star5.IsVisible = true;
            star1b.IsVisible = true;
            star2b.IsVisible = true;
            star3b.IsVisible = true;
            star4b.IsVisible = false;
            star5b.IsVisible = false;
            star = 3;
        }

        private void star4_Clicked(object sender, EventArgs e)
        {
            star1.IsVisible = false;
            star2.IsVisible = false;
            star3.IsVisible = false;
            star4.IsVisible = false;
            star5.IsVisible = true;
            star1b.IsVisible = true;
            star2b.IsVisible = true;
            star3b.IsVisible = true;
            star4b.IsVisible = true;
            star5b.IsVisible = false;
            star = 4;
        }

        private void star5_Clicked(object sender, EventArgs e)
        {
            star1.IsVisible = false;
            star2.IsVisible = false;
            star3.IsVisible = false;
            star4.IsVisible = false;
            star5.IsVisible = false;
            star1b.IsVisible = true;
            star2b.IsVisible = true;
            star3b.IsVisible = true;
            star4b.IsVisible = true;
            star5b.IsVisible = true;
            star = 5;
        }

        private void star1b_Clicked(object sender, EventArgs e)
        {
            star1.IsVisible = true;
            star2.IsVisible = true;
            star3.IsVisible = true;
            star4.IsVisible = true;
            star5.IsVisible = true;
            star1b.IsVisible = false;
            star2b.IsVisible = false;
            star3b.IsVisible = false;
            star4b.IsVisible = false;
            star5b.IsVisible = false;
            star = 0;
        }

        private void star2b_Clicked(object sender, EventArgs e)
        {
            star1.IsVisible = false;
            star2.IsVisible = true;
            star3.IsVisible = true;
            star4.IsVisible = true;
            star5.IsVisible = true;
            star1b.IsVisible = true;
            star2b.IsVisible = false;
            star3b.IsVisible = false;
            star4b.IsVisible = false;
            star5b.IsVisible = false;
            star = 1;
        }

        private void star3b_Clicked(object sender, EventArgs e)
        {
            star1.IsVisible = false;
            star2.IsVisible = false;
            star3.IsVisible = true;
            star4.IsVisible = true;
            star5.IsVisible = true;
            star1b.IsVisible = true;
            star2b.IsVisible = true;
            star3b.IsVisible = false;
            star4b.IsVisible = false;
            star5b.IsVisible = false;
            star = 2;
        }

        private void star4b_Clicked(object sender, EventArgs e)
        {
            star1.IsVisible = false;
            star2.IsVisible = false;
            star3.IsVisible = false;
            star4.IsVisible = true;
            star5.IsVisible = true;
            star1b.IsVisible = true;
            star2b.IsVisible = true;
            star3b.IsVisible = true;
            star4b.IsVisible = false;
            star5b.IsVisible = false;
            star = 3;
        }

        private void star5b_Clicked(object sender, EventArgs e)
        {
            star1.IsVisible = false;
            star2.IsVisible = false;
            star3.IsVisible = false;
            star4.IsVisible = false;
            star5.IsVisible = true;
            star1b.IsVisible = true;
            star2b.IsVisible = true;
            star3b.IsVisible = true;
            star4b.IsVisible = true;
            star5b.IsVisible = false;
            star = 4;
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            Review obj = new Review();
            obj.Rating = star;
            obj.Image = PhotoPath;
            obj.Description = txtReview.Text;
            obj.ProductId = Convert.ToInt32(param1);
            obj.UserId = ((App)App.Current).userID;
            int c= rs.InsertReview(obj);

            await Shell.Current.GoToAsync($"{nameof(UserTransaction)}");
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
            btnImage.Source = ImageSource.FromStream(() => stream2);
            btnImage.IsEnabled = false;
        }

        private async void btnImage_Clicked(object sender, EventArgs e)
        {
            await PickerPhotoAsync();
        }
    }
}
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private bool isVendor;
        public bool IsVendor { get => isVendor; set => SetProperty(ref isVendor, value); }
        //public bool IsAdmin { get { return isAdmin; }
        //    set { isAdmin = value; OnPropertyChanged(); }
        //}
        public AboutViewModel()
        {
            //MessagingCenter.Subscribe<LoginViewModel>(this, message: "vendor", (sender) => { IsVendor = true; });

            //IsVendor = ((App)App.Current).isVendor;
            //IsUser = ((App)App.Current).isUser;
            //if (IsVendor == true)
            //{
            //    Title = "Vendor Home";

            //}

            //if (IsUser == true)
            //{
               Title = "User Home";
            //}
            //IsAdmin = false;
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}
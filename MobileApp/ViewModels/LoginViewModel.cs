using MobileApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Action DisplayInvalidLoginPrompt;

        
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
            }
        }
        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);

        }

        private async void OnLoginClicked(object obj)
        {
          
            if (email == "vendor")
            {
                //((App)App.Current).isVendor = true;
                //((App)App.Current).isUser = false;
                MessagingCenter.Send<LoginViewModel>(this, (email == "vendor") ? "vendor" : "user");
                ((App)App.Current).userLoggedIn = email;
                await Shell.Current.GoToAsync($"//{nameof(VendorHomePage)}");
            } else if (email == "user")
            {
                MessagingCenter.Send<LoginViewModel>(this, (email == "vendor") ? "vendor" : "user");
                ((App)App.Current).userLoggedIn = email;
                await Shell.Current.GoToAsync($"//{nameof(UserHomePage)}");
                //await Shell.Current.GoToAsync("//main");
                //((App)App.Current).isVendor = false;
                //((App)App.Current).isUser = true;
            } else
            {
                await App.Current.MainPage.DisplayAlert("Invalid login", "email or password is wrong", "OK");
            }
        }

      
       
        //public void OnSubmit()
        //{
        //    if (email == "vendor" && password == "vendor")
        //    {
        //        App.Current.MainPage.Navigation.PushAsync(new VendorHomePage());
        //    }
        //    else if (email == "user" && password == "user")
        //    {
        //        App.Current.MainPage.Navigation.PushAsync(new UserHomePage());
        //    }
        //    else
        //    {
        //        DisplayInvalidLoginPrompt();
        //    }
        //}

    }
}

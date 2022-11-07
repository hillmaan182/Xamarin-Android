using MobileApp.Models;
using MobileApp.Services;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        UserService user;
        VendorService vendor;
        public LoginPage()
        {
            user = new UserService();
            vendor = new VendorService();

            addInitial();
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
        public LoginPage(User obj)
        {
            InitializeComponent();
            user = new UserService();
            if (obj != null)
            {
                txtUsername.Text = obj.Password;
                txtPassword.Text = obj.Password;
            }
        }
        private void BtnRegister_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//RegisterPage");
            ((App)App.Current).userLoggedIn = "";
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            int val = user.LoginUser(txtUsername.Text, txtPassword.Text).Result;
            if (val == 1)
            {
                int? vendor = user.GetVendorUser(txtUsername.Text, txtPassword.Text);
                var res = user.GetDataUser(txtUsername.Text);

                ((App)App.Current).vendorName = res.FirstOrDefault().Username;

                ((App)App.Current).vendorID = vendor;
                ((App)App.Current).userLoggedIn = txtUsername.Text;
                ((App)App.Current).IsUserLoggedIn = true;
                if (res.FirstOrDefault().Role == "Vendor")
                {
                    MessagingCenter.Send<LoginPage>(this, (res.FirstOrDefault().Role == "Vendor") ? "vendor" : "user");
                    await Shell.Current.GoToAsync($"//{nameof(VendorHomePage)}");
                } else if (res.FirstOrDefault().Role == "Shipyard")
                {
                    MessagingCenter.Send<LoginPage>(this, (res.FirstOrDefault().Role == "Shipyard") ? "vendor" : "user");
                    await Shell.Current.GoToAsync($"//{nameof(VendorHomePage)}");
                } else
                {
                    MessagingCenter.Send<LoginPage>(this, (res.FirstOrDefault().Role == "User") ? "user" : "vendor");
                    await Shell.Current.GoToAsync($"//{nameof(UserHomePage)}");
                }

                
            } else if (val == 2){
                int id = user.GetUserId(txtUsername.Text, txtPassword.Text);
                ((App)App.Current).userID = id;
                await Shell.Current.GoToAsync($"//{nameof(VerificationPage)}");
            }
            else
            {
                await DisplayAlert("Login Failed!", "username / password incorrect", "OK");
            }
        }

        public void addInitial()
        {
            Vendor obj = new Vendor();
            obj.VendorName = "Vendor A";
            obj.VendorAddress = "Jalan A no 1";
            obj.VendorPhone = "02112345678";
            obj.VendorEmail = "vendorA@gmail.com";
            vendor.InsertVendor(obj);

            User objUser = new User();
            objUser.Username = "vendora";
            objUser.Email = "vendora@gmail.com";
            objUser.Password = "vendora";
            objUser.Role = "Vendor";
            objUser.IsVerified = true;
            objUser.VerifiedCode = "1234";
            objUser.VendorID = obj.ID;
            //objUser.Vendor = obj;
            user.InsertUser(objUser);

            Vendor obj2 = new Vendor();
            obj2.VendorName = "Vendor B";
            obj2.VendorEmail = "vendorB@gmail.com";
            vendor.InsertVendor(obj2);

            User objUser2 = new User();
            objUser2.Username = "vendorb";
            objUser2.Email = "vendorb@gmail.com";
            objUser2.Password = "vendorb";
            objUser2.Role = "Shipyard";
            objUser2.IsVerified = true;
            objUser2.VerifiedCode = "1234";
            //objUser2.Vendor = obj2;
            objUser2.VendorID = obj2.ID;
            user.InsertUser(objUser2);

            User objUser3 = new User();
            objUser3.Username = "user";
            objUser3.Email = "user@gmail.com";
            objUser3.Password = "user";
            objUser3.Role = "User";
            objUser3.IsVerified = true;
            objUser3.VerifiedCode = "1234";
            user.InsertUser(objUser3);
        }

        
    }
}
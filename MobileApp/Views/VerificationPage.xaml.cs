using MobileApp.Models;
using MobileApp.Services;
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
    public partial class VerificationPage : ContentPage
    {
        UserService us;
        int userID;
        public VerificationPage()
        {
            InitializeComponent();
            us = new UserService();
            userID = ((App)App.Current).userID;
        }

        private async void BtnEnter_Clicked(object sender, EventArgs e)
        {
            var res = us.GetDataUserById(userID);
            string code = res.FirstOrDefault().VerifiedCode;

            string enterCode = code1.Text + code2.Text + code3.Text + code4.Text;
            if (enterCode != code)
            {
               await DisplayAlert("Error", "Verification Code invalid!", "OK");
            } else
            {
                foreach (var item in res.ToList())
                {
                    User obj = new User();
                    obj.Email = item.Email;
                    obj.Username = item.Username;
                    obj.Password = item.Password;
                    obj.VendorID = item.VendorID;
                    obj.Role = item.Role;
                    obj.VerifiedCode = item.VerifiedCode;
                    obj.IsVerified = true;
                    obj.ID = userID;
                    us.UpdateVerified(obj);
                }
                await DisplayAlert("Success" , "Verification is complete , you can log in now!" , "OK");
                //MessagingCenter.Send<VerificationPage>(this, "user");
                //Shell.Current.GoToAsync($"//{nameof(UserHomePage)}");
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }

        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }

    }
}
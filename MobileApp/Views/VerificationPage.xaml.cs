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

        private void BtnEnter_Clicked(object sender, EventArgs e)
        {
            var res = us.GetDataUserById(userID);
            string code = res.FirstOrDefault().VerifiedCode;

            string enterCode = code1.Text + code2.Text + code3.Text + code4.Text;
            if (enterCode != code)
            {
                DisplayAlert("Error", "Verification Code invalid!", "OK");
            } else
            {
                User obj = new User();
                obj.IsVerified = true;
                obj.ID = userID;
                us.UpdateVerified(obj);

                MessagingCenter.Send<VerificationPage>(this, "user");
                Shell.Current.GoToAsync($"//{nameof(UserHomePage)}");
            }
        }
    }
}
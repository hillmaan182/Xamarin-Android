using MobileApp.Services;
using MobileApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class AppViewModel : BaseViewModel
    {
        UserService services;
        private bool isVendor;
        private bool isUser;
        public bool IsVendor { get => isVendor; set => SetProperty(ref isVendor, value); }
        public bool IsUser { get => isUser; set => SetProperty(ref isUser, value); }
        public AppViewModel()
        {
            services = new UserService();
            MessagingCenter.Subscribe<LoginPage>(this, "vendor", (sender) =>
            {
                IsVendor = true;
                IsUser = false;
            });

            MessagingCenter.Subscribe<LoginPage>(this, "user", (sender) =>
            {
                IsVendor = false;
                IsUser = true;
            });

            MessagingCenter.Subscribe<VerificationPage>(this, "user", (sender) =>
            {
                IsVendor = false;
                IsUser = true;
            });



        }
    }
}
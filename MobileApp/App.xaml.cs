using MobileApp.Services;
using MobileApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    public partial class App : Application
    {
        public bool IsUserLoggedIn { get; set; }
        public  string userLoggedIn { get; set; }
        public int? vendorID { get; set; }
        public int userID { get; set; }
        public int msgID { get; set; }
        public string vendorName { get; set; }
        public string userName { get; set; }

        //public bool isVendor { get; set; }
        //public bool isUser { get; set; }
        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new AddProductPage());
            //DependencyService.Register<MockDataStore>();

            MainPage = new AppShell();
            if (!IsUserLoggedIn)
            {
                Shell.Current.GoToAsync("//LoginPage");
            }
        }
       
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

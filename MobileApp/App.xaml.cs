using MobileApp.Services;
using MobileApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data;

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

        public int shipyardID { get; set; }
        public DataTable globalDT { get; set; }

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
                //Shell.Current.GoToAsync("//AddShipyardProjectPage");
                //Shell.Current.GoToAsync($"//ProductUserDetailPage?param={id.ToString()}");
                //Shell.Current.GoToAsync($"{nameof(ProductUserDetailPage)}?Param=1");
                //Shell.Current.GoToAsync($"{nameof(ProductUserDetailPage)}?id=1");
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
            if (!IsUserLoggedIn)
            {
                Shell.Current.GoToAsync("//LoginPage");
            }else if (IsUserLoggedIn && vendorID == null)
            {
                Shell.Current.GoToAsync("//UserHomePage");
            }
            else if (IsUserLoggedIn && vendorID != null)
            {
                Shell.Current.GoToAsync("//VendorHomePage");
            }
        }
    }
}

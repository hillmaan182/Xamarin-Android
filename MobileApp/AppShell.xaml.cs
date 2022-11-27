using MobileApp.ViewModels;
using MobileApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MobileApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ProductUserDetailPage), typeof(ProductUserDetailPage));
            Routing.RegisterRoute(nameof(CompanyProfilePage), typeof(CompanyProfilePage));
            Routing.RegisterRoute(nameof(ShipyardProfilePage), typeof(ShipyardProfilePage));
            Routing.RegisterRoute(nameof(ReviewAddPage), typeof(ReviewAddPage));
            Routing.RegisterRoute(nameof(AddProductPage), typeof(AddProductPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(ProjectProductPage), typeof(ProjectProductPage));
            Routing.RegisterRoute(nameof(AddShipyardProjectPage), typeof(AddShipyardProjectPage));
            Routing.RegisterRoute(nameof(DocumentPage), typeof(DocumentPage));
            Routing.RegisterRoute(nameof(HistoryTransaction), typeof(HistoryTransaction));
            Routing.RegisterRoute(nameof(HistoryTransactionUser), typeof(HistoryTransactionUser));
            Routing.RegisterRoute(nameof(MessagePage), typeof(MessagePage));
            Routing.RegisterRoute(nameof(ContactPage), typeof(ContactPage));
            //Routing.RegisterRoute(nameof(ShipyardProjectPage), typeof(ShipyardProjectPage));

        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            ((App)App.Current).userLoggedIn = null;
            ((App)App.Current).vendorID = 0;
            ((App)App.Current).userID = 0;
            ((App)App.Current).shipyardID = 0;
            ((App)App.Current).msgID = 0;
            ((App)App.Current).userName = null;
            ((App)App.Current).vendorName = null;
            ((App)App.Current).globalDT = null;
            ((App)App.Current).IsUserLoggedIn = false;
            await Shell.Current.GoToAsync("//LoginPage");
            //await Shell.Current.GoToAsync("//VendorProductPage");
            //await Shell.Current.GoToAsync("//VendorListPage");
        }

        public class HiddenItem : ShellItem
        {

        }
    }
}

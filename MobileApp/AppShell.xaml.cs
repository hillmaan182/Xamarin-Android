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
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            ((App)App.Current).userLoggedIn = null;
            ((App)App.Current).vendorID = 0;
            ((App)App.Current).userID = 0;
            ((App)App.Current).msgID = 0;
            ((App)App.Current).vendorName = null;
            await Shell.Current.GoToAsync("//LoginPage");
        }

        public class HiddenItem : ShellItem
        {

        }
    }
}

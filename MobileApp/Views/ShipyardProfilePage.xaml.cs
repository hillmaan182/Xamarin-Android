using MobileApp.Services;
using MobileApp.Models;
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
    public partial class ShipyardProfilePage : ContentPage
    {
        ShipyardService ss;
        private int shipId;
        public ShipyardProfilePage()
        {
            InitializeComponent();
            ss = new ShipyardService();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            shipId = ((App)App.Current).shipyardID;
            showShipyardProfile(shipId);
        }
        private void showShipyardProfile(int shipyardId)
        {
            var res = ss.GetShipyardById(shipyardId).Result;
            lstShipyard.ItemsSource = res;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Models;
using MobileApp.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShipyardProjectPage : ContentPage
    {
        ProjectService ps;
        private int UserId;
        public ShipyardProjectPage()
        {
            InitializeComponent();
            ps = new ProjectService();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UserId = ((App)App.Current).userID;
            showProject("OnGoing");
        }
        private void showProject(string status)
        {
            var res = ps.GetProjectByStatus(status,UserId).Result;
            lstData.ItemsSource = res;
        }

        private async void addProjectBtn_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(AddShipyardProjectPage)}");
        }

        private void ongoingBtn_Clicked(object sender, EventArgs e)
        {
            showProject("OnGoing");
            finishedBtn.IsVisible = true;
        }

        private void finishedBtn_Clicked(object sender, EventArgs e)
        {
            showProject("Finished");
            finishedBtn.IsVisible = false;
        }
    }
}
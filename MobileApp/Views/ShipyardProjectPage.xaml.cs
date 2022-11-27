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
            var res = ps.GetProjectByStatus(status, UserId).Result;
            if (status == "OnGoing")
            {
                lstDataOngoing.ItemsSource = res;
            }
            else
            {
                lstDataFinished.ItemsSource = res;
            }
        }

        private async void addProjectBtn_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(AddShipyardProjectPage)}");
        }

        private void ongoingBtn_Clicked(object sender, EventArgs e)
        {
            scOngoing.IsVisible = true;
            scFinished.IsVisible = false;
            showProject("OnGoing");
        }

        private void finishedBtn_Clicked(object sender, EventArgs e)
        {
            scOngoing.IsVisible = false;
            scFinished.IsVisible = true;
            showProject("Finished");
        }

        private void btnFinished_Clicked(object sender, EventArgs e)
        {
            try
            {
                var getParam = ((Button)sender).CommandParameter;
                int idProject = Convert.ToInt32(getParam);
                var db = getContext();
                var query = from q in db.Project
                            where q.ID == idProject
                            select q;

                foreach (var x in query.ToList())
                {
                    Project p = new Project();
                    p.ProjectName = x.ProjectName;
                    p.ProjectOwner = x.ProjectOwner;
                    p.ProjectCategory = x.ProjectCategory;
                    p.ProjectStartTime = x.ProjectStartTime;
                    p.ProjectEndTime = x.ProjectEndTime;
                    p.UserID = x.UserID;
                    p.ProjectImage = x.ProjectImage;
                    p.ProjectStatus = "Finished";
                    p.ID = x.ID;

                    ps.UpdateProject(p);
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.ToString(), "OK");
            }
            
        }
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }

        private async void lstDataOngoing_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Project obj = (Project)e.SelectedItem;
                string res = await DisplayActionSheet("Operation", "Cancel", null, "Delete");

                switch (res)
                {
                    case "Delete":
                        ps.DeleteProject(obj);
                        showProject("OnGoing");
                        break;
                }
                lstDataFinished.SelectedItem = null;
            }
        }
    }
}
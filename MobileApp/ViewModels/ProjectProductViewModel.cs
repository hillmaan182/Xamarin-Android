using MobileApp.Services;
using MobileApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class ProjectProductViewModel : BaseViewModel
    {
        private int projectId;
        public int ProjectID{ get => projectId; set => SetProperty(ref projectId, value); }

        public ProjectProductViewModel()
        {
            MessagingCenter.Subscribe<AddShipyardProjectPage, int>(this, "IntPropertyMessage", (s,e) =>
            {
                ProjectID = e;
            });
        }
    }
}

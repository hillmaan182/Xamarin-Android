using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    public partial class VendorHomePage : ContentPage
    {
        public VendorHomePage()
        {
            //User = ((App)App.Current).userLoggedIn;
            InitializeComponent();
            this.BindingContext = new VendorHomeViewModel();
        }
    }
}
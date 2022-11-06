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
    public partial class OthersPage : ContentPage
    {
        public OthersPage()
        {
            InitializeComponent();
        }

        private void OnGoing_Tapped(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AddProductPage());
        }

        private void History_Tapped(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AddProductPage());
        }

        private void Trusted_Tapped(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AddProductPage());
        }

        private void Account_Tapped(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new CompanyProfilePage());
        }

        private void Legal_Tapped(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AddProductPage());
        }

        private void Contact_Tapped(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AddProductPage());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.ViewModels
{
    public class VendorHomeViewModel : BaseViewModel
    {
        public VendorHomeViewModel()
        {
            Title = "Vendor Home";
            User = ((App)App.Current).userLoggedIn;
        }

        
    }
}

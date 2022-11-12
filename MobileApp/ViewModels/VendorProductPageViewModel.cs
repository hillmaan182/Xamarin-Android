using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.ViewModels
{
    public class VendorProductPageViewModel : BaseViewModel
    {
        private bool isShow;
        public bool IsShow { get => isShow; set => SetProperty(ref isShow, value); }

        public VendorProductPageViewModel()
        {
            IsShow = true;
        }
    }
}

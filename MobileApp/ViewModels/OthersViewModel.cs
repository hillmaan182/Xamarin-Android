using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.ViewModels
{
    public class OthersViewModel : BaseViewModel
    {
        public OthersViewModel(bool flag)
        {
            if (flag == true)
            {
                IsEnabled = true;
            } else
            {
                IsEnabled = false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        public MessageViewModel()
        {
            User = ((App)App.Current).userLoggedIn;
        }
       
    }
}

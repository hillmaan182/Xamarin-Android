using MobileApp.Models;
using MobileApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        public ObservableCollection<Message> Messages { get; }
        public Command LoadItemsCommand { get; }
        MessageService ms;
        private string user;
        public string User { get => user; set => SetProperty(ref user, value); }
        public ChatViewModel(string uname)
        {
            ms = new MessageService();
            User = uname;
            Messages = new ObservableCollection<Message>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Messages.Clear();
                var res = ms.GetMessagesByUser("user", "vendora").Result;
                foreach (var item in res)
                {
                    if (item.MessageSender == "user")
                    {
                        //item.margin = new Thickness(10, 10, 80, 10);
                        //item.margin = "80";
                        //Mrg = new Thickness(10, 10, 80, 10);
                    }


                    else
                    {
                        //item.margin = new Thickness(80, 10, 10, 10);
                        //item.margin = "80";
                        //Mrg = new Thickness(80, 10, 10, 10);
                    }
                    Messages.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

       
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}

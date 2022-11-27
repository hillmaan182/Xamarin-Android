using MobileApp.Models;
using MobileApp.Services;
using MobileApp.ViewModels;
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
    public partial class MessagePage : ContentPage
    {
        MessageService ms;
        private string username;
        private string vendorname;
        private string receiver;
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }

        public MessagePage()
        {
            this.BindingContext = new MessageViewModel();
            InitializeComponent();
            ms = new MessageService();
            vendorname = ((App)App.Current).vendorName;
            username = ((App)App.Current).userLoggedIn;
            if (vendorname == "" || vendorname == null)
            {
                receiver = username;
            } else
            {
                receiver = vendorname;
            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var db = getContext();
            var check = (from q in db.Message
                        where q.ID == 7
                        select q).ToList();


            if (check.Count() == 0)
            {
                init();
            }
            showMessages();
        }
        private void showMessages()
        {
            //var res = ms.GetAllMessages().Result;
            //lstData.ItemsSource = res;

            var db = getContext();
            var query = from q in db.Message
                        where (q.MessageReceiver == receiver)
                        select q;

            var newQuery = from p in query
                           group p by new { p.MessageSender, p.MessageIsRead }
                           into g
                           select new { MessageSender = g.Key.MessageSender, MessageUnread = g.Key.MessageIsRead == false ? g.Count() : 0 };

            if (newQuery.Count() > 1)
            {
                var a = newQuery.Where(x => x.MessageUnread > 0).ToList();
                List<NewMessage> nmm = new List<NewMessage>();
                var res = a.Select(x => new { x.MessageSender, MessageUnread = x.MessageUnread.ToString() + " message unread" });
                foreach (var x in res)
                {
                    nmm.Add(new NewMessage { MessageSender = x.MessageSender, MessageUnread = x.MessageUnread });

                }
                //lstData.ItemsSource = a.Select(x => new { x.MessageSender, MessageUnread = x.MessageUnread.ToString() + " message unread" });
                lstData.ItemsSource = nmm;
            }
            else
            {
                var a = newQuery.ToList();
                lstData.ItemsSource = a.Select(x => new { x.MessageSender, MessageUnread = x.MessageUnread.ToString() + " message unread" });
            }

        }


        public void init()
        {

            var db = getContext();
          
            ms.delMsg();

            Message m = new Message();
            m.MessageSender = "user";
            m.MessageReceiver = "vendorp1";
            m.MessageBody = "Halo min";
            m.MessageTime = DateTime.Now;
            m.MessageIsRead = true;
            ms.InsertMessage(m);

            Message m2 = new Message();
            m2.MessageSender = "user";
            m2.MessageReceiver = "vendorp1";
            m2.MessageBody = "mau nanya nih ...";
            m2.MessageTime = DateTime.Now;
            m2.MessageIsRead = true;
            ms.InsertMessage(m2);

            Message m3 = new Message();
            m3.MessageSender = "vendorp1";
            m3.MessageReceiver = "user";
            m3.MessageBody = "iya kak kenapa";
            m3.MessageTime = DateTime.Now;
            m3.MessageIsRead = true;
            ms.InsertMessage(m3);

            Message m4 = new Message();
            m4.MessageSender = "user";
            m4.MessageReceiver = "vendorp1";
            m4.MessageBody = "Harga produk a berapa ya , aku ada rencana mau beli nih ...";
            m4.MessageTime = DateTime.Now;
            m4.MessageIsRead = false;
            ms.InsertMessage(m4);

            Message m5 = new Message();
            m5.MessageSender = "vendorp1";
            m5.MessageReceiver = "user";
            m5.MessageBody = "bisa ditanyakan ke nomor xxx ini kak untuk pertanyaan lebih lanjut ...";
            m5.MessageTime = DateTime.Now;
            m5.MessageIsRead = true;
            ms.InsertMessage(m5);

            Message m6 = new Message();
            m6.MessageSender = "userb";
            m6.MessageReceiver = "vendorp1";
            m6.MessageBody = "Halo min";
            m6.MessageTime = DateTime.Now;
            m6.MessageIsRead = false;
            ms.InsertMessage(m6);

            Message m7 = new Message();
            m7.MessageSender = "vendorp1";
            m7.MessageReceiver = "user";
            m7.MessageBody = "Iya kak";
            m7.MessageTime = DateTime.Now;
            m7.MessageIsRead = false;
            ms.InsertMessage(m7);

            var query = from q in db.Message
                        where (q.MessageReceiver == vendorname)
                        select q;

            var newQuery = from p in query
                           group p by new { p.MessageSender, p.MessageIsRead }
                           into g
                           select new { MessageSender = g.Key.MessageSender, MessageUnread = g.Key.MessageIsRead == false ? g.Count() : 0 };

            if (newQuery.Count() > 1)
            {
                var a = newQuery.Where(x => x.MessageUnread > 0).ToList();
                List<NewMessage> nmm = new List<NewMessage>();
                var res = a.Select(x => new { x.MessageSender, MessageUnread = x.MessageUnread.ToString() + " message unread" });
                foreach ( var x in res)
                {
                    nmm.Add(new NewMessage { MessageSender = x.MessageSender , MessageUnread = x.MessageUnread});
                   
                }
                //lstData.ItemsSource = a.Select(x => new { x.MessageSender, MessageUnread = x.MessageUnread.ToString() + " message unread" });
                lstData.ItemsSource = nmm;
            }
            else
            {
                var a = newQuery.ToList();
                lstData.ItemsSource = a.Select(x => new { x.MessageSender, MessageUnread = x.MessageUnread.ToString() + " message unread" });
            }

        }

        public class NewMessage{
            public string MessageSender { get; set; }   
            public string MessageUnread { get; set; }
        }

        private void lstData_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                string senderM;
                NewMessage obj = (NewMessage)e.SelectedItem;
                senderM = obj.MessageSender;

                if (senderM != "")
                {
                    this.Navigation.PushAsync(new ChatsPage(senderM, null));
                }
            }
        }
    }
}
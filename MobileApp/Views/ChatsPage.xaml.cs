using MobileApp.ViewModels;
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
    public partial class ChatsPage : ContentPage
    {
        MessageService ms;
        ChatViewModel _viewModel;
        private string Username;
        private string Vendorname;

        public ChatsPage(string uname , string vendor)
        {
            InitializeComponent();
            if (vendor == null)
            {
                this.BindingContext = new ChatViewModel(uname);
                Vendorname = ((App)App.Current).vendorName;
            } else
            {
                this.BindingContext = new ChatViewModel(vendor);
                Vendorname = vendor;
                Username = uname;
            }

            ms = new MessageService();
        }
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            init();


            //_viewModel.OnAppearing();
            showMessages();
        }

        private void showMessages()
        {
            var res = ms.GetMessagesByUser(Username, Vendorname).Result;
            lstData.ItemsSource = res;
        }
     
        //Example
        public void init()
        {
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
            m7.MessageReceiver = "usera";
            m7.MessageBody = "Iya kak";
            m7.MessageTime = DateTime.Now;
            m7.MessageIsRead = false;
            ms.InsertMessage(m7);

            var db = getContext();
            var query = from q in db.Message
                        where (q.MessageSender == Username && q.MessageReceiver == "vendora")
                        select q;

            var newQuery = from p in query
                           group p by new { p.MessageSender, p.MessageIsRead }
                           into g
                           select new { MessageSender = g.Key.MessageSender , MessageUnread = g.Key.MessageIsRead == false ? g.Count() : 0 };

            if (newQuery.Count() > 1)
            {
                var a = newQuery.Where(x => x.MessageUnread > 0).ToList();
            } else
            {
                var a = newQuery.ToList();
            }

        }

        private void Send_Tapped(object sender, EventArgs e)
        {
            Message obj = new Message();
            obj.MessageSender = Vendorname;
            obj.MessageReceiver = Username;
            obj.MessageBody = chatText.Text;
            obj.MessageTime = DateTime.Now;
            obj.MessageIsRead = false;
            ms.InsertMessage(obj);

            showMessages();
        }
    }
}
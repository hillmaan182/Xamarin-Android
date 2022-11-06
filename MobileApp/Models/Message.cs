using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.Models
{
    public class Message
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string MessageSender { get; set; }
        public string MessageReceiver { get; set; }
        public string MessageBody { get; set; }
        public DateTime MessageTime { get; set; }
        public bool MessageIsRead { get; set; }
        public bool Incoming { get; set; }
        public bool Outgoing { get; set; }
    }
}

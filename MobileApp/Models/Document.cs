using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.Models
{
    public class Document
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int UserId { get; set; }
        public int DocId { get; set; }
        public bool IsUpload { get; set; }

        public string FilePath { get; set; }
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class Review
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }

        public int TransactionId { get; set; }

    }
}

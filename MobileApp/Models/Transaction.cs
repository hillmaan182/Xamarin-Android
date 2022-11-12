using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int VendorID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }

        //New Order
        //Ready to Ship
        //Sold
        public string Status { get; set; }

        public DateTime BuyDate { get; set; }
        public int TotalItem { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }

    }
}

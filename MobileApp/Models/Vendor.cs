using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class Vendor
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string VendorName { get; set; }
        public string VendorEmail { get; set; }
        public string VendorPhone { get; set; }
        public string VendorAddress { get; set; }
        public string VendorImage { get; set; }
        public string VendorAbout { get; set; }
        public string VendorMission { get; set; }
        public string Type { get; set; }

    }
}

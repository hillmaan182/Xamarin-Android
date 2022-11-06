using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsVerified { get; set; }
        public string VerifiedCode { get; set; } 
        //FK
        public int? VendorID { get; set; }
        public Vendor Vendor { get; set; }
        
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Text;


namespace MobileApp.Models
{
    public class Shipyard
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string ShipyardName { get; set; }
        public string ShipyardEmail { get; set; }
        public string ShipyardPhone { get; set; }
        public string ShipyardAddress { get; set; }
        public string ShipyardImage { get; set; }

        public int UserID { get; set; }
    }
}

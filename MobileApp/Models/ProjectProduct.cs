using SQLite;
using System;

namespace MobileApp.Models
{
    public class ProjectProduct
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int ProjectID { get; set; }
    }
}

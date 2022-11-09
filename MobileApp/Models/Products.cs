using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp.Models
{
    public class Products
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int ProductSisa { get; set; }
        public string ProductImage { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSpecification { get; set; }
        public string ProductDescription { get; set; }

        public int VendorID { get; set; }
        public Vendor Vendor { get; set; }

    }
}

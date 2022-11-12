using SQLite;

namespace MobileApp.Models
{
    public class FavoriteVendor
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int UserId { get; set; }
        public int VendorId { get; set; }
        public string Type { get; set; }
    }
}

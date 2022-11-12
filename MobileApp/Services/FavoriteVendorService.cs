using Microsoft.EntityFrameworkCore;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;

namespace MobileApp.Services
{
    public class FavoriteVendorService
    {
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }

        public async Task<List<FavoriteVendor>> GetAllFavoriteVendors(string type)
        {
            var db = getContext();
            var res = await db.FavoriteVendor.Where(x=> x.Type == type).ToListAsync();
            return res;
        }

        public int InsertFavoriteVendor(FavoriteVendor obj)
        {
            try
            {
                var db = getContext();
                db.FavoriteVendor.Add(obj);
                int cnt = db.SaveChanges();
                return cnt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }

        }

    }
}

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
    public class FavoriteProductService
    {
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }

        public async Task<List<FavoriteProduct>> GetAllFavoriteProducts(string type)
        {
            var db = getContext();
            var res = await db.FavoriteProduct.Where(x => x.Type == type).ToListAsync();
            return res;
        }

        public async Task<List<FavoriteProduct>> GetFavoriteProducts(int productId, int userId)
        {
            var db = getContext();
            var res = await db.FavoriteProduct.Where(x => x.ProductId == productId && x.UserId == userId).ToListAsync();
            return res;
        }

        public int InsertFavoriteProduct(FavoriteProduct obj)
        {
            try
            {
                var db = getContext();
                db.FavoriteProduct.Add(obj);
                int cnt = db.SaveChanges();
                return cnt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }

        }

        public int DelFavoriteProduct(int id)
        {
            var db = getContext();
            var res = db.FavoriteProduct.Where(x => x.ID == id).ToList();
            foreach (var x in res)
            {
                db.Remove(x);
            }
            int c = db.SaveChanges();
            return c;
        }

    }
}

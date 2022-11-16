using Microsoft.EntityFrameworkCore;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    public class ShipyardService
    {
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }
        public async Task<List<Shipyard>> GetShipyardById(int id)
        {
            var db = getContext();
            var res = await db.Shipyard.Where(x => x.ID == id).ToListAsync();
            return res;
        }

        public async Task<List<Shipyard>> GetShipyardByUserId(int id)
        {
            var db = getContext();
            var result = await db.Shipyard.Where(x => x.UserID == id).ToListAsync();
            return result;
        }

        public int InsertShipyard(Shipyard obj)
        {
            var db = getContext();

            var result = db.Shipyard.Where(x => x.ShipyardName == obj.ShipyardName).Count();
            int cnt = 0;
            if (result == 0)
            {
                db.Shipyard.Add(obj);
                int c = db.SaveChanges();
                cnt = 1;
            }
            return cnt;
        }


    }
}

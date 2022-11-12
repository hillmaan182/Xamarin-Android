using Microsoft.EntityFrameworkCore;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    public class VendorService
    {
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }

        public int InsertVendor(Vendor obj)
        {
            var db = getContext();

            var result = db.Vendor.Where(x => x.VendorName == obj.VendorName).Count();
            int cnt = 0;
            if (result == 0)
            {
                db.Vendor.Add(obj);
                int c = db.SaveChanges();
                cnt = 1;
            }
            return cnt;
        }
        public async Task<List<Vendor>> GetVendorById(int? id)
        {
            var db = getContext();
            var res = await db.Vendor.ToListAsync();
            var result = await db.Vendor.Where(x => x.ID == id).ToListAsync();
            return result;
        }

        public async Task<List<Vendor>> GetAllVendor()
        {
            var db = getContext();
            var res = await db.Vendor.ToListAsync();
            return res;
        }

        public async Task<List<Vendor>> GetAllVendorByCategory(string type)
        {
            var db = getContext();
            var res = await db.Vendor.Where(x=> x.Type == type).ToListAsync();
            return res;
        }

        public async Task<List<Vendor>> GetAllVendorByName(string name)
        {
            var db = getContext();
            var res = await db.Vendor.Where(x=> x.VendorName.ToLower().Contains(name.ToLower())).ToListAsync();
            return res;
        }

    }
}

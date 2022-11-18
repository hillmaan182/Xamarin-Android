using Microsoft.EntityFrameworkCore;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MobileApp.Services
{
    public class ProjectProductService
    {
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }
        public async Task<List<ProjectProduct>> GetProjectProductByShipyard(int ShipyardId)
        {
            var db = getContext();
            var result = await db.ProjectProduct.Where(x => x.ProjectID == ShipyardId).ToListAsync();
            return result;
        }

        public int InsertProjectProduct(ProjectProduct obj)
        {
            var db = getContext();
            db.ProjectProduct.Add(obj);
            int c = db.SaveChanges();
            return c;
        }

        public int UpdateProjectProduct(ProjectProduct obj)
        {
            var db = getContext();
            db.ProjectProduct.Update(obj);
            int c = db.SaveChanges();
            return c;
        }

    }
}

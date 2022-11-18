using Microsoft.EntityFrameworkCore;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MobileApp.Services
{
    public class ProjectService
    {
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }
        public async Task<List<Project>> GetProjectByShipyard(int ShipyardId)
        {
            var db = getContext();
            var result = await db.Project.Where(x=> x.UserID == ShipyardId).ToListAsync();
            return result;
        }

        public async Task<List<Project>> GetProjectByStatus(string status , int userid)
        {
            var db = getContext();
            var result = await db.Project.Where(x=> x.UserID == userid && x.ProjectStatus == "OnGoing").ToListAsync();
            return result;
        }

        public int InsertProject(Project obj)
        {
            var db = getContext();
            db.Project.Add(obj);
            int c = db.SaveChanges();
            return c;
        }
    }
}

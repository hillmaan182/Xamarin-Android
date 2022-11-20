using Microsoft.EntityFrameworkCore;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    public class DocumentService
    {
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }
        public int GetDocumentByDocId(int id , int userid)
        {
            var db = getContext();
            var c =  db.Document.Where(x=> x.DocId == id && x.UserId == userid).ToListAsync().Result.Count();
            return c;
        }

        public int InsertDocument(Document obj)
        {
            var db = getContext();
            db.Document.Add(obj);
            int c = db.SaveChanges();

            return c;
        }

    }
}

using Microsoft.EntityFrameworkCore;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    public class TransactionService
    {
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }
        public int InsertTransaction(Transaction obj)
        {
            var db = getContext();
            db.Transaction.Add(obj);
            int cnt = db.SaveChanges();
            return cnt;
        }

        public int UpdateStatus(Transaction obj)
        {

            var db = getContext();
            db.Transaction.Update(obj);
            int c = db.SaveChanges();
            return c;

            //var db = getContext();
            //var res = db.Transaction.Where(x => x.ID == id);
            //foreach (var x in res)
            //{
            //    Transaction obj = new Transaction();
            //    obj.ID = x.ID;
            //    obj.VendorID = x.VendorID;
            //    obj.UserID = x.UserID;
            //    obj.Price = x.Price;
            //    obj.TotalPrice = x.TotalPrice;
            //    obj.TotalItem = x.TotalItem;
            //    obj.Status =status;
            //    db.Transaction.Update(obj);
            //    int c = db.SaveChanges();
            //}
            //return 1;

        }

        public async Task<List<Transaction>> GetAllTransactions()
        {
            var _dbContext = getContext();
            var res = await _dbContext.Transaction.ToListAsync();
            return res;
        }

        public async Task<List<Transaction>> GetAllTransactionsNewOrder(int? id)
        {
            var _dbContext = getContext();
            var res = await _dbContext.Transaction.Where(x=> x.VendorID == id && x.Status == "New Order").ToListAsync();
            return res;
        }

        public async Task<List<Transaction>> GetAllTransactionsReadyShip(int? id)
        {
            var _dbContext = getContext();
            var res = await _dbContext.Transaction.Where(x => x.VendorID == id && x.Status == "Ready to Ship").ToListAsync();
            return res;
        }

        public async Task<List<Transaction>> GetAllTransactionsFinished(int? id)
        {
            var _dbContext = getContext();
            var res = await _dbContext.Transaction.Where(x => x.VendorID == id && x.Status == "Finished").ToListAsync();
            return res;
        }

        public async Task<List<Transaction>> GetAllTransactionsById(int id)
        {
            var _dbContext = getContext();
            var res = await _dbContext.Transaction.Where(x => x.ID == id).ToListAsync();
            return res;
        }

    }
}

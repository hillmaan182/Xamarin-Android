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

        public async Task<List<Transaction>> GetAllTransactions()
        {
            var _dbContext = getContext();
            var res = await _dbContext.Transaction.ToListAsync();
            return res;
        }

    }
}

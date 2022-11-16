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
    public class MessageService
    {
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }

        public async Task<List<Message>> GetAllMessages()
        {
            var db = getContext();
            var res = await db.Message.ToListAsync();
            return res;
        }

        //public Task<List<Message>> GetMessages()
        //{
        //    var db = getContext();
        //    var query = from q in db.Message
        //                where (q.MessageSender == "user" && q.MessageReceiver == "vendora") || (q.MessageSender == "vendora" && q.MessageReceiver == "user")
        //                select q;

        //    var newQuery = from p in query
        //                   group p by p.MessageSender
        //                   into g
        //                   select new { g.Key, Count = g.Count() };

        //    return newQuery;
        //}

        public async Task<List<Message>> GetMessagesByUser(string user, string vendor)
        {
            try
            {
                var _dbContext = getContext();
                var res = await _dbContext.Message.Where(x => (x.MessageSender == user && x.MessageReceiver == vendor) || (x.MessageSender == vendor && x.MessageReceiver == user)).OrderBy(x => x.MessageTime).ToListAsync();
                foreach (var item in res)
                {
                    if (item.MessageSender == vendor)
                    {
                        item.Incoming = true;
                        item.Outgoing = false;
                    }
                    else
                    {
                        item.Incoming = false;
                        item.Outgoing = true;
                    }
                }

                return res;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public int InsertMessage(Message obj)
        {
            try
            {
                var db = getContext();
                db.Message.Add(obj);
                int cnt = db.SaveChanges();
                return cnt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }

        }

        public int delMsg()
        {
            var db = getContext();
            var res = db.Message.ToList();
            foreach (var x in res)
            {
                db.Remove(x);
            }
            int c = db.SaveChanges();
            return c;
        }
    }
}

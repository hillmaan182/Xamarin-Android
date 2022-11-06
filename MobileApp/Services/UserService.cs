using Microsoft.EntityFrameworkCore;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    public class UserService
    {
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }

        public async Task<List<User>> GetAllProducts()
        {
            var _dbContext = getContext();
            var res = await _dbContext.User.ToListAsync();
            return res;
        }
        public int? GetVendorUser(string username, string password)
        {
            var db = getContext();
            int? idVendor = 0;
            var res = db.User.Where(x => x.Username == username && x.Password == password);
            if (res.ToList().Count() > 0)
            {
                idVendor = res.First().VendorID;
            }
            return idVendor;
        }

        public int GetUserId(string username, string password)
        {
            var db = getContext();
            int id = 0;
            var res = db.User.Where(x => x.Username == username && x.Password == password);
            if (res.ToList().Count() > 0)
            {
                id = res.First().ID;
            }
            return id;
        }

        public List<User> GetDataUser(string username)
        {
            var db = getContext();
            var res = db.User.Where(x => x.Username == username).ToList();
            return res;
        }

        public List<User> GetDataUserById(int id)
        {
            var db = getContext();
            var res = db.User.Where(x => x.ID == id).ToList();
            return res;
        }

        public List<User> GetDataUserByEmail(string email)
        {
            var db = getContext();
            var res = db.User.Where(x => x.Email == email).ToList();
            return res;
        }

        public int UpdateVerified(User obj)
        {
            var db = getContext();
            var res = db.User.Update(obj);
            int c = db.SaveChanges();
            return c;
        }

        public async Task<int> LoginUser(string username, string password)
        {
            var db = getContext();
            var result = await db.User.Where(x => x.Username == username && x.Password == password).ToListAsync();

            int cnt = 0;
            if (result.Count > 0)
            {
                if (result.FirstOrDefault().IsVerified == false && result.FirstOrDefault().Role == "User")
                {
                    cnt = 2;
                }
                else
                {
                    cnt = 1;

                }
            }
            //else if (username == "vendor" && password == "vendor")
            //{
            //    cnt = 1;
            //}

            return cnt;
        }

        public int InsertUser(User obj)
        {
            var db = getContext();
            var result = db.User.Where(x => x.Username == obj.Username).Count();
            int cnt = 0;
            if (result == 0)
            {
                db.User.Add(obj);
                db.SaveChanges();
                cnt = 1;
            }
            return cnt;
        }

        public string generateCode()
        {
            string a = "";
            Random rnd = new Random();
            for (int j = 0; j < 4; j++)
            {
                a += rnd.Next(1, 9).ToString();
            }
            return a;
        }

    }
}

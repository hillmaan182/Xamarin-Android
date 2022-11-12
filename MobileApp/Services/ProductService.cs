using Microsoft.EntityFrameworkCore;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MobileApp.Services
{
    public class ProductService
    {
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }

        public async Task<List<Products>> GetProductById(int id)
        {
            var _dbContext = getContext();
            var res = await _dbContext.Product.ToListAsync();
            var result = await _dbContext.Product.Where(x => x.ID == id).ToListAsync();

            return result;
        }

        public async Task<List<Products>> GetAllProducts(string category)
        {
            var _dbContext = getContext();
            var res = await _dbContext.Product.ToListAsync();
            var result = await _dbContext.Product.Where(x => x.ProductCategory == category).ToListAsync();

            return result;
        }

        public async Task<List<Products>> GetAllProductsVendor(string category , int? vendorId)
        {
            var _dbContext = getContext();
            var res = await _dbContext.Product.ToListAsync();
            var result = await _dbContext.Product.Where(x => x.ProductCategory == category && x.VendorID == vendorId).ToListAsync();
            return result;
        }

        public async Task<int> UpdateProduct(Products obj)
        {
            var _dbContext = getContext();
            _dbContext.Product.Update(obj);
            int c = await _dbContext.SaveChangesAsync();
            return c;
        }

        public int InsertProduct(Products obj)
        {
            var db = getContext();

            var result = db.Product.Where(x => x.ProductName == obj.ProductName).Count();
            int cnt = 0;
            if (result == 0)
            {
                db.Product.Add(obj);
                int c = db.SaveChanges();
                cnt = 1;
            }
            return cnt;
        }

        public int DeleteProduct(Products obj)
        {
            var _dbContext = getContext();
            _dbContext.Product.Remove(obj);
            int c = _dbContext.SaveChanges();
            return c;
        }
    }
}

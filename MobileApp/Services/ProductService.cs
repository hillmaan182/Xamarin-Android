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

        public async Task<List<Products>> GetAllProducts(string category)
        {
            var _dbContext = getContext();
            var res = await _dbContext.Product.ToListAsync();
            var result = await _dbContext.Product.Where(x => x.ProductCategory == category).ToListAsync();
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
            var _dbContext = getContext();
            _dbContext.Product.Add(obj);
            int c = _dbContext.SaveChanges();
            return c;
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

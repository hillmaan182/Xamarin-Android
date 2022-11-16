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
            var db = getContext();
            var res = await db.Product.ToListAsync();
            var result = await db.Product.Where(x => x.ID == id).ToListAsync();

            return result;
        }

        public async Task<List<Products>> GetAllProducts(string category)
        {
            var db = getContext();
            var res = await db.Product.ToListAsync();
            var result = await db.Product.Where(x => x.ProductCategory == category).ToListAsync();

            return result;
        }

        public async Task<List<Products>> GetAllProductsVendor(string category , int? vendorId)
        {
            var db = getContext();
            var res = await db.Product.ToListAsync();
            var result = await db.Product.Where(x => x.ProductCategory == category && x.VendorID == vendorId).ToListAsync();
            return result;
        }

        public async Task<List<Products>> GetAllProductsByIdVendor(int? vendorId)
        {
            var db = getContext();
            var res = await db.Product.ToListAsync();
            var result = await db.Product.Where(x =>  x.VendorID == vendorId).ToListAsync();
            return result;
        }

        public int UpdateProduct(Products obj)
        {
            var db = getContext();
            db.Product.Update(obj);
            int c = db.SaveChanges();
            return c;
        }

        public int UpdateSeen(int id)
        {
            var db = getContext();
            var res = db.Product.Where(x => x.ID == id);
            foreach (var x in res)
            {
                Products obj= new Products();
                obj.ID = x.ID;
                obj.ProductName = x.ProductName;
                obj.ProductPrice = x.ProductPrice;
                obj.ProductSisa = x.ProductSisa;
                obj.ProductCategory = x.ProductCategory;
                obj.ProductDescription = x.ProductDescription;
                obj.ProductImage = x.ProductImage;
                obj.ProductSeen = x.ProductSeen + 1;
                db.Product.Update(obj);
                int c = db.SaveChanges();
            }
            return 1;

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
            var db = getContext();
            db.Product.Remove(obj);
            int c = db.SaveChanges();
            return c;
        }
    }
}

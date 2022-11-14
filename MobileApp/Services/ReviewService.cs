using Microsoft.EntityFrameworkCore;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MobileApp.Services
{
    public class ReviewService
    {
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }

        public int InsertReview(Review obj)
        {
            var db = getContext();
            db.Review.Add(obj);
            int cnt = db.SaveChanges();
            return cnt;
        }

        public async Task<List<Review>> GetReviews()
        {
            var db = getContext();
            var res = await db.Review.ToListAsync();

            return res;
        }

        public async Task<List<Review>> GetReviewsById(int id)
        {
            var db = getContext();
            var result = await db.Review.Where(x => x.ID == id).ToListAsync();

            return result;
        }

    }
}

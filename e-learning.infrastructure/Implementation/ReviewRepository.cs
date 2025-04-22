using e_learning.Data.Entities;
using e_learning.infrastructure.Context;
using e_learning.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.infrastructure.Implementation
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reviews>> GetCourseReviewsAsync(int courseId)
        {
            return await _context.Reviews
                .Where(r => r.CourseId == courseId)
                .Include(r => r.Student)
                .ToListAsync();
        }
        public async Task<Reviews> GetByIdAsync(int reviewId)
        {
            return await _context.Reviews.Include(r => r.Student).FirstOrDefaultAsync(r => r.Id == reviewId);
        }
        public async Task<bool> UpdateAsync(Reviews review)
        {
            var existing = await _context.Reviews.FindAsync(review.Id);
            if (existing == null)
                return false;

            existing.Rating = review.Rating;
            existing.Comment = review.Comment;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null)
                return false;

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<string> AddReviewAsync(Reviews review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return "Success";
        }
    }

}

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

        public async Task<string> AddReviewAsync(Reviews review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return "Success";
        }
    }

}

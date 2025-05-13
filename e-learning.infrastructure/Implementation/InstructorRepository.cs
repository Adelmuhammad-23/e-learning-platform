using e_learning.Data.Entities;
using e_learning.infrastructure.Context;
using e_learning.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.infrastructure.Implementation
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly ApplicationDbContext _context;

        public InstructorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Instructor>> GetAllAsync()
        {
            return await _context.instructors
                .Include(i => i.Courses)
                .ToListAsync();
        }
        public async Task AddAsync(Instructor instructor)
        {
            _context.instructors.Add(instructor);
            await _context.SaveChangesAsync();
        }
        public async Task<Instructor?> GetByIdAsync(int id)
        {
            return await _context.instructors
                .Include(i => i.Courses)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Instructor?> GetByEmailAsync(string Email)
        {
            return await _context.instructors
                .FirstOrDefaultAsync(i => i.Email == Email);
        }




        public async Task UpdateAsync(Instructor instructor)
        {
            _context.instructors.Update(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Instructor instructor)
        {
            _context.instructors.Remove(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.instructors.AnyAsync(i => i.Id == id);
        }

        public async Task<List<Instructor>> GetInstructorsIsNotApproved()
        {
            return await _context.instructors
                           .Include(i => i.Courses)
                           .Where(i => i.isApproved == false)
                           .ToListAsync();
        }
    }
}

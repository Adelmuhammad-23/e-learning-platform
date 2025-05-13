using e_learning.Data.Entities;
using e_learning.infrastructure.Context;
using e_learning.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.infrastructure.Implementation
{
    public class ModuleRepository : IModuleRepository
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructor
        public ModuleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Handel Functions
        public async Task<Module> GetByIdAsync(int id)
        {
            return await _context.Modules
                .Include(m => m.Videos)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Module>> GetByCourseIdAsync(int courseId)
        {
            var moduls = await _context.Modules
                .Where(m => m.CourseId == courseId)
                .Include(m => m.Videos)
                .Include(m => m.Quizzes).ThenInclude(m=>m.Questions)
                .ToListAsync();
            return moduls;
        }

        public async Task AddAsync(Module module)
        {
            await _context.Modules.AddAsync(module);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Module module)
        {
            _context.Modules.Update(module);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var module = await _context.Modules.FindAsync(id);
            if (module != null)
            {
                _context.Modules.Remove(module);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Modules.AnyAsync(m => m.Id == id);
        }
        #endregion
    }
}
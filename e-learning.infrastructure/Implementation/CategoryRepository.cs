using e_learning.Data.Entities;
using e_learning.infrastructure.Context;
using e_learning.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.infrastructure.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        #region Fields
        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion
        public async Task<List<Category>> GetAllCategory()
        {
            return await _context.Category.AsNoTracking().ToListAsync();
        }

        #region Functions
        public async Task<Category?> GetCategoryById(int id)
        {
            return await _context.Category.FindAsync(id);
        }

        public async Task<Category> CreateCategory(Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            _context.Category.Update(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null) return false;

            _context.Category.Remove(category);
            return await _context.SaveChangesAsync() > 0;
        }
    }


    #endregion

}


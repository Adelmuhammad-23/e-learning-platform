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



        #endregion

    }
}

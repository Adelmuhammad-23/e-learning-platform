using e_learning.Data.Entities.Identity;
using e_learning.infrastructure.Context;
using e_learning.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.infrastructure.Implementation
{
    public class UserRefreshTokenRepository : IUserRefreshTokenRepository
    {
        #region Fields
        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors
        public UserRefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion


        #region

        public IQueryable<UserRefreshToken> GetTableNoTracking()
        {
            return _context.UserRefreshToken.AsNoTracking().AsQueryable();
        }

        public async Task<UserRefreshToken> AddAsync(UserRefreshToken entity)
        {
            await _context.UserRefreshToken.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(UserRefreshToken entity)
        {
            _context.UserRefreshToken.Update(entity);
            await _context.SaveChangesAsync();
        }


        #endregion

    }
}

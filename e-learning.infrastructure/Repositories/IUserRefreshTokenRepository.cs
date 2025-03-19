using e_learning.Data.Entities.Identity;

namespace e_learning.infrastructure.Repositories
{
    public interface IUserRefreshTokenRepository
    {
        IQueryable<UserRefreshToken> GetTableNoTracking();
        Task<UserRefreshToken> AddAsync(UserRefreshToken entity);

        Task UpdateAsync(UserRefreshToken entity);

    }
}

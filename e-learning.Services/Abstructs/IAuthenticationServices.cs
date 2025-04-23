using e_learning.Data.Entities.Identity;
using e_learning.Data.Helpers;
using Microsoft.EntityFrameworkCore.Storage;

namespace e_learning.Services.Abstructs
{
    public interface IAuthenticationServices
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        public Task SaveChangesAsync();

        public Task<JwtAuthResult> GetJwtToken(User user);
        public Task<string> ConfirmEmailAsync(int userId, string code);
        public Task<string> ValidateToken(string accessToken);

        public Task<string> SendResetPasswordCodeAsync(string email);
        public Task<string> ConfirmResetPasswordAsync(string email, string code);
        public Task<string> ResetPasswordAsync(string email, string Password);
    }
}


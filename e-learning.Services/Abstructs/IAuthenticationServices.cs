using e_learning.Data.Entities.Identity;
using e_learning.Data.Helpers;

namespace e_learning.Services.Abstructs
{
    public interface IAuthenticationServices
    {
        public Task<JwtAuthResult> GetJwtToken(User user);
        public Task<string> ConfirmEmailAsync(int userId, string code);
        public Task<string> SendResetPasswordCodeAsync(string email);
        public Task<string> ConfirmResetPasswordAsync(string email, string code);
        public Task<string> ResetPasswordAsync(string email, string Password);
    }
}


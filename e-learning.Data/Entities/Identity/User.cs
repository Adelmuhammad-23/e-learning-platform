using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
namespace e_learning.Data.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public string? Code { get; set; }
        [InverseProperty(nameof(UserRefreshToken.user))]
        public virtual ICollection<UserRefreshToken> userRefreshToken { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}

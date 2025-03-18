using Microsoft.AspNetCore.Identity;
namespace e_learning.Data.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
    }
}

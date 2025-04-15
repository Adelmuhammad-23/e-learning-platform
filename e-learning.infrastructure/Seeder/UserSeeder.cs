using e_learning.Data.Entities.Identity;
using e_learning.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace e_learning.infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> userManager)
        {
            var checkAdmin = await userManager.Users.SingleOrDefaultAsync(un => un.UserName == "superAdmin");

            if (checkAdmin is null)
            {
                var adminUser = new User()
                {
                    FullName = "Super Admin",
                    UserName = "superAdmin",
                    RoleName = DefaultRoles.Admin.ToString(),
                    Email = "admin@project.com",
                    PhoneNumber = "01234567890",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var result = await userManager.CreateAsync(adminUser, "Dodd2003122!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, DefaultRoles.Admin.ToString());
                }

            }
        }
    }
}

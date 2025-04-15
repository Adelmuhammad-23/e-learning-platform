using e_learning.Data.Entities.Identity;
using e_learning.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace e_learning.infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> roleManager)
        {
            var CheckRole = await roleManager.Roles.SingleOrDefaultAsync(n => n.Name == DefaultRoles.Admin.ToString());
            if (CheckRole is null)
            {
                var adminRole = new Role { Name = DefaultRoles.Admin.ToString() };
                await roleManager.CreateAsync(adminRole);
            }
            var CheckInstructorRole = await roleManager.Roles.SingleOrDefaultAsync(n => n.Name == DefaultRoles.Instructor.ToString());
            if (CheckRole is null)
            {
                var instructorRole = new Role { Name = DefaultRoles.Instructor.ToString() };
                await roleManager.CreateAsync(instructorRole);
            }
            var ChecStudentRole = await roleManager.Roles.SingleOrDefaultAsync(n => n.Name == DefaultRoles.Student.ToString());
            if (CheckRole is null)
            {
                var studentRole = new Role { Name = DefaultRoles.Student.ToString() };
                await roleManager.CreateAsync(studentRole);
            }
        }
    }
}

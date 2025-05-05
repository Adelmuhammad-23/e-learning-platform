using e_learning.Data.Entities;
using e_learning.Data.Entities.Identity;
using e_learning.Data.Entities.Views;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_learning.infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Video> videos { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Instructor> instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<TopPricedCourses> TopPricedCourses { get; set; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }
    }
}

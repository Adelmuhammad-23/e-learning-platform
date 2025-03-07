using e_learning.Data.Entities;
using e_learning.Data.Entities.Views;
using Microsoft.EntityFrameworkCore;

namespace e_learning.infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Video> videos { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Instructor> instructors { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<TopPricedCourses> TopPricedCourses { get; set; }

    }
}

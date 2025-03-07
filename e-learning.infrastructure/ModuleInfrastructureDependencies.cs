using e_learning.Data.Entities.Views;
using e_learning.infrastructure.Implementation;
using e_learning.infrastructure.Implementation.ViewsImplementation;
using e_learning.infrastructure.Repositories;
using e_learning.infrastructure.Repositories.Views;
using Microsoft.Extensions.DependencyInjection;

namespace e_learning.infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencis(this IServiceCollection services)
        {
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ITopPricedCoursesView<TopPricedCourses>, TopPricedCoursesView>();

            return services;

        }
    }
}

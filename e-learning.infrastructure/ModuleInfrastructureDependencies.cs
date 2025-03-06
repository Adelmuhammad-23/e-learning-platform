using e_learning.infrastructure.Implementation;
using e_learning.infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace e_learning.infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencis(this IServiceCollection services)
        {
            services.AddTransient<ICourseRepository, CourseRepository>();

            return services;

        }
    }
}

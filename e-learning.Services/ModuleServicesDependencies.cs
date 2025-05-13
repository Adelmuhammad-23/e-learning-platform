using e_learning.Services.Abstructs;
using e_learning.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace e_learning.Services
{
    public static class ModuleServicesDependencies
    {

        public static IServiceCollection AddServicesDependencis(this IServiceCollection services)
        {
            services.AddTransient<ICourseServices, CourseServices>();
            services.AddTransient<ICategoryServices, CategoryServices>();
            services.AddTransient<IAdminServices, AdminServices>();
            services.AddTransient<IAuthenticationServices, AuthenticationServices>();
            services.AddTransient<IEmailServices, EmailServices>();
            services.AddTransient<IVideoServices, VideoServices>();
            services.AddTransient<IModuleService, ModuleService>();
            services.AddTransient<IInstructorService, InstructorService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IStudentServices, StudentServices>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddTransient<QuizService>();
            services.AddTransient<QuestionService>();
            services.AddTransient<ChoiceService>();



            return services;

        }
    }
}

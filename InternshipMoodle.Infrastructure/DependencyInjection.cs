using InternshipMoodle.Application.Auth;
using InternshipMoodle.Application.Chat;
using InternshipMoodle.Application.Common;
using InternshipMoodle.Application.Professors;
using InternshipMoodle.Application.Students;
using InternshipMoodle.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace InternshipMoodle.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Default")));

            services.AddScoped<IAppDbContext>(provider =>
                provider.GetRequiredService<AppDbContext>());

            services.AddScoped<AuthService>();
            services.AddScoped<StudentCourseService>();
            services.AddScoped<ProfessorCourseService>();
            services.AddScoped<ProfessorCourseManagementService>();
            services.AddScoped<ChatService>();



            return services;
        }

    }
}

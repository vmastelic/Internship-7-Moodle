using InternshipMoodle.Application.Auth;
using InternshipMoodle.Application.Professors;
using InternshipMoodle.Application.Students;
using InternshipMoodle.Infrastructure;
using InternshipMoodle.Infrastructure.Persistence;
using InternshipMoodle.Infrastructure.Persistence.Seed;
using InternshipMoodle.Presentation.Menus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

var host = Host.CreateDefaultBuilder(args)
    .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!)
    .ConfigureServices((context, services) =>
    {
        services.AddInfrastructure(context.Configuration);
    })
    .Build();

using var scope = host.Services.CreateScope();

var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await DatabaseSeeder.SeedAsync(context);

var authMenu = new AuthMenu(
    scope.ServiceProvider.GetRequiredService<AuthService>()
);

await authMenu.ShowAsync();

var mainMenu = new MainMenu(
    scope.ServiceProvider.GetRequiredService<StudentCourseService>(),
    scope.ServiceProvider.GetRequiredService<ProfessorCourseService>()
);


await mainMenu.ShowAsync();


using InternshipMoodle.Application.Auth;
using InternshipMoodle.Infrastructure;
using InternshipMoodle.Infrastructure.Persistence;
using InternshipMoodle.Infrastructure.Persistence.Seed;
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

Console.WriteLine("Application started successfully.");

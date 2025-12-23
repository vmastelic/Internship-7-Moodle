using InternshipMoodle.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("appsettings.json", optional: false);
        config.AddJsonFile("appsettings.Development.json", optional: true);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddInfrastructure(context.Configuration);
    })
    .Build();

Console.WriteLine("Infrastructure and DbContext configured.");

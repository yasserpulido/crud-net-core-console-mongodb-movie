using Core.Interfaces;
using Core.Services;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;

namespace App
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application Starting");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.Configure<MovieDatabaseSettings>(context.Configuration.GetSection(nameof(MovieDatabaseSettings)));
                    services.AddSingleton<IMovieDatabaseSettings>(sp => sp.GetRequiredService<IOptions<MovieDatabaseSettings>>().Value);
                    services.AddScoped<MovieService>();
                    services.AddScoped<IMovie, MovieRepository>();
                }).UseSerilog()
                .Build();

            var svc = ActivatorUtilities.CreateInstance<UI>(host.Services);
            svc.Menu();
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
        }
    }
}
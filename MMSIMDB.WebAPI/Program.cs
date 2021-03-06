using Microsoft.AspNetCore.Identity;
using MMSIMDB.Application.Interfaces.Repositories;
using MMSIMDB.Identity.Models;
using Serilog;

namespace MMSIMDB.WebAPI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            //Read Configuration from appSettings
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            //Initialize Logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    await Identity.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
                    await Identity.Seeds.DefaultAdmin.SeedAsync(userManager, roleManager);
                    await Identity.Seeds.DefaultBasicUser.SeedAsync(userManager, roleManager);

                    
                    await MMSIMDB.Persistence.Seeds.MovieTypeSeed.SeedAsync(services.GetRequiredService<IMovieTypeRepositoryAsync>());
                    await MMSIMDB.Persistence.Seeds.MovieSeed.SeedAsync(services.GetRequiredService<IMovieRepositoryAsync>());
                    await MMSIMDB.Persistence.Seeds.TVSeriesSeed.SeedAsync(services.GetRequiredService<IMovieRepositoryAsync>());
                    Log.Information("Finished Seeding Default Data");
                    Log.Information("Application Starting");
                }
                catch (Exception ex)
                {
                    Log.Warning(ex, "An error occurred seeding the DB");
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }
            host.Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog() //Uses Serilog instead of default .NET Logger
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}


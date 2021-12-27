using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MMSIMDB.Application.Interfaces;
using MMSIMDB.Persistence.Contexts;
using MMSIMDB.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMSIMDB.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<MMSIMDBDBContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<MMSIMDBDBContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(MMSIMDBDBContext).Assembly.FullName)));
            }
            #region Repositories

            Type[] list = typeof(MovieRepositoryAsync).Assembly.GetTypes()
                      .Where(el => el.Namespace == typeof(MovieRepositoryAsync).Namespace && !el.FullName.Contains("GenericRepositoryAsync") && el.IsClass && !el.IsAbstract)
                      .ToArray();
            foreach (var type in list)
            {
                var iType = type.GetInterfaces().FirstOrDefault(el => !el.IsGenericType);
                if (iType != null)
                {
                    services.AddTransient(iType, type);
                }
            }
            #endregion
        }
    }
}

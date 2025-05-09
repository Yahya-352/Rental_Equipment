using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using myproject.services;
using myproject_Library.Model;

namespace myproject.configs
{
    internal class ServiceConfigurator
    {

        private static IServiceProvider _serviceProvider;

        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Add DbContext
            services.AddDbContext<EquipmentDBContext>(options =>

            //Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=EquipmentsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False


            //Data Source=STS_LAPTOP_02\\MSSQLSERVER02;Initial Catalog=EquipmentDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False
                options.UseSqlServer(@"Data Source=STS_LAPTOP_02\\MSSQLSERVER02;Initial Catalog=EquipmentDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"));

            // Add Identity services
            services.AddIdentity<User, Role>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;

                // User settings
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<EquipmentDBContext>()
            .AddDefaultTokenProviders();

            // Required services
            services.AddLogging();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add custom services
            services.AddScoped<AuthService>();

            // Build the service provider
            _serviceProvider = services.BuildServiceProvider();
            return _serviceProvider;
        }

        public static IServiceProvider GetServiceProvider()
        {
            if (_serviceProvider == null)
            {
                _serviceProvider = ConfigureServices();
            }
            return _serviceProvider;
        }

        public static T GetService<T>() where T : class
        {
            return GetServiceProvider().GetService<T>();
        }
    }
}

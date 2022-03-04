using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PerfumeManufacturerProject.Business.Interfaces.Mapping;
using PerfumeManufacturerProject.Business.Interfaces.Services;
using PerfumeManufacturerProject.Business.Services;
using PerfumeManufacturerProject.Data.EF;
using PerfumeManufacturerProject.Data.Interfaces.Models;
using Serilog;
using System.Net;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.DependencyConfiguration
{
    public static class DependencyConfig
    {
        public static void Register(IServiceCollection services, IConfiguration config)
        {
            AddApplicationExtensions(services);
            AddDataExtensions(services, config);

            AddLoggerConfiguration(services, config);
            AddSwaggerConfiguration(services);

            services.AddAutoMapper(typeof(MappingProfile));
        }

        public static void AddApplicationExtensions(IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IRolesService, RolesService>();
        }

        public static void AddDataExtensions(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "PerfumeAppCookie";
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {
                        ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        return Task.CompletedTask;
                    }
                };
            });
        }

        private static void AddLoggerConfiguration(this IServiceCollection services, IConfiguration config)
        {
            var logger = new LoggerConfiguration().ReadFrom
                .Configuration(config)
                .CreateLogger();

            services.AddLogging(p =>
            {
                p.ClearProviders();
                p.AddSerilog(logger);
            });
        }

        public static void AddSwaggerConfiguration(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PerfumeManufacturerProject", Version = "v1" });
                c.DescribeAllParametersInCamelCase();
            });
        }
    }
}

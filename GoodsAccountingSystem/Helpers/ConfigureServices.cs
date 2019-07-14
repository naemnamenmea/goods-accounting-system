using GoodsAccountingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsAccountingSystem.Helpers
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("HomeConnection");
            services.AddDbContext<DataContext>(o => o.UseMySql(connectionString));
            return services;
        }

        public static IdentityBuilder ConfigureIdentity(this IServiceCollection services)
        {
            return services.AddIdentity<UserModel, IdentityRole>(o => {
                o.Password.RequiredLength = 5;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireDigit = false;
            });
        }
    }
}

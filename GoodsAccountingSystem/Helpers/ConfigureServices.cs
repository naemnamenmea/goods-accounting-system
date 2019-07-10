using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsAccountingSystem.Helpers
{
    public static class ConfigureServices
    {
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("HomeConnection");
            services.AddDbContext<DataContext>(o => o.UseMySql(connectionString));
        }
    }
}

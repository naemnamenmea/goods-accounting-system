using GoodsAccountingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoodsAccountingSystem
{
    public class DataContext : 
        IdentityDbContext<UserModel>
    {
        public DbSet<GoodModel> Goods { get; set; }
        public DbSet<UserModel> UserModel { get; set; }

        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        public DataContext(string connectionString)
            : base(GetOptions(connectionString))
        {

        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return MySqlDbContextOptionsExtensions.UseMySql(new DbContextOptionsBuilder(), connectionString).Options;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

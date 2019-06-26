using GoodsAccountingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsAccountingSystem
{
    public class DataContext : IdentityDbContext<UserModel, IdentityRole<int>, int>
    {
        public DbSet<GoodModel> Goods { get; set; }

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

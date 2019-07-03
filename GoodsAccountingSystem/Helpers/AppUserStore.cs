using GoodsAccountingSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsAccountingSystem.Helpers
{
    public interface IAppUserStore : IUserStore<AppUser, int>
    {

    }

    public class AppUserStore :
        UserStore<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>,
        IAppUserStore
    {
        public AppUserStore() : base(new AppDbContext())
        {

        }

        public AppUserStore(AppDbContext context) : base(context)
        {

        }
    }
}

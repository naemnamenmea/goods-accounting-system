using GoodsAccountingSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsAccountingSystem.Helpers
{
    public class AppUserManager : UserManager<AppUser, int>
    {
        public AppUserManager(IAppUserStore store) : base(store)
        {

        }
    }
}

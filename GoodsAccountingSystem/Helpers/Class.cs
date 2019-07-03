using GoodsAccountingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GoodsAccountingSystem.Helpers
{
    public abstract class BaseController : Controller
    {
        public AppClaimsPrincipal CurrentUser
        {
            get { return new AppClaimsPrincipal((ClaimsPrincipal)this.User); }
        }


        public BaseController()
        {

        }
    }
}

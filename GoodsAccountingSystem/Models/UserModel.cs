using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static GoodsAccountingSystem.Models.AppUser;

namespace GoodsAccountingSystem.Models
{
    public class AppUser :
        IdentityUser<int, AppUserLogin, AppUserRole, AppUserClaim>,
        IUser<int>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool Activity { get; set; } = true;
    }

    public class AppUserRole : IdentityUserRole<int> { }

    public class AppUserClaim : IdentityUserClaim<int> { }

    public class AppUserLogin : IdentityUserLogin<int> { }

    public class AppRole : IdentityRole<int, AppUserRole> { }

    public class AppClaimsPrincipal : ClaimsPrincipal
    {
        public AppClaimsPrincipal(ClaimsPrincipal principal) : base(principal)
        { }

        public int UserId
        {
            get { return int.Parse(this.FindFirst(ClaimTypes.Sid).Value); }
        }
    }   
}

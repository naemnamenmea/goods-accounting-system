using GoodsAccountingSystem.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsAccountingSystem.Helpers
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@admin.com";
            string password = "tesT321!";
            if (await roleManager.FindByNameAsync(RoleCategory.Admin) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(RoleCategory.Admin));
            }
            if (await roleManager.FindByNameAsync(RoleCategory.Anonymous) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(RoleCategory.Anonymous));
            }
            if (await roleManager.FindByNameAsync(RoleCategory.User) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(RoleCategory.User));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User()
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FirstName = "admin",
                    SecondName = "admin",
                    MiddleName = "admin",
                    RegisterDate = DateTime.Now,
                    BirthDate = new DateTime()
                };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, RoleCategory.Admin);
                }
            }
        }
    }
}

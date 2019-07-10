using GoodsAccountingSystem.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace GoodsAccountingSystem.Helpers
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@admin.com";
            string password = "tesT321!";
            if (await roleManager.FindByNameAsync(RoleCategory.Admin) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(RoleCategory.Admin));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                UserModel admin = new UserModel()
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

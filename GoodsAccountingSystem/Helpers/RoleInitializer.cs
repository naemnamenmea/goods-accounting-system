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
            if (await roleManager.FindByNameAsync(Role.ADMIN) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Role.ADMIN));
            }
            if (await roleManager.FindByNameAsync(Role.USER) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Role.USER));
            }

            string adminEmail = "admin@admin.com";
            string password = "tesT321!";
            UserModel presentAdmin = await userManager.FindByNameAsync(adminEmail);
            if (presentAdmin == null)
            {
                UserModel newAdmin = new UserModel()
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FirstName = "admin",
                    SecondName = "admin",
                    MiddleName = "admin",
                    RegisterDate = DateTime.Now,
                    BirthDate = new DateTime()
                };
                IdentityResult result = await userManager.CreateAsync(newAdmin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, Role.ADMIN);
                }
            }
            else
            {
                if (!(await userManager.IsInRoleAsync(presentAdmin, Role.ADMIN)))
                {
                    IdentityResult res = await userManager.AddToRoleAsync(presentAdmin, Role.ADMIN);
                    if(res.Succeeded)
                    {

                    } else
                    {

                    }
                }
            }
        }
    }
}

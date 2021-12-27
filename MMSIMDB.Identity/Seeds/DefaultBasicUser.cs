using Microsoft.AspNetCore.Identity;
using MMSIMDB.Application.Enums;
using MMSIMDB.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSIMDB.Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var user = new ApplicationUser
            {
                UserName = "user",
                Email = "user@testAcountMMSIMDB.com",
                FirstName = "Predrag",
                LastName = "Pesic",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != user.Id))
            {
                var userItem = await userManager.FindByEmailAsync(user.Email);
                if (userItem == null)
                {
                    await userManager.CreateAsync(user, "Test123!");
                    await userManager.AddToRoleAsync(user, Roles.User.ToString());
                }

            }
        }
    }
}

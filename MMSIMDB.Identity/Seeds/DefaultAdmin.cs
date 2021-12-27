using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MMSIMDB.Application.Enums;
using MMSIMDB.Identity.Models;
using Org.BouncyCastle.Crypto.Prng.Drbg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSIMDB.Identity.Seeds
{
    public static class DefaultAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "p.pesic85@gmail.com",
                FirstName = "Predrag",
                LastName = "Pesic",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != adminUser.Id))
            {
                var user = await userManager.FindByEmailAsync(adminUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(adminUser, "Test123!");
                    await userManager.AddToRoleAsync(adminUser, Roles.Admin.ToString());
                }

            }
        }
    }
}

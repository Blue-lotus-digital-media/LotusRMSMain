using LotusRMS.Models;
using Microsoft.AspNetCore.Identity;

namespace LotusRMS.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndSuperAdminAsync(IServiceProvider service)
        {
            //Seed Roles
            var userManager = service.GetService<UserManager<RMSUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Constants.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Constants.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Constants.Roles.Cashier.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Constants.Roles.Waiter.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Constants.Roles.Kitchen.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Constants.Roles.User.ToString()));

            //seed superadmin
            var user = new RMSUser()
            {
                UserName= "forwebsite.fw@gmail.com",
                Email="forwebsite.fw@gmail.com",
                EmailConfirmed=true,
                PhoneNumberConfirmed=true
            };
            user.Update("Blue Lotus", "Digital", "Media", "9844662120");

            var userInDb=await userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                await userManager.CreateAsync(user,"Lotus@123");
                await userManager.AddToRoleAsync(user, Constants.Roles.SuperAdmin.ToString());
            }

        }
        }
}

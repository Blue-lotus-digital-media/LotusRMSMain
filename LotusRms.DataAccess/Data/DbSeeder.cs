using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Models.Viewmodels.MenuUnit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;

namespace LotusRMS.DataAccess
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
            await roleManager.CreateAsync(new IdentityRole(Constants.Roles.Bar.ToString()));

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
                await userManager.AddToRoleAsync(user, Constants.Roles.Admin.ToString());
                await userManager.AddToRoleAsync(user, Constants.Roles.Cashier.ToString());
                await userManager.AddToRoleAsync(user, Constants.Roles.Waiter.ToString());
                await userManager.AddToRoleAsync(user, Constants.Roles.User.ToString());
                await userManager.AddToRoleAsync(user, Constants.Roles.Kitchen.ToString());
                await userManager.AddToRoleAsync(user, Constants.Roles.Bar.ToString());
            }

        }
        public static async Task SeedMenuUnit(IServiceProvider service)
        {
            using (StreamReader reader = new StreamReader(@"wwwroot/files/menuunit.json"))
            {
                var json = reader.ReadToEnd();
                var menuUnitJson = JsonConvert.DeserializeObject<List<MenuUnitFromJson>>(json);
                var menuUnits = new List<LotusRMS_Menu_Unit>();
                foreach (var item in menuUnitJson)
                {

                    var menuUnit = new LotusRMS_Menu_Unit(unit_Name: item.Name, unit_Symbol: item.Symbol, unit_Description: item.Description)
                    {
                        UnitDivision = new List<LotusRMS_Unit_Division>()
                    };
                    foreach (var abc in item.Division)
                    {
                        var div = new LotusRMS_Unit_Division()
                        {
                            Title = abc.Title,
                            Value = abc.Value
                        };
                        menuUnit.UnitDivision.Add(div);
                    }

                    menuUnits.Add(menuUnit);
                }
                using (var serviceScope = service.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                    if (!context.LotusRMS_Menu_Units.Any())
                    {
                        context.AddRange(menuUnits);
                        context.SaveChanges();
                    }
                }
            }
        } 
    
    
    }
}

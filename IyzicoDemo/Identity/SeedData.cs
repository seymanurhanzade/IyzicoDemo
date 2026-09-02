
using IyzicoDemo.Entity;
using Microsoft.AspNetCore.Identity;

namespace IyzicoDemo.Identity
{
    public static class SeedData
    {
        //public static async Task SeedRoleAsync(IServiceProvider serviceProvider)
        //{
        //    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //    if(!await roleManager.RoleExistsAsync("Admin"))
        //    {
        //        await roleManager.CreateAsync(new IdentityRole("Admin"));
        //    }
        //    if(!await roleManager.RoleExistsAsync("User"))
        //    {
        //        await roleManager.CreateAsync(new IdentityRole("User"));
        //    }

            
        //}


        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("Member"))
            {
                await roleManager.CreateAsync(new IdentityRole("Member"));
            }
            if (!await roleManager.RoleExistsAsync("Guest"))
            {
                await roleManager.CreateAsync(new IdentityRole("Guest"));
            }

            var adminUser = await userManager.FindByNameAsync("admin");

            if (adminUser == null)
            {
                var newAdminUser = new AppUser
                {
                    FullName = "Admin User",
                    UserName = "admin",
                    Email = "admin@example.com"

                };
                var result = await userManager.CreateAsync(newAdminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdminUser, "Admin");
                }
            }
        }

    }
}

using Microsoft.AspNetCore.Identity;
using ERPInventoryPurchesSystems.Models.Master;

namespace ERPInventoryPurchesSystems.Utility
{
    public static class DbInitializer
    {
        public static async Task SeedRolesAndAdminUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminRole = "Admin";
            string userRole = "User";

            string adminEmail = "admin@erp.com";
            string adminPassword = "Admin@123";

            string userEmail = "mandar@erp.com";
            string userPassword = "Mandar@123";

            if (!await roleManager.RoleExistsAsync(adminRole))
                await roleManager.CreateAsync(new IdentityRole(adminRole));

            if (!await roleManager.RoleExistsAsync(userRole))
                await roleManager.CreateAsync(new IdentityRole(userRole));

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "System Admin",
                    DepartmentCode = "DEP001", 
                    UserRole = adminRole,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, adminPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, adminRole);
            }


            var regularUser = await userManager.FindByEmailAsync(userEmail);
            if (regularUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    FullName = "Mandar Wagale",
                    DepartmentCode = "DEP001", 
                    UserRole = userRole,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, userPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, userRole);
            }
        }
    }
}

using Microsoft.AspNetCore.Identity;
using RopeyDVDManagementSystem.Models.Identity;

namespace RopeyDVDManagementSystem.Data
{
    public class ApplicaitonDbInitializer
    {

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                // Seeding roles to the initial database
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));


                // Seeding users to the initial database
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                
                string adminUserEmail = "admin@ropey.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new IdentityUser()
                    {
                        UserName = "Admin-User",
                        Email = adminUserEmail,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin!123");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@ropey.com";
                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new IdentityUser()
                    {
                        UserName = "App-User",
                        Email = appUserEmail,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "User!123");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

            }
        }

    }
}

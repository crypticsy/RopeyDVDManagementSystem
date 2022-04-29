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

                if (!await roleManager.RoleExistsAsync(UserRoles.Manager))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));

                if (!await roleManager.RoleExistsAsync(UserRoles.Assistant))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Assistant));


                // Seeding users to the initial database
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                
                string adminUserEmail = "admin@ropey.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FirstName="Ropey",
                        LastName="Admin",
                        UserName = "admin",
                        Email = adminUserEmail,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(newAdminUser, "Admin!123");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Manager);
                }

                string appUserEmail = "user@ropey.com";
                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FirstName = "Ropey",
                        LastName = "User",
                        UserName = "customer",
                        Email = appUserEmail,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(newAppUser, "User!123");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Assistant);
                }

            }
        }

    }
}

using Common;
using DataAccess.Data;
using HiddenVilla.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HiddenVilla.Service
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DbInitializer(AppDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    dbContext.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
            }

            // Create roles if not exist
            if (!dbContext.Roles.Any(r => r.Name == Roles.Role_Admin))
            {
                roleManager.CreateAsync(new IdentityRole(Roles.Role_Admin)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(Roles.Role_Employee)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(Roles.Role_Customer)).GetAwaiter().GetResult();
            }

            // Create Admin user if not exist
            if (!dbContext.Users.Any(u => u.Email == "admin@gmail.com"))
            {
                var result = userManager.CreateAsync(new IdentityUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true
                }, "Admin@123").GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create Admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            // Assign Admin role to the user
            IdentityUser user = dbContext.Users.FirstOrDefault(x => x.Email == "admin@gmail.com");
            if (user == null)
            {
                throw new Exception("Admin user not found in the database after creation.");
            }

            userManager.AddToRoleAsync(user, Roles.Role_Admin).GetAwaiter().GetResult();
        }
    }
}

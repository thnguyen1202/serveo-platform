using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serveo.Domain.Entities.Identity;

namespace Serveo.Infrastructure.Persistence.EntityFramework.Seed
{
    public static class SeedDataDefault
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            #region Role
            if (!context.Roles.IgnoreQueryFilters().Any())
            {
                // Create default system role
                var systemRole = new Role()
                {
                    Name = "Super Admin",
                    IsStatic = true,
                    IsDefault = true
                };
                systemRole.SetNormalizedNames();

                context.Roles.Add(systemRole);
                context.SaveChanges();
            }
            #endregion

            #region User
            if (!context.Users.IgnoreQueryFilters().Any())
            {
                // Create default admin user
                var hasher = new PasswordHasher<User>();
                var adminUser = new User
                {
                    UserName = "Administrator",
                    Email = "administrator@ebizity.com",
                    DisplayName = "Administrator",
                    TimeZoneId = "Singapore Standard Time",
                    EmailConfirmed = true
                };
                adminUser.SetNormalizedNames();
                adminUser.SetSecurityStamp();
                adminUser.PasswordHash = hasher.HashPassword(adminUser, "Qwe@123");

                context.Users.Add(adminUser);
                context.SaveChanges();

                // Assign default admin user to system role
                var systemRole = context.Roles.IgnoreQueryFilters().FirstOrDefault(x => x.Name == "Super Admin");
                if (systemRole != null)
                {
                    context.UserRoles.Add(new UserRole
                    {
                        RoleId = systemRole.Id,
                        UserId = adminUser.Id,
                    });
                    context.SaveChanges();
                }
            }
            #endregion
        }
    }
}

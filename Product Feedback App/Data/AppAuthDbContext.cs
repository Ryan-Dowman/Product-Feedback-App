using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Product_Feedback_App.Models.Identity;

namespace Product_Feedback_App.Data
{
    public class AppAuthDbContext : IdentityDbContext<AppUser>
    {
        public AppAuthDbContext(DbContextOptions<AppAuthDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings
                .Ignore(RelationalEventId.PendingModelChangesWarning));  // Suppress the warning
        }

        protected async override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed Role Data
            string adminRoleId = "4244f42f-63f0-40fe-b5b1-7d2d84cd58a7";
            string userRoleId = "921ac26b-2721-4c1d-9005-ba5f98e3925d";

            List<IdentityRole> identityRoles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(identityRoles);

            // Seed Admin User Data
            string adminUserId = "588ba132-f018-4b61-bce9-4977098b1146";

            Env.Load();

            AppUser adminUser = new AppUser
            {
                UserName = Environment.GetEnvironmentVariable("ADMINUSERNAME"),
                NormalizedUserName = Environment.GetEnvironmentVariable("ADMINUSERNAME").ToUpper(),
                Email = Environment.GetEnvironmentVariable("ADMINEMAIL"),
                NormalizedEmail = Environment.GetEnvironmentVariable("ADMINEMAIL").ToUpper(),
                Id = adminUserId,
                ProfilePictureUrl = "7a95e6c4-31da-4c14-8760-f5de479f1698.png"
            };

            adminUser.PasswordHash = new PasswordHasher<AppUser>().HashPassword(adminUser, Environment.GetEnvironmentVariable("ADMINPASSWORD"));

            // Seed Admin User Role Data
            List<IdentityUserRole<string>> adminUserRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                },

                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = adminUserId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminUserRoles);
        }
    }
}

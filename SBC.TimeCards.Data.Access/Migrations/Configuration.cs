namespace SBC.TimeCards.Data.Access.Migrations
{
    using Microsoft.AspNet.Identity;
    using SBC.TimeCards.Common;
    using SBC.TimeCards.Data.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SBC.TimeCards.Data.EF.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SBC.TimeCards.Data.EF.ApplicationContext context)
        {
            if (!context.Roles.Any())
            {
                //Step 1 Create the roles.
                var adminRole = new Role { Name = AppRoles.Administrator };
                var userRole = new Role { Name = AppRoles.User };
                context.Roles.Add(adminRole);
                context.Roles.Add(userRole);
                context.SaveChanges();
                
                //Step 2 Create the admin user.
                var passwordHasher = new PasswordHasher();
                var user = new User();
                user.UserName = "admin";
                user.Email = "admin@sbc.com";
                user.PasswordHash = passwordHasher.HashPassword("Admin12345");
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.CreateDate = GlobalSettings.CURRENT_DATETIME;
                context.Users.Add(user);
                context.SaveChanges();

                //Step 3 Create a role for a user
                var userrole = new UserRole();
                userrole.RoleId = adminRole.Id;
                userrole.UserId = user.Id;
                context.UserRoles.Add(userrole);
                context.SaveChanges();
            }
        }
    }
}

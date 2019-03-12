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
                var adminRoleId = SeedRoles(context);

                //Step 2 Create the admin user.
                var passwordHasher = new PasswordHasher();
                var user = new User();
                user.UserName = "admin";
                user.Name = "Administrator";
                user.Email = "admin@sbc.com";
                user.PasswordHash = passwordHasher.HashPassword("Admin12345");
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.CreateDate = GlobalSettings.CURRENT_DATETIME;
                context.Users.Add(user);
                context.SaveChanges();

                //Step 3 Create a role for a user
                var userrole = new UserRole();
                userrole.RoleId = adminRoleId;
                userrole.UserId = user.Id;
                context.UserRoles.Add(userrole);
                context.SaveChanges();
            }

            if (!context.TicketStates.Any())
            {
                SeedTicketStates(context);
            }
        }

        /// <summary>
        /// Seeds the available roles by the app.
        /// </summary>
        /// <param name="context"> ApplicationDbContest responsible for adding the roles </param>
        /// <returns>the administrator role id</returns>
        private int SeedRoles(SBC.TimeCards.Data.EF.ApplicationContext context)
        {
            var adminRole = new Role {Name = AppRoles.Administrator};
            context.Roles.Add(adminRole);
            context.Roles.Add(new Role {Name = AppRoles.Manager});
            context.Roles.Add(new Role {Name = AppRoles.Network});
            context.Roles.Add(new Role {Name = AppRoles.System});
            context.Roles.Add(new Role {Name = AppRoles.DataCenter});
            context.SaveChanges();
            return adminRole.Id;
        }

        private void SeedTicketStates(SBC.TimeCards.Data.EF.ApplicationContext context)
        {
            context.TicketStates.Add(new TicketState() {Id = 1, Name = "Active"});
            context.TicketStates.Add(new TicketState() {Id = 2, Name = "Done"});
            context.TicketStates.Add(new TicketState() {Id = 3, Name = "Delayed"});
        }
    }
}

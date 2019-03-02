﻿using System;
using System.Data.Entity;
using SBC.TimeCards.Data.Configuration;
using SBC.TimeCards.Data.Models;

namespace SBC.TimeCards.Data.EF
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection") { }

        private static void Magic()
        {
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
            if (type == null)
                throw new Exception("Do not remove, ensures static reference to System.Data.Entity.SqlServer");
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new UserClaimConfiguration());
            modelBuilder.Configurations.Add(new UserLoginConfiguration());
            modelBuilder.Configurations.Add(new ProjectConfiguration());
            modelBuilder.Configurations.Add(new AttachmentConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}

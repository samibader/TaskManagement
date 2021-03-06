﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using SBC.TimeCards.Data.Models;

namespace SBC.TimeCards.Data.Configuration
{
    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");
            Property(c => c.Email)
                .HasMaxLength(256);
            Property(c => c.UserName)
                .HasMaxLength(256)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UserNameIndex") { IsUnique = true }));
            Property(c => c.Name)
                .HasMaxLength(256)
                .IsRequired();
            HasMany(x => x.Projects)
                .WithRequired(p => p.User)
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);
            HasMany(u => u.FavoriteProjects)
                .WithMany(p => p.UserFavorites)
                .Map(x => {
                    x.ToTable("Favorites");
                    x.MapLeftKey("UserId");
                    x.MapRightKey("ProjectId");
                    }

                );
            HasMany(x => x.TicketsAssigned)
                .WithOptional(t => t.Assignee)
                .HasForeignKey(x => x.AssigneeId)
                .WillCascadeOnDelete(false);
            
            HasMany(x => x.TicketsCreated)
                .WithRequired(t => t.Owner)
                .HasForeignKey(t => t.OwnerId)
                .WillCascadeOnDelete(false);
        }
    }
}

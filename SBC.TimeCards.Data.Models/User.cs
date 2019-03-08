using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace SBC.TimeCards.Data.Models
{
    public class User : IUser<int>
    {
        public User()
        {
            Projects = new List<Project>();
            UserRoles = new List<UserRole>();
            TicketsAssigned = new List<Ticket>();
            TicketsCreated = new List<Ticket>();
        }

        public int Id { get; set; }

        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual int AccessFailedCount { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual IList<Project> Projects { get; set; }
        public virtual IList<UserRole> UserRoles { get; set; }
        public virtual IList<Project> FavoriteProjects { get; set; }
        public virtual IList<Ticket> TicketsAssigned { get; set; }
        public virtual IList<Ticket> TicketsCreated { get; set; }

    }
}

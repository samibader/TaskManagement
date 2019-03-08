using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using SBC.TimeCards.Data.Models;

namespace SBC.TimeCards.Data.Configuration
{
    internal class TicketConfiguration : EntityTypeConfiguration<Ticket>
    {
        public TicketConfiguration()
        {
            ToTable("Tickets");
            Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Title)
                .HasMaxLength(256)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("TicketTitleIndex")));

            HasRequired(p => p.Project)
                .WithMany(a => a.Tickets)
                .HasForeignKey(p => p.ProjectId);

            HasOptional(x => x.Parent)
                .WithMany(x => x.SubTickets)
                .HasForeignKey(x => x.ParentTicketId);
            HasOptional(x => x.Assignee)
                .WithMany(u => u.TicketsAssigned)
                .HasForeignKey(x => x.AssigneeId)
                .WillCascadeOnDelete(false);
            HasRequired(x => x.Owner)
                .WithMany(u => u.TicketsCreated)
                .HasForeignKey(x => x.OwnerId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.TicketState)
                .WithMany(s => s.Tickets)
                .HasForeignKey(x => x.StateId)
                .WillCascadeOnDelete(false);


        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using SBC.TimeCards.Data.Models;

namespace SBC.TimeCards.Data.Configuration
{
    internal class TicketStateConfiguration : EntityTypeConfiguration<TicketState>
    {
        public TicketStateConfiguration()
        {
            ToTable("TicketStates");
            Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Name)
                .HasMaxLength(512)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("TicketStateTitleIndex")));

            HasMany(x => x.Tickets)
                 .WithRequired(t => t.TicketState)
             .HasForeignKey(t => t.StateId);
        }
    }
}

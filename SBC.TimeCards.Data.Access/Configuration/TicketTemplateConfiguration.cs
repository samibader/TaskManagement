using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using SBC.TimeCards.Data.Models;

namespace SBC.TimeCards.Data.Configuration
{
    internal class TicketTemplateConfiguration : EntityTypeConfiguration<TicketTemplate>
    {
        public TicketTemplateConfiguration()
        {
            ToTable("TicketTemplates");
            Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(x => x.Ticket)
                 .WithMany(y => y.TicketTemplates);
            HasRequired(x => x.TemplateType)
                .WithMany(y => y.TicketTemplates);
            HasOptional(x => x.DeviceTemplate)
                 .WithRequired(x => x.TicketTemplate);
            HasOptional(x => x.NetworkTemplate)
                 .WithRequired(x => x.TicketTemplate);
            HasOptional(x => x.ServerTemplate)
                  .WithRequired(x => x.TicketTemplate);
            HasOptional(x => x.UserTemplate)
                  .WithRequired(x => x.TicketTemplate);
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using SBC.TimeCards.Data.Models;

namespace SBC.TimeCards.Data.Configuration
{
    internal class ServerNetworkTemplateConfiguration : EntityTypeConfiguration<ServerNetworkTemplate>
    {
        public ServerNetworkTemplateConfiguration()
        {
            ToTable("ServerNetworkTemplates");
            Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(p => p.ServerTemplate)
                .WithMany(y => y.ServerNetworkTemplates);
        }
    }
}

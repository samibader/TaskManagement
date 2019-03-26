using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using SBC.TimeCards.Data.Models;

namespace SBC.TimeCards.Data.Configuration
{
    internal class ServerDiskTemplateConfiguration : EntityTypeConfiguration<ServerDiskTemplate>
    {
        public ServerDiskTemplateConfiguration()
        {
            ToTable("ServerDiskTemplates");
            Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(p => p.ServerTemplate)
                .WithMany(a => a.ServerDiskTemplates);
        }
    }
}

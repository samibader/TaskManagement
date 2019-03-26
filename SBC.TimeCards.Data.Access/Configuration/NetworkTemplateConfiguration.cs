using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using SBC.TimeCards.Data.Models;

namespace SBC.TimeCards.Data.Configuration
{
    internal class NetworkTemplateConfiguration : EntityTypeConfiguration<NetworkTemplate>
    {
        public NetworkTemplateConfiguration()
        {
            ToTable("NetworkTemplates");
            HasKey(x => x.Id);
           
        }
    }
}

﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using SBC.TimeCards.Data.Models;

namespace SBC.TimeCards.Data.Configuration
{
    internal class ServerTemplateConfiguration : EntityTypeConfiguration<ServerTemplate>
    {
        public ServerTemplateConfiguration()
        {
            ToTable("ServerTemplates");
            HasKey(x => x.Id);
            HasMany(x => x.ServerNetworkTemplates)
                .WithRequired(x => x.ServerTemplate);
            HasMany(x => x.ServerDiskTemplates)
                 .WithRequired(x => x.ServerTemplate);
        }
    }
}

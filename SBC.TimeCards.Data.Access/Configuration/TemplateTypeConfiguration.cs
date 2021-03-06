﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using SBC.TimeCards.Data.Models;

namespace SBC.TimeCards.Data.Configuration
{
    internal class TemplateTypeConfiguration : EntityTypeConfiguration<TemplateType>
    {
        public TemplateTypeConfiguration()
        {
            ToTable("TemplateType");
            Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Name)
                .HasMaxLength(512)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AttachmentTitleIndex")));

            HasMany(x => x.TicketTemplates)
                .WithRequired(y => y.TemplateType)
                .HasForeignKey(x => x.TemplateTypeId);
        }
    }
}

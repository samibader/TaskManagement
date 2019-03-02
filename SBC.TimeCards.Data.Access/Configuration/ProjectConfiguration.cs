using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using SBC.TimeCards.Data.Models;

namespace SBC.TimeCards.Data.Configuration
{
    internal class ProjectConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectConfiguration()
        {
            ToTable("Projects");
            Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Name)
                .HasMaxLength(256)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ProjectNameIndex") { IsUnique = true }));

            HasRequired(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId);

            HasMany(p => p.Attachments)
                .WithRequired(a => a.Project)
                .HasForeignKey(a => a.ProjectId);

            Property(p => p.ArchiveDate)
                .IsOptional();
        }
    }
}

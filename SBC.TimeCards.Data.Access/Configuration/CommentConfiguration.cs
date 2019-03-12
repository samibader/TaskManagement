using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using SBC.TimeCards.Data.Models;

namespace SBC.TimeCards.Data.Configuration
{
    internal class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            ToTable("Comments");
            Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Text)
                .IsRequired();
               // .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("CommentTextIndex")));

            HasRequired(p => p.Ticket)
                .WithMany(a => a.Comments)
                .HasForeignKey(p => p.TicketId);
            HasRequired(x => x.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(x => x.UserId);
        }
    }
}

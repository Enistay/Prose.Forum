using Prose.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Prose.Infrastructure.Persistency.EntityTypeConfiguration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("User");
            HasKey(k => k.UserId);
            Property(p => p.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Username).HasMaxLength(150).IsRequired()
                                     .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                        new IndexAnnotation(new IndexAttribute("IX_Username", 1) { IsUnique = true }));
            Property(p => p.Password).HasMaxLength(64).IsRequired();
            Property(p => p.Enable).IsRequired();
            Property(p => p.Registration).IsRequired();
        }
    }
}

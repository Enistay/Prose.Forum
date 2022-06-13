using Prose.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Prose.Infrastructure.Persistency.EntityTypeConfiguration
{
    public class TopicConfiguration : EntityTypeConfiguration<Topic>
    {
        public TopicConfiguration()
        {
            ToTable("Topic");
            HasKey(k => k.TopicId);
            Property(p => p.TopicId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Title).HasMaxLength(150).IsRequired();
            Property(p => p.Text).IsMaxLength().IsRequired();
            Property(p => p.Creation).IsRequired();
            Property(p => p.UpdateDate).IsOptional();
            Property(p => p.Enable).IsRequired();
            Property(p => p.Keyword).IsOptional().HasMaxLength(150);
        }

    }
}

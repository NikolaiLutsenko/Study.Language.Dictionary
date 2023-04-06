using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lang.Dictionary.Adapter.Postgres.Words
{
    internal class WordEntityConfiguration : IEntityTypeConfiguration<WordDal>
    {
        public void Configure(EntityTypeBuilder<WordDal> builder)
        {
            builder.ToTable("word").HasKey(t => t.Id);
            builder.Property(d => d.Id).HasColumnName("id").IsRequired();
            builder.Property(d => d.From).HasColumnName("from").HasColumnType("jsonb");
            builder.Property(d => d.To).HasColumnName("to").HasColumnType("jsonb");
            builder.Property(d => d.OwnerId).HasColumnName("owner_id").IsRequired(false);
            builder.Property(d => d.CreatedAt).HasColumnName("created_at").IsRequired();

            builder.HasOne(d => d.Owner)
                .WithMany(x => x.Words)
                .HasForeignKey(x => x.OwnerId);
        }
    }
}

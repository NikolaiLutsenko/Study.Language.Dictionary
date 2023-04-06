using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lang.Dictionary.Adapter.Postgres.WordLists
{
    internal class WordListEntityTypeConfiguration : IEntityTypeConfiguration<WordListDal>
    {
        public void Configure(EntityTypeBuilder<WordListDal> builder)
        {
            builder.ToTable("word_list").HasKey(t => t.Id);
            builder.Property(x => x.Name);
            builder.Property(d => d.Id).HasColumnName("id").IsRequired();
            builder.Property(d => d.OwnerId).HasColumnName("owner_id").IsRequired(false);
            builder.Property(d => d.CreatedAt).HasColumnName("created_at").IsRequired();

            builder.HasOne(d => d.Owner)
                .WithMany(x => x.WordLists)
                .HasForeignKey(x => x.OwnerId);

            builder.HasMany(x => x.WordsToWordList)
                .WithOne(x => x.WordList)
                .HasForeignKey(x => x.WordListId);
        }
    }
}

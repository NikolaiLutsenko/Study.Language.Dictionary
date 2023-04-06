using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lang.Dictionary.Adapter.Postgres.WordLists
{
    internal class WordToWordListEntityTypeConfiguration : IEntityTypeConfiguration<WordToWordListDal>
    {
        public void Configure(EntityTypeBuilder<WordToWordListDal> builder)
        {
            builder.ToTable("word_to_word_list").HasKey(t => new { t.WordId, t.WordListId });
            builder.Property(x => x.WordListId).IsRequired();
            builder.Property(x => x.WordId).IsRequired();

            builder.HasOne(d => d.Word)
                .WithMany(x => x.WordToWordLists)
                .HasForeignKey(x => x.WordId);
        }
    }
}

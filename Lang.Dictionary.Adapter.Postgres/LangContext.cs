using Lang.Dictionary.Adapter.Postgres.Users;
using Lang.Dictionary.Adapter.Postgres.WordLists;
using Lang.Dictionary.Adapter.Postgres.Words;
using Microsoft.EntityFrameworkCore;

namespace Lang.Dictionary.Adapter.Postgres
{
    internal class LangContext : DbContext
    {
        public DbSet<WordDal> Words { get; set; }

        public DbSet<UserBox> Users { get; set; }

        public DbSet<WordListDal> WordsList { get; set; }

        public LangContext(DbContextOptions<LangContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WordEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new WordListEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WordToWordListEntityTypeConfiguration());
        }
    }

}

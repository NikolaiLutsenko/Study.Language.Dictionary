using Lang.Dictionary.Adapter.Postgres.Users;
using Lang.Dictionary.Adapter.Postgres.Words;
using Microsoft.EntityFrameworkCore;

namespace Lang.Dictionary.Adapter.Postgres
{
    internal class LangContext : DbContext
    {
        public DbSet<WordDal> Words { get; set; }

        public DbSet<UserBox> Users { get; set; }

        public LangContext(DbContextOptions<LangContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WordEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }
    }

}

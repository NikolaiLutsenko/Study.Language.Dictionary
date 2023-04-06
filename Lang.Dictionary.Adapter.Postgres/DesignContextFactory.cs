using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Lang.Dictionary.Adapter.Postgres
{
    internal class DesignContextFactory : IDesignTimeDbContextFactory<LangContext>
    {
        public LangContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<LangContext>()
                    .UseNpgsql("Host=localhost;Port=5432;Database=LangDictionary;UserName=postgres;Password=admin;Maximum Pool Size = 50", o => o.MigrationsAssembly(typeof(LangContext).Assembly.GetName().Name))
                    .Options;
            return new LangContext(options);
        }
    }
}

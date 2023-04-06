using Microsoft.EntityFrameworkCore;

namespace Lang.Dictionary.Adapter.Postgres
{
    internal class MigrationRuner
    {
        private LangContext _context;

        public MigrationRuner(LangContext context)
        {
            _context = context;
        }

        public async Task Run()
        {
            var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await _context.Database.MigrateAsync();
            }
        }
    }
}

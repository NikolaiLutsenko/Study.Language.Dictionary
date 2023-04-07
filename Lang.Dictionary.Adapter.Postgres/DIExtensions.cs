using Lang.Dictionary.Adapter.Postgres.Users;
using Lang.Dictionary.Adapter.Postgres.WordLists;
using Lang.Dictionary.Adapter.Postgres.Words;
using Lang.Dictionary.App.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lang.Dictionary.Adapter.Postgres
{
    public static class DIExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DbContext, LangContext>(ob => ob.UseNpgsql(connectionString, o => o.MigrationsAssembly(typeof(LangContext).Assembly.GetName().Name)), ServiceLifetime.Scoped);

            services
                .AddTransient<MigrationRuner>()
                .AddTransient<IWordRepository, WordRepository>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IWordListRepository, WordListRepository>();

            return services;
        }

        public static async Task RunMigrations(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<MigrationRuner>();
            await runner.Run();
        }
    }
}

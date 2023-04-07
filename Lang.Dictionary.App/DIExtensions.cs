using Lang.Dictionary.App.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lang.Dictionary.App
{
    public static class DIExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<WordsService>()
                .AddScoped<UserService>()
                .AddScoped<WordsListsService>();

            return services;
        }
    }
}

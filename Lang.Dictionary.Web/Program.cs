using Lang.Dictionary.Adapter.Postgres;
using Lang.Dictionary.App.Settings;
using Lang.Dictionary.App;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services
    .AddRepositories(builder.Configuration.GetConnectionString("DB"))
    .AddAppServices(builder.Configuration);
AddSettings(builder);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.Services.RunMigrations();
app.Run();

static void AddSettings(WebApplicationBuilder builder)
{
    builder.Services.Configure<LanguageSettings>(builder.Configuration.GetSection(LanguageSettings.Section));
}

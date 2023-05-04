using Infastructure.Contexts;
using Infastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infastructure.Configs
{
    public static class InfastructureServiceCollection
    {
        public static IServiceCollection AddInfrastructureServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<AddYouContext>(options =>
            {
                var connection = new SqliteConnection(configuration.GetConnectionString("AppConnection"));
                options.UseSqlite(connection);
            });

            services
                .AddTransient(o => o.GetRequiredService<IDbContextFactory<AddYouContext>>().CreateDbContext());

            services.Configure<PasswordHasherOptions>(options =>
            {
                options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3;
            });

            services.AddIdentity<AppUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<AddYouContext>()
                .AddDefaultTokenProviders();

            return services;
        }

    }
}

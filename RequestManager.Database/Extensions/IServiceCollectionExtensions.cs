using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RequestManager.Database.Contexts;
using RequestManager.Database.Models;

namespace RequestManager.Database.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DatabaseContext>(options => options
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseSqlServer(connectionString))
            .AddDatabaseDeveloperPageExceptionFilter()
            .AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<DatabaseContext>();

        return services;
    }
}
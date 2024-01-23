using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using RequestManager.Database.Contexts;

namespace RequestManager.Database.Factoryies;

public sealed class StorageDesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseSqlServer("Server=DESKTOP-1VNDJCG\\SQLEXPRESS;Database=RequestManager;User ID=sai;Password=2412;MultipleActiveResultSets=True;Encrypt=false;");
        var operationalStoreOptions = Options.Create(new OperationalStoreOptions());
        return new DatabaseContext(optionsBuilder.Options, operationalStoreOptions);
    }
}
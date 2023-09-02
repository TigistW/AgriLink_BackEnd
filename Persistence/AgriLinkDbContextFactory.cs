using Domain;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace Persistence;

public class AgriLinkDbContextFactory : IDesignTimeDbContextFactory<AgriLinkDbContext>
{
    public AgriLinkDbContext CreateDbContext(string[] args)
    {
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                  .AddJsonFile($"appsettings.{environment}.json", optional: true)
                 .Build();

        var builder = new DbContextOptionsBuilder<AgriLinkDbContext>();
        var connectionString = configuration.GetConnectionString("AgriConnectionStrings");

        builder.UseNpgsql(connectionString);

        return new AgriLinkDbContext(builder.Options);
    }

}

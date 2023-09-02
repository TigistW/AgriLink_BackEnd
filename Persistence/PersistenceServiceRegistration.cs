using Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<AgriLinkDbContext>(opt =>
           opt.UseNpgsql(configuration.GetConnectionString("AgriConnectionStrings")));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IFarmRepository, FarmRepository>();
        return services;
        
    }
}

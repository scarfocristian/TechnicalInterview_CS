using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Domain.Services;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.Repositories;
using CleanArchitecture.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServer");
        
        services.AddDbContext<DataContext>(b => b.UseLazyLoadingProxies()
            .UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();

        services.AddScoped<IKafkaService, KafkaService>();
        services.AddScoped<IElasticSearchService, ElasticSearchService>();
    }
}
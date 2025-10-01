using AnaliseVideoRepository.Interfaces;
using AnaliseVideoRepository.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AnaliseVideoRepository.Configurations;

public static class SalvarAnaliseVideoRepoConfiguration
{
    public static IServiceCollection ConfigurarSalvarAnaliseVideoRepo(this IServiceCollection services)
    {
        services.TryAddScoped(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var conn = config.GetConnectionString("MongoDb");
            return new MongoDbRepository(conn);
        });

        services.TryAddScoped<ISalvarAnaliseVideoRepository>(sp => sp.GetRequiredService<MongoDbRepository>());

        return services;
    }
}

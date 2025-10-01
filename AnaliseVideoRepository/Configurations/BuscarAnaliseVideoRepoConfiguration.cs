using AnaliseVideoRepository.Interfaces;
using AnaliseVideoRepository.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AnaliseVideoRepository.Configurations;

public static class BuscarAnaliseVideoRepoConfiguration
{
    public static IServiceCollection ConfigurarBuscarAnaliseVideoRepo(this IServiceCollection services)
    {
        services.TryAddScoped(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var conn = config.GetConnectionString("MongoDb");
            return new MongoDbRepository(conn);
        });

        services.TryAddScoped<IBuscarAnaliseVideoRepository>(sp => sp.GetRequiredService<MongoDbRepository>());

        return services;
    }
}

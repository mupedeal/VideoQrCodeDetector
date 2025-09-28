using AnaliseVideoRepository.Configurations;
using EnvioVideoApplication.Interfaces;
using EnvioVideoApplication.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EnvioVideoApplication.Configurations;

public static class EnviarVideoServiceConfiguration
{
    public static IServiceCollection ConfigurarEnviarVideoService(this IServiceCollection services)
    {
        _ = services.ConfigurarSalvarAnaliseVideoRepo();
        services.TryAddScoped<IEnviarVideoService, EnviarVideoService>();

        return services;
    }
}

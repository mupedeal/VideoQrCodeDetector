using AnaliseVideoRepository.Configurations;
using ArmazenamentoVideoService.Configurations;
using EnvioVideoApplication.Interfaces;
using EnvioVideoApplication.Services;
using FilaAnaliseVideoService.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EnvioVideoApplication.Configurations;

public static class EnviarVideoServiceConfiguration
{
    public static IServiceCollection ConfigurarEnviarVideoService(this IServiceCollection services)
    {
        _ = services.ConfigurarVpDockerService()
            .ConfigurarSalvarAnaliseVideoRepo()
            .ConfigurarRabbitMqPubAnaliseVideoEvtSvc();

        services.TryAddScoped<IEnviarVideoService, EnviarVideoService>();

        return services;
    }
}

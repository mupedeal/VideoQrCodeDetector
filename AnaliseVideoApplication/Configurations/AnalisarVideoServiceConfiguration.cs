using AnaliseVideoApplication.Services;
using AnaliseVideoRepository.Configurations;
using ArmazenamentoVideoService.Configurations;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AnaliseVideoApplication.Configurations;

public static class AnalisarVideoServiceConfiguration
{
    public static IServiceCollection ConfigurarAnalisarVideoService(this IServiceCollection services)
    {
        _ = services.ConfigurarVpDockerService()
            .ConfigurarGerenciarAnaliseVideoRepo();

        services.TryAddScoped<IAnalisarVideoService, AnalisarVideoService>();

        return services;
    }
}

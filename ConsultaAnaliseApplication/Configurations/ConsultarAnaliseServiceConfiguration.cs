using AnaliseVideoRepository.Configurations;
using ConsultaAnaliseApplication.Services;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ConsultaAnaliseApplication.Configurations;

public static class ConsultarAnaliseServiceConfiguration
{
    public static IServiceCollection ConfigurarConsultarAnaliseService(this IServiceCollection services)
    {
        _ = services.ConfigurarBuscarAnaliseVideoRepo();

        services.TryAddScoped<IConsultarStatusService, ConsultarStatusService>();
        services.TryAddScoped<IConsultarResultadoService, ConsultarResultadoService>();

        return services;
    }
}

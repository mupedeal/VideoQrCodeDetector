using ArmazenamentoVideoService.Interfaces;
using ArmazenamentoVideoService.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ArmazenamentoVideoService.Configurations;

public static class ExcluirVideoServiceConfiguration
{
    public static IServiceCollection ConfigurarExcluirVideoVpDockerService(this IServiceCollection services)
    {
        services.TryAddScoped<VolumePersistenteDockerService>();
        services.TryAddScoped<IExcluirVideoService>(sp => sp.GetRequiredService<VolumePersistenteDockerService>());

        return services;
    }
}

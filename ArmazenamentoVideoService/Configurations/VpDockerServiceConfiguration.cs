using ArmazenamentoVideoService.Interfaces;
using ArmazenamentoVideoService.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ArmazenamentoVideoService.Configurations;

public static class VpDockerServiceConfiguration
{
    public static IServiceCollection ConfigurarVpDockerService(this IServiceCollection services)
    {
        services.TryAddScoped<VolumePersistenteDockerService>();
        services.TryAddScoped<IExcluirVideoService>(sp => sp.GetRequiredService<VolumePersistenteDockerService>());
        services.TryAddScoped<ISalvarVideoService>(sp => sp.GetRequiredService<VolumePersistenteDockerService>());

        return services;
    }
}

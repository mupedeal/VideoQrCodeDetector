using ArmazenamentoVideoService.Interfaces;
using ArmazenamentoVideoService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ArmazenamentoVideoService.Configurations;

public static class VpDockerServiceConfiguration
{
    public static IServiceCollection ConfigurarVpDockerService(this IServiceCollection services)
    {
        services.AddSingleton<VolumePersistenteDockerService>();
        services.AddSingleton<IExcluirVideoService>(sp => sp.GetRequiredService<VolumePersistenteDockerService>());
        services.AddSingleton<ISalvarVideoService>(sp => sp.GetRequiredService<VolumePersistenteDockerService>());

        return services;
    }
}

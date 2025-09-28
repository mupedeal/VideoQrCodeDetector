using ArmazenamentoVideoService.Interfaces;
using ArmazenamentoVideoService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ArmazenamentoVideoService.Configurations;

public static class ExcluirVideoServiceConfiguration
{
    public static IServiceCollection ConfigurarExcluirVideoVpDockerService(this IServiceCollection services)
    {
        services.AddSingleton<VolumePersistenteDockerService>();
        services.AddSingleton<IExcluirVideoService>(sp => sp.GetRequiredService<VolumePersistenteDockerService>());

        return services;
    }
}

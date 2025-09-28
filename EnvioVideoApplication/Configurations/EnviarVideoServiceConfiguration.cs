using EnvioVideoApplication.Interfaces;
using EnvioVideoApplication.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EnvioVideoApplication.Configurations;

public static class EnviarVideoServiceConfiguration
{
    public static IServiceCollection ConfigurarEnviarVideoService(this IServiceCollection services)
    {
        services.AddScoped<IEnviarVideoService, EnviarVideoService>();

        return services;
    }
}

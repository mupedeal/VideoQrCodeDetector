using FilaAnaliseVideoService.Interfaces;
using FilaAnaliseVideoService.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FilaAnaliseVideoService.Configurations;

public static class RabbitMqPubAnaliseVideoEvtSvcCfg
{
    public static IServiceCollection ConfigurarRabbitMqPubAnaliseVideoEvtSvc(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                var config = context.GetRequiredService<IConfiguration>();
                var host = config["RabbitMq:Host"];
                var user = config["RabbitMq:User"];
                var pass = config["RabbitMq:Pass"];

                cfg.Host(host, "/", h =>
                {
                    h.Username(user);
                    h.Password(pass);
                });
            });
        });

        services.TryAddScoped<IPublicarAnaliseVideoEventoService, RabbitMqService>();

        return services;
    }
}

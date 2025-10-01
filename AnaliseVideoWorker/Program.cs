using AnaliseVideoApplication.Configurations;
using AnaliseVideoWorker;
using MassTransit;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<AnaliseVideoConsumer>();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                var config = ctx.GetRequiredService<IConfiguration>();

                cfg.Host(config["RabbitMq:Host"], "/", h =>
                {
                    h.Username(config["RabbitMq:User"]);
                    h.Password(config["RabbitMq:Pass"]);
                });

                cfg.ReceiveEndpoint("processar-video-queue", e =>
                {
                    e.ConfigureConsumer<AnaliseVideoConsumer>(ctx);
                });
            });
        });

        services.ConfigurarAnalisarVideoService();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();

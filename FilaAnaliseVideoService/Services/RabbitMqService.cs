using Domain.Entities;
using FilaAnaliseVideoService.Events;
using FilaAnaliseVideoService.Interfaces;
using MassTransit;

namespace FilaAnaliseVideoService.Services;

public class RabbitMqService(IPublishEndpoint publishEndpoint) : IPublicarAnaliseVideoEventoService
{
    public async Task PublicarAsync(AnaliseVideo analiseVideo)
    {
        await publishEndpoint.Publish<IAnaliseVideoEvent>(new
        {
            analiseVideo.Id
        });
    }
}

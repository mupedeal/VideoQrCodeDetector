using Domain.Interfaces;
using FilaAnaliseVideoService.Events;
using MassTransit;

namespace AnaliseVideoWorker;

public class AnaliseVideoConsumer(IAnalisarVideoService analisarVideoService) : IConsumer<IAnaliseVideoEvent>
{
    public async Task Consume(ConsumeContext<IAnaliseVideoEvent> context)
    {
        await analisarVideoService.AnalisarAsync(context.Message.Id);
    }
}

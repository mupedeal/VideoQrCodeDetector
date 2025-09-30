using Domain.Entities;
using FilaAnaliseVideoService.Events;
using FilaAnaliseVideoService.Services;
using MassTransit;
using MassTransit.Testing;

namespace FilaAnaliseVideoServiceTests;

public class RabbitMqServiceTests
{
    [Fact]
    public async Task PublicarAsync_DevePublicarMensagem()
    {
        // Arrange
        var harness = new InMemoryTestHarness();
        var consumerHarness = harness.Consumer(() => new TestConsumer());

        await harness.Start();

        var service = new RabbitMqService(harness.Bus);

        var id = Guid.NewGuid().ToString();
        var analise = new AnaliseVideo("video.mp4", "video/mp4", id, "/tmp");

        // Act
        await service.PublicarAsync(analise);

        // Assert
        Assert.True(await harness.Published.Any<IAnaliseVideoEvent>());
        Assert.True(await consumerHarness.Consumed.Any<IAnaliseVideoEvent>());

        await harness.Stop();
    }

    private class TestConsumer : IConsumer<IAnaliseVideoEvent>
    {
        public Task Consume(ConsumeContext<IAnaliseVideoEvent> context)
        {
            Console.WriteLine($"Mensagem recebida: {context.Message.Id}");
            return Task.CompletedTask;
        }
    }
}

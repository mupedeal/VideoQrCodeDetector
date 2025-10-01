using AnaliseVideoApplication.Services;
using AnaliseVideoRepository.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Moq;

namespace AnaliseVideoApplicationTests;

public class AnalisarVideoServiceTests
{
    [Fact]
    public async Task AnalisarAsync_DeveProcessarVideoComSucesso()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var caminho = "/tmp/videos";
        var analise = new AnaliseVideo("video.mp4", "video/mp4", id, caminho);

        var buscarMock = new Mock<IBuscarAnaliseVideoRepository>();
        buscarMock.Setup(r => r.BuscarPorIdAsync(id)).ReturnsAsync(analise);

        var atualizarMock = new Mock<IAtualizarAnaliseVideoRepository>();

        var resultados = new List<ResultadoAnalise> { new("QR123", 0.0, 1.0) };
        var detectorMock = new Mock<IQrCodeDetector>();
        detectorMock.Setup(d => d.Detectar(analise.CaminhoCompleto)).Returns(resultados);

        var service = new AnalisarVideoService(atualizarMock.Object, buscarMock.Object, detectorMock.Object);

        // Act
        await service.AnalisarAsync(id);

        // Assert
        Assert.Equal(StatusEnum.PROCESSADO.ToString(), analise.Status);
        Assert.Single(analise.Resultados);
        Assert.Equal("QR123", analise.Resultados[0].Conteudo);
        Assert.Equal(analise.CaminhoCompleto, $"{caminho}\\{analise.NomeUnico}");

        // Verifica que AtualizarAsync foi chamado duas vezes
        atualizarMock.Verify(r => r.AtualizarAsync(It.IsAny<AnaliseVideo>()), Times.Exactly(2));

        // Verifica que o detector foi chamado com o caminho correto
        detectorMock.Verify(d => d.Detectar(analise.CaminhoCompleto), Times.Once);
    }
}

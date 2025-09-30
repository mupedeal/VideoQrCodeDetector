using AnaliseVideoRepository.Interfaces;
using ArmazenamentoVideoService.Interfaces;
using Domain.Entities;
using EnvioVideoApplication.Services;
using FilaAnaliseVideoService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using System.Text;

namespace EnvioVideoApplicationTests;

public class EnviarVideoServiceTests
{
    [Fact]
    public async Task EnviarVideoAsync_DeveSalvarAnaliseVideoERetornarId()
    {
        // Arrange
        var mockSalvarVideo = new Mock<ISalvarVideoService>();
        var mockSalvarAnaliseRepo = new Mock<ISalvarAnaliseVideoRepository>();
        var mockExcluirVideo = new Mock<IExcluirVideoService>();
        var mockPublicarNaFila = new Mock<IPublicarAnaliseVideoEventoService>();

        var fakeId = Guid.NewGuid().ToString();
        var fakeAnalise = new AnaliseVideo(
            nomeArquivo: "video.mp4",
            contentType: "video/mp4",
            id: fakeId,
            caminhoArmazenamento: "/app/videos"
        );

        mockSalvarVideo.Setup(s => s.SalvarVideoAsync(It.IsAny<IFormFile>()))
                       .ReturnsAsync(fakeAnalise);

        mockSalvarAnaliseRepo.Setup(r => r.SalvarAsync(It.IsAny<AnaliseVideo>()))
                             .Returns(Task.CompletedTask);

        mockPublicarNaFila.Setup(r => r.PublicarAsync(It.IsAny<AnaliseVideo>()))
                             .Returns(Task.CompletedTask);

        var service = new EnviarVideoService(
            mockExcluirVideo.Object,
            mockPublicarNaFila.Object,
            mockSalvarAnaliseRepo.Object,
            mockSalvarVideo.Object
        );

        var content = "fake video content";
        var fileName = "video.mp4";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        var formFile = new FormFile(stream, 0, stream.Length, "video", fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "video/mp4"
        };

        // Act
        var result = await service.EnviarVideoAsync(formFile);

        // Assert
        Assert.Equal(fakeId, result);
        mockSalvarVideo.Verify(s => s.SalvarVideoAsync(formFile), Times.Once);
        mockSalvarAnaliseRepo.Verify(r => r.SalvarAsync(fakeAnalise), Times.Once);
    }
}

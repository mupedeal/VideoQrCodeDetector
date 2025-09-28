using ArmazenamentoVideoService.Interfaces;
using EnvioVideoApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using System.Text;

namespace EnvioVideoApplicationTests;

public class EnviarVideoServiceTests
{
    [Fact]
    public async Task EnviarVideoAsync_ReturnsId()
    {
        // Arrange
        var mockSalvar = new Mock<ISalvarVideoService>();
        var mockExcluir = new Mock<IExcluirVideoService>();

        var fakeId = Guid.NewGuid().ToString();
        mockSalvar.Setup(s => s.SalvarVideoAsync(It.IsAny<IFormFile>()))
                  .ReturnsAsync(fakeId);

        var service = new EnviarVideoService(mockExcluir.Object, mockSalvar.Object);

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

    }
}

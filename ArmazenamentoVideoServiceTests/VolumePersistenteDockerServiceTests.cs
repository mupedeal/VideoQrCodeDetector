using ArmazenamentoVideoService.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.Text;

namespace ArmazenamentoVideoServiceTests;

public class VolumePersistenteDockerServiceTests : IDisposable
{
    private readonly string _tempPath;

    public VolumePersistenteDockerServiceTests()
    {
        _tempPath = Path.Combine(Path.GetTempPath(), "videos_test");
        Directory.CreateDirectory(_tempPath);
    }

    [Fact]
    public async Task SalvarVideoAsync_DeveSalvarArquivoERetornarId()
    {
        // Arrange
        var service = new VolumePersistenteDockerService(_tempPath);

        var content = "conteúdo de vídeo falso";
        var fileName = "teste.mp4";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        var formFile = new FormFile(stream, 0, stream.Length, "video", fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "video/mp4"
        };

        // Act
        var id = await service.SalvarVideoAsync(formFile);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(id));

        var arquivoSalvo = Directory.GetFiles(_tempPath)
            .FirstOrDefault(f => Path.GetFileNameWithoutExtension(f) == id);

        Assert.NotNull(arquivoSalvo);
    }

    [Fact]
    public async Task SalvarVideoAsync_DeveLancarExcecao_SeArquivoForNuloOuVazio()
    {
        // Arrange
        var service = new VolumePersistenteDockerService(_tempPath);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => service.SalvarVideoAsync(null));
    }

    [Fact]
    public void ExcluirVideo_DeveExcluirArquivoExistente()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var nomeArquivo = $"{id}.mp4";
        var caminhoCompleto = Path.Combine(_tempPath, nomeArquivo);
        File.WriteAllText(caminhoCompleto, "conteúdo falso");

        var analise = new AnaliseVideo(nomeArquivo, "video/mp4", id, _tempPath);
        var service = new VolumePersistenteDockerService(_tempPath);

        // Act
        var resultado = service.ExcluirVideo(analise);

        // Assert
        Assert.True(resultado);
        Assert.False(File.Exists(caminhoCompleto));
    }

    [Fact]
    public void ExcluirVideo_DeveRetornarFalse_SeArquivoNaoExistir()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var nomeArquivo = $"{id}.mp4";
        var analise = new AnaliseVideo(nomeArquivo, "video/mp4", id, _tempPath);
        var service = new VolumePersistenteDockerService(_tempPath);

        // Act
        var resultado = service.ExcluirVideo(analise);

        // Assert
        Assert.False(resultado);
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempPath))
            Directory.Delete(_tempPath, true);
    }
}

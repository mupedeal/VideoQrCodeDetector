using Domain.Entities;

namespace DomainTests;

public class AnaliseVideoTests
{
    [Theory]
    [InlineData("video.mp4", "video/mp4")]
    [InlineData("filme.avi", "video/x-msvideo")]
    [InlineData("clip.mkv", "video/x-matroska")]
    [InlineData("trailer.mov", "video/quicktime")]
    [InlineData("documentario.mpeg", "video/mpeg")]
    public void Construtor_DeveInicializarPropriedadesCorretamente(string nomeArquivo, string contentType)
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var caminho = "/app/videos";

        // Act
        var analise = new AnaliseVideo(nomeArquivo, contentType, id, caminho);

        // Assert
        Assert.Equal(nomeArquivo, analise.NomeArquivo);
        Assert.Equal(contentType, analise.ContentType);
        Assert.Equal(Path.GetExtension(nomeArquivo), analise.Extensao);
        Assert.Equal(id, analise.Id);
        Assert.Equal($"{id}{Path.GetExtension(nomeArquivo)}", analise.NomeUnico);
        Assert.Equal(Path.Combine(caminho, analise.NomeUnico), analise.CaminhoCompleto);
    }

    [Theory]
    [InlineData("video.txt", "text/plain")]
    [InlineData("imagem.jpg", "image/jpeg")]
    [InlineData("audio.mp3", "audio/mpeg")]
    public void Construtor_DeveLancarExcecaoParaTipoInvalido(string nomeArquivo, string contentType)
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var caminho = "/app/videos";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new AnaliseVideo(nomeArquivo, contentType, id, caminho));

        Assert.Contains("não é um vídeo compatível", ex.Message);
    }

}

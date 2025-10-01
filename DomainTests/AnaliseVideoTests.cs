using Domain.Entities;
using Domain.Enums;

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

    [Fact]
    public void Construtor_DeveLancarExcecaoParaStatusInvalido()
    {
        var id = Guid.NewGuid().ToString();
        var caminho = "/app/videos";

        var ex = Assert.Throws<ArgumentException>(() =>
            new AnaliseVideo("video.mp4", "video/mp4", id, caminho, "INVALIDO"));

        Assert.Contains("Valor inválido para status da análise", ex.Message);
    }

    [Fact]
    public void Construtor_DeveLancarExcecaoSeResultadosInformadosComStatusNaoProcessado()
    {
        var id = Guid.NewGuid().ToString();
        var caminho = "/app/videos";
        var resultados = new List<ResultadoAnalise> { new("QR123", 0.0, 1.0) };

        var ex = Assert.Throws<ArgumentException>(() =>
            new AnaliseVideo("video.mp4", "video/mp4", id, caminho, StatusEnum.AGUARDANDO_PROCESSAMENTO.ToString(), resultados));

        Assert.Contains("não está com status PROCESSADO", ex.Message);
    }

    [Fact]
    public void IniciarProcessamento_DeveAtualizarStatus()
    {
        var analise = new AnaliseVideo("video.mp4", "video/mp4", Guid.NewGuid().ToString(), "/app/videos");

        analise.IniciarProcessamento();

        Assert.Equal(StatusEnum.EM_PROCESSAMENTO.ToString(), analise.Status);
    }

    [Fact]
    public void IniciarProcessamento_DeveLancarExcecaoSeStatusInvalido()
    {
        var analise = new AnaliseVideo(
            "video.mp4",
            "video/mp4",
            Guid.NewGuid().ToString(),
            "/app/videos",
            StatusEnum.PROCESSADO.ToString(),
            new List<ResultadoAnalise> { new("QR123", 0.0, 1.0) });

        Assert.Equal(StatusEnum.PROCESSADO.ToString(), analise.Status);

        var ex = Assert.Throws<InvalidOperationException>(() => analise.IniciarProcessamento());
        Assert.Contains("Não é permitido iniciar processamento", ex.Message);
    }

    [Fact]
    public void AdicionarResultados_DeveAtualizarStatusEAdicionar()
    {
        var analise = new AnaliseVideo("video.mp4", "video/mp4", Guid.NewGuid().ToString(), "/app/videos");
        analise.IniciarProcessamento();

        var resultados = new List<ResultadoAnalise> { new("QR123", 0.0, 1.0) };
        analise.AdicionarResultados(resultados);

        Assert.Equal(StatusEnum.PROCESSADO.ToString(), analise.Status);
        Assert.Single(analise.Resultados);
    }

    [Fact]
    public void AdicionarResultados_DeveLancarExcecaoSeStatusInvalido()
    {
        var analise = new AnaliseVideo("video.mp4", "video/mp4", Guid.NewGuid().ToString(), "/app/videos");

        Assert.Equal(StatusEnum.AGUARDANDO_PROCESSAMENTO.ToString(), analise.Status);

        var resultados = new List<ResultadoAnalise> { new("QR123", 0.0, 1.0) };

        var ex = Assert.Throws<InvalidOperationException>(() => analise.AdicionarResultados(resultados));
        Assert.Contains("Não é permitido adicionar resultados", ex.Message);
    }
}

using AnaliseVideoRepository.Documents;
using AnaliseVideoRepository.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mongo2Go;
using MongoDB.Driver;

public class MongoDbRepositoryTests : IDisposable
{
    private readonly MongoDbRunner _runner;
    private readonly MongoDbRepository _repository;
    private readonly IMongoCollection<AnaliseVideoDocument> _collection;

    public MongoDbRepositoryTests()
    {
        _runner = MongoDbRunner.Start();
        _repository = new MongoDbRepository(_runner.ConnectionString);

        var client = new MongoClient(_runner.ConnectionString);
        var database = client.GetDatabase("envio_video_db");
        _collection = database.GetCollection<AnaliseVideoDocument>("analises");
    }

    [Fact]
    public async Task SalvarAsync_DevePersistirDocumento()
    {
        var id = Guid.NewGuid().ToString();
        var analise = new AnaliseVideo("teste.mp4", "video/mp4", id, "/tmp/videos");

        await _repository.SalvarAsync(analise);

        var documento = await _collection.Find(d => d.Id == id).FirstOrDefaultAsync();
        Assert.NotNull(documento);
        Assert.Equal("teste.mp4", documento.NomeArquivo);
    }

    [Fact]
    public async Task AtualizarAsync_DeveSobrescreverDocumentoExistente()
    {
        var id = Guid.NewGuid().ToString();
        var original = new AnaliseVideo("original.mp4", "video/mp4", id, "/tmp/videos");
        await _repository.SalvarAsync(original);

        var atualizado = new AnaliseVideo("atualizado.mp4", "video/mp4", id, "/tmp/videos", StatusEnum.PROCESSADO.ToString(), new List<ResultadoAnalise> { new("QR123", 0.0, 1.0) });
        await _repository.AtualizarAsync(atualizado);

        var documento = await _collection.Find(d => d.Id == id).FirstOrDefaultAsync();
        Assert.NotNull(documento);
        Assert.Equal("atualizado.mp4", documento.NomeArquivo);
        Assert.Equal(StatusEnum.PROCESSADO.ToString(), documento.Status);
        Assert.Single(documento.Resultados);
    }

    [Fact]
    public async Task BuscarPorIdAsync_DeveRetornarDocumentoExistente()
    {
        var id = Guid.NewGuid().ToString();
        var analise = new AnaliseVideo("busca.mp4", "video/mp4", id, "/tmp/videos");
        await _repository.SalvarAsync(analise);

        var resultado = await _repository.BuscarPorIdAsync(id);

        Assert.NotNull(resultado);
        Assert.Equal("busca.mp4", resultado.NomeArquivo);
    }

    [Fact]
    public async Task BuscarPorIdAsync_DeveRetornarNullSeNaoExistir()
    {
        var resultado = await _repository.BuscarPorIdAsync("inexistente-id");

        Assert.Null(resultado);
    }

    public void Dispose()
    {
        _runner.Dispose();
    }
}

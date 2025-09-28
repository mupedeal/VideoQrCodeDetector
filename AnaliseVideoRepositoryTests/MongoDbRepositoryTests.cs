using AnaliseVideoRepository.Documents;
using AnaliseVideoRepository.Repositories;
using Domain.Entities;
using Mongo2Go;
using MongoDB.Driver;

namespace AnaliseVideoRepositoryTests;

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
        // Arrange
        var id = Guid.NewGuid().ToString();
        var analise = new AnaliseVideo(
            nomeArquivo: "teste.mp4",
            contentType: "video/mp4",
            id: id,
            caminhoArmazenamento: "/tmp/videos"
        );

        // Act
        await _repository.SalvarAsync(analise);

        // Assert
        var documento = await _collection.Find(d => d.Id == id).FirstOrDefaultAsync();
        Assert.NotNull(documento);
        Assert.Equal("teste.mp4", documento.NomeArquivo);
        Assert.Equal("video/mp4", documento.ContentType);
    }

    public void Dispose()
    {
        _runner.Dispose();
    }
}

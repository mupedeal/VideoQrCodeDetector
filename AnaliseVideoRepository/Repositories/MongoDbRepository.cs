using AnaliseVideoRepository.Documents;
using AnaliseVideoRepository.Interfaces;
using Domain.Entities;
using MongoDB.Driver;

namespace AnaliseVideoRepository.Repositories;

public class MongoDbRepository : ISalvarAnaliseVideoRepository
{
    private readonly IMongoCollection<AnaliseVideoDocument> _collection;

    public MongoDbRepository(string connectionString)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("envio_video_db");
        _collection = database.GetCollection<AnaliseVideoDocument>("analises");
    }

    public async Task SalvarAsync(AnaliseVideo analiseVideo)
    {
        var analiseVideoDocument = new AnaliseVideoDocument(analiseVideo);

        await _collection.InsertOneAsync(analiseVideoDocument);
    }
}

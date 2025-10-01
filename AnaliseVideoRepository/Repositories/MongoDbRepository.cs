using AnaliseVideoRepository.Documents;
using AnaliseVideoRepository.Interfaces;
using Domain.Entities;
using MongoDB.Driver;

namespace AnaliseVideoRepository.Repositories;

public class MongoDbRepository : IAtualizarAnaliseVideoRepository, IBuscarAnaliseVideoRepository, ISalvarAnaliseVideoRepository
{
    private readonly IMongoCollection<AnaliseVideoDocument> _collection;

    public MongoDbRepository(string connectionString)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("envio_video_db");
        _collection = database.GetCollection<AnaliseVideoDocument>("analises");
    }

    public async Task AtualizarAsync(AnaliseVideo analiseVideo)
    {
        var analiseVideoDocument = AnaliseVideoDocument.FromAnaliseVideo(analiseVideo);

        await _collection.ReplaceOneAsync(
            doc => doc.Id == analiseVideoDocument.Id,
            analiseVideoDocument,
            new ReplaceOptions { IsUpsert = true });
    }

    public async Task<AnaliseVideo?> BuscarPorIdAsync(string id)
    {
        var filtro = Builders<AnaliseVideoDocument>.Filter.Eq(x => x.Id, id);
        var documento = await _collection.Find(filtro).FirstOrDefaultAsync();

        return documento?.ToAnaliseVideo();
    }


    public async Task SalvarAsync(AnaliseVideo analiseVideo)
    {
        var analiseVideoDocument = AnaliseVideoDocument.FromAnaliseVideo(analiseVideo);

        await _collection.InsertOneAsync(analiseVideoDocument);
    }
}

using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AnaliseVideoRepository.Documents;

public class AnaliseVideoDocument(AnaliseVideo analiseVideo)
{
    public string CaminhoCompleto { get; set; } = analiseVideo.CaminhoCompleto;
    public string ContentType { get; set; } = analiseVideo.ContentType;
    public string Extensao { get; set; } = analiseVideo.Extensao;
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; } = analiseVideo.Id;
    public string NomeArquivo { get; set; } = analiseVideo.NomeArquivo;
    public string NomeUnico { get; set; } = analiseVideo.NomeUnico;
}

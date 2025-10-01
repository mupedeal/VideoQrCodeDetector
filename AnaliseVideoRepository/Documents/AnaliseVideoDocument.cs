using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AnaliseVideoRepository.Documents;

public class AnaliseVideoDocument
{
    public required string CaminhoCompleto { get; set; }
    public required string ContentType { get; set; }
    public required string Extensao { get; set; }
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public required string Id { get; set; }
    public required string NomeArquivo { get; set; }
    public required string NomeUnico { get; set; }
    public required string Status { get; set; }
    public List<ResultadoAnaliseDocument> Resultados { get; set; } = new();

    public AnaliseVideoDocument() { }

    public static AnaliseVideoDocument FromAnaliseVideo(AnaliseVideo analiseVideo)
    {
        return new AnaliseVideoDocument
        {
            CaminhoCompleto = analiseVideo.CaminhoCompleto,
            ContentType = analiseVideo.ContentType,
            Extensao = analiseVideo.Extensao,
            Id = analiseVideo.Id,
            NomeArquivo = analiseVideo.NomeArquivo,
            NomeUnico = analiseVideo.NomeUnico,
            Status = analiseVideo.Status,
            Resultados = analiseVideo.Resultados
                .Select(ResultadoAnaliseDocument.FromResultadoAnalise)
                .ToList()
        };
    }

    public AnaliseVideo ToAnaliseVideo()
    {
        return new AnaliseVideo(
            nomeArquivo: NomeArquivo,
            contentType: ContentType,
            id: Id,
            caminhoArmazenamento: Path.GetDirectoryName(CaminhoCompleto) ?? "",
            status: Status,
            resultados: Resultados.Select(r => r.ToResultadoAnalise()).ToList()
        );
    }
}

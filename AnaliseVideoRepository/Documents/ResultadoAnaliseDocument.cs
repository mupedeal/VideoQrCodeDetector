using Domain.Entities;

namespace AnaliseVideoRepository.Documents;

public class ResultadoAnaliseDocument
{
    public required string Conteudo { get; set; }
    public List<ResultadoTimestampDocument> Timestamps { get; set; } = new();

    public ResultadoAnaliseDocument() { }

    public static ResultadoAnaliseDocument FromResultadoAnalise(ResultadoAnalise resultado)
    {
        return new ResultadoAnaliseDocument
        {
            Conteudo = resultado.Conteudo,
            Timestamps = resultado.Timestamps
                .Select(ResultadoTimestampDocument.FromResultadoTimestamp)
                .ToList()
        };
    }

    public ResultadoAnalise ToResultadoAnalise()
    {
        var timestamps = Timestamps.Select(t => t.ToResultadoTimestamp()).ToList();
        var resultado = new ResultadoAnalise(Conteudo, timestamps.First().Inicio, timestamps.First().Fim);
        resultado.Timestamps.AddRange(timestamps.Skip(1)); // preserva múltiplos timestamps
        return resultado;
    }

}

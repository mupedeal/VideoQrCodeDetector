namespace Domain.Entities;

public class ResultadoAnalise
{
    public string Conteudo { get; init; }
    public List<ResultadoTimestamp> Timestamps { get; init; } = [];
    
    public ResultadoAnalise(string conteudo, double inicio, double fim)
    {
        Conteudo = conteudo;
        Timestamps.Add(new(inicio, fim));
    }
}

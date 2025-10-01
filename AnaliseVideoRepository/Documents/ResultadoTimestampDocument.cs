using Domain.Entities;

namespace AnaliseVideoRepository.Documents;

public class ResultadoTimestampDocument
{
    public double Inicio { get; set; }
    public double Fim { get; set; }
    public double Duracao { get; set; }

    public ResultadoTimestampDocument() { }

    public static ResultadoTimestampDocument FromResultadoTimestamp(ResultadoTimestamp timestamp)
    {
        return new ResultadoTimestampDocument
        {
            Inicio = timestamp.Inicio,
            Fim = timestamp.Fim,
            Duracao = timestamp.Duracao
        };
    }

    public ResultadoTimestamp ToResultadoTimestamp()
    {
        return new ResultadoTimestamp(Inicio, Fim);
    }

}

namespace Domain.Entities;

public class ResultadoTimestamp
{
    public double Inicio { get; init; }
    public double Fim { get; init; }
    public double Duracao
    {
        get
        {
            return Fim - Inicio;
        }
    }

    public ResultadoTimestamp(double inicio, double fim)
    {
        Inicio = inicio;
        Fim = fim;
    }
}

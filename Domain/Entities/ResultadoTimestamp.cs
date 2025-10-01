namespace Domain.Entities;

public class ResultadoTimestamp
{
    public double Inicio { get; init; } //$"{inicio:F2} s"
    public double Fim { get; init; } //$"{fim:F2} s"
    public double Duracao //$"{(fim - inicio):F2} s"
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

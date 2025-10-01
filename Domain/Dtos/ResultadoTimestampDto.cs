namespace Domain.Dtos;

public class ResultadoTimestampDto(double inicio, double fim, double duracao)
{
    public string Inicio { get; set; } = $"{inicio:F2} s";
    public string Fim { get; set; } = $"{fim:F2} s";
    public string Duracao { get; set; } = $"{duracao:F2} s";
}

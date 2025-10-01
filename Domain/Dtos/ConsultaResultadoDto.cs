namespace Domain.Dtos;

public class ConsultaResultadoDto(List<ResultadoAnaliseDto> resultados)
{
    public List<ResultadoAnaliseDto> Resultados { get; set; } = resultados;
}

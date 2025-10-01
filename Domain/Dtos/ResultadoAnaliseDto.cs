namespace Domain.Dtos;

public class ResultadoAnaliseDto(string qRCode, List<ResultadoTimestampDto> ocorrencias)
{
    public string QRCode { get; set; } = qRCode;
    public List<ResultadoTimestampDto> Ocorrencias { get; set; } = ocorrencias;
}

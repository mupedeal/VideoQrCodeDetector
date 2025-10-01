using Domain.Enums;

namespace Domain.Entities;

public class AnaliseVideo
{
    public string CaminhoCompleto { get; init; }
    public string ContentType { get; init; }
    public string Extensao { get; init; }
    public string Id { get; init; }
    public string NomeArquivo { get; init; }
    public string NomeUnico { get; init; }
    public string Status { get; private set; } //StatusEnum
    public List<ResultadoAnalise> Resultados { get; private set; } = [];

    public AnaliseVideo(string nomeArquivo, string contentType, string id, string caminhoArmazenamento, string? status = null, List<ResultadoAnalise>? resultados = null)
    {
        ValidateContentType(contentType);
        NomeArquivo = nomeArquivo;
        Extensao = Path.GetExtension(nomeArquivo);        
        ContentType = contentType;
        Id = id;
        NomeUnico = $"{Id}{Extensao}";
        CaminhoCompleto = Path.Combine(caminhoArmazenamento, NomeUnico);

        if (!string.IsNullOrEmpty(status) && !Enum.IsDefined(typeof(StatusEnum), status))
            throw new ArgumentException($"Valor inválido para status da análise. Valor informado: {status}.");

        Status = status ?? StatusEnum.AGUARDANDO_PROCESSAMENTO.ToString();

        if ((Status == StatusEnum.AGUARDANDO_PROCESSAMENTO.ToString()
            || Status == StatusEnum.EM_PROCESSAMENTO.ToString()) && (resultados?.Count ?? 0) > 0)
            throw new ArgumentException($"Foram informados resultados para uma amostra que não está com status {StatusEnum.PROCESSADO}. Status informado: {status}.");

        Resultados = resultados ?? [];
    }

    private readonly string[] _validVideoTypes =
    [
        "video/mp4",
        "video/x-msvideo",     // AVI
        "video/x-matroska",    // MKV
        "video/quicktime",     // MOV
        "video/mpeg"
    ];

    private void ValidateContentType(string contentType)
    {
        if (!_validVideoTypes.Contains(contentType))
            throw new ArgumentException($"O arquivo enviado não é um vídeo compatível. Tipos compatíveis: ${string.Join(",", _validVideoTypes)}.");
    }

    public void IniciarProcessamento()
    {
        if (Status != StatusEnum.AGUARDANDO_PROCESSAMENTO.ToString())
            throw new InvalidOperationException($"Não é permitido iniciar processamento de uma análise com status diferente de {StatusEnum.AGUARDANDO_PROCESSAMENTO}. Status atual: {Status}.");

        Status = StatusEnum.EM_PROCESSAMENTO.ToString();
    }

    public void AdicionarResultados(List<ResultadoAnalise> resultados)
    {
        if (Status != StatusEnum.EM_PROCESSAMENTO.ToString())
            throw new InvalidOperationException($"Não é permitido adicionar resultados a uma análise com status diferente de {StatusEnum.EM_PROCESSAMENTO}. Status atual: {Status}.");

        if (Resultados.Count > 0)
            throw new InvalidOperationException($"A análise atual já possui resultados.");

        Status = StatusEnum.PROCESSADO.ToString();
        Resultados.AddRange(resultados);
    }
}

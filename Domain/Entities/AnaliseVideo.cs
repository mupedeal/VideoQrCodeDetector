namespace Domain.Entities;

public class AnaliseVideo
{
    public string CaminhoCompleto { get; init; }
    public string ContentType { get; init; }
    public string Extensao { get; init; }
    public string Id { get; init; }
    public string NomeArquivo { get; init; }
    public string NomeUnico { get; init; }

    public AnaliseVideo(string nomeArquivo, string contentType, string id, string caminhoArmazenamento)
    {
        ValidateContentType(contentType);
        NomeArquivo = nomeArquivo;
        Extensao = Path.GetExtension(nomeArquivo);        
        ContentType = contentType;
        Id = id;
        NomeUnico = $"{Id}{Extensao}";
        CaminhoCompleto = Path.Combine(caminhoArmazenamento, NomeUnico);
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
}

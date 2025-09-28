using ArmazenamentoVideoService.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace ArmazenamentoVideoService.Services;

public class VolumePersistenteDockerService(string videoStoragePath = "/app/videos") : IExcluirVideoService, ISalvarVideoService
{
    public bool ExcluirVideo(AnaliseVideo analiseVideo)
    {
        if (!Directory.Exists(videoStoragePath))
            return false;

        var arquivos = Directory.GetFiles(videoStoragePath);

        foreach (var caminho in arquivos)
        {
            if (caminho.Equals(analiseVideo.CaminhoCompleto, StringComparison.OrdinalIgnoreCase))
            {
                File.Delete(caminho);
                return true;
            }
        }

        return false; // vídeo não encontrado
    }

    public async Task<string> SalvarVideoAsync(IFormFile? video)
    {
        if (video == null || video.Length == 0)
            throw new ArgumentException("Nenhum arquivo foi enviado.");

        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(video.FileName, out var contentType))
            contentType = video.ContentType;

        if (!Directory.Exists(videoStoragePath))
            Directory.CreateDirectory(videoStoragePath);

        var analiseVideo = new AnaliseVideo(video.FileName, contentType, Guid.NewGuid().ToString(), videoStoragePath);

        using var stream = File.Create(analiseVideo.CaminhoCompleto);
        await video.CopyToAsync(stream);

        return analiseVideo.Id;
    }
}

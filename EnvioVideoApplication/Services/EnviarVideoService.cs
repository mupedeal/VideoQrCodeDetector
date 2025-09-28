using AnaliseVideoRepository.Interfaces;
using ArmazenamentoVideoService.Interfaces;
using Domain.Entities;
using EnvioVideoApplication.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EnvioVideoApplication.Services;

public class EnviarVideoService(
    IExcluirVideoService excluirVideoService, 
    ISalvarAnaliseVideoRepository salvarAnaliseVideoRepository,
    ISalvarVideoService salvarVideoService) : IEnviarVideoService
{
    public async Task<string> EnviarVideoAsync(IFormFile video)
    {
        AnaliseVideo analiseVideo = await salvarVideoService.SalvarVideoAsync(video);

        await salvarAnaliseVideoRepository.SalvarAsync(analiseVideo);

        return analiseVideo.Id;
    }
}

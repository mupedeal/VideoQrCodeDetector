using AnaliseVideoRepository.Interfaces;
using ArmazenamentoVideoService.Interfaces;
using Domain.Entities;
using EnvioVideoApplication.Interfaces;
using FilaAnaliseVideoService.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EnvioVideoApplication.Services;

public class EnviarVideoService(
    IExcluirVideoService excluirVideoService,
    IPublicarAnaliseVideoEventoService pubAnaliseVideoEvtSvc,
    ISalvarAnaliseVideoRepository salvarAnaliseVideoRepository,
    ISalvarVideoService salvarVideoService) : IEnviarVideoService
{
    public async Task<string> EnviarVideoAsync(IFormFile video)
    {
        AnaliseVideo analiseVideo = await salvarVideoService.SalvarVideoAsync(video);

        await salvarAnaliseVideoRepository.SalvarAsync(analiseVideo);

        await pubAnaliseVideoEvtSvc.PublicarAsync(analiseVideo);

        return analiseVideo.Id;
    }
}

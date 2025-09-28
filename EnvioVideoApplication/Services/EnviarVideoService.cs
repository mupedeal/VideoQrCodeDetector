using ArmazenamentoVideoService.Interfaces;
using EnvioVideoApplication.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EnvioVideoApplication.Services;

public class EnviarVideoService(IExcluirVideoService excluirVideoService, ISalvarVideoService salvarVideoService) : IEnviarVideoService
{
    public async Task<string> EnviarVideoAsync(IFormFile video)
    {
        string id = await salvarVideoService.SalvarVideoAsync(video);

        return id;
    }
}

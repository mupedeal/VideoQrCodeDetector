using Microsoft.AspNetCore.Http;

namespace EnvioVideoApplication.Interfaces;

public interface IEnviarVideoService
{
    Task<string> EnviarVideoAsync(IFormFile video);
}

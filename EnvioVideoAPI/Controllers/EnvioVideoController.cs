using EnvioVideoApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnvioVideoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnvioVideoController(IEnviarVideoService enviarVideoService) : ControllerBase
{
    [HttpPost("enviar")]
    [RequestSizeLimit(1_073_741_824)] // 1 GB
    public async Task<IActionResult> EnviarVideoAsync(IFormFile video)
    {
        string id = await enviarVideoService.EnviarVideoAsync(video);

        return Ok(new
        {
            message = "Vídeo enviado com sucesso.",
            id
        });
    }
}

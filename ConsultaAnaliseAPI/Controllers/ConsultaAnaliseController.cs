using Domain.Dtos;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaAnaliseAPI.Controllers;

[ApiController]
[Route("api/analise")]
public class ConsultaAnaliseController(
    IConsultarStatusService consultarStatusService,
    IConsultarResultadoService consultarResultadoService) : ControllerBase
{
    [HttpGet("{id}/status")]
    public async Task<ActionResult<ConsultaStatusDto>> ConsultarStatus(string id)
    {
        try
        {
            var resultado = await consultarStatusService.ConsultarAsync(id);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    [HttpGet("{id}/resultados")]
    public async Task<ActionResult<ConsultaResultadoDto>> ConsultarResultados(string id)
    {
        try
        {
            var resultado = await consultarResultadoService.ConsultarAsync(id);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
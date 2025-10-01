using Domain.Dtos;

namespace Domain.Interfaces;

public interface IConsultarResultadoService
{
    Task<ConsultaResultadoDto> ConsultarAsync(string id);
}

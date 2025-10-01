using Domain.Dtos;

namespace Domain.Interfaces;

public interface IConsultarStatusService
{
    Task<ConsultaStatusDto> ConsultarAsync(string id);
}

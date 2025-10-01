using Domain.Entities;

namespace FilaAnaliseVideoService.Interfaces;

public interface IPublicarAnaliseVideoEventoService
{
    Task PublicarAsync(AnaliseVideo analiseVideo);
}

using Domain.Entities;

namespace AnaliseVideoRepository.Interfaces;

public interface IBuscarAnaliseVideoRepository
{
    Task<AnaliseVideo?> BuscarPorIdAsync(string id);
}

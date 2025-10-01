using Domain.Entities;

namespace AnaliseVideoRepository.Interfaces;

public interface IAtualizarAnaliseVideoRepository
{
    Task AtualizarAsync(AnaliseVideo analiseVideo);
}

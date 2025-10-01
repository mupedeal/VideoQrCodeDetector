using Domain.Entities;

namespace AnaliseVideoRepository.Interfaces;

public interface ISalvarAnaliseVideoRepository
{
    Task SalvarAsync(AnaliseVideo analiseVideo);
}

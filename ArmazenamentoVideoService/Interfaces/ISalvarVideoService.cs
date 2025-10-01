using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ArmazenamentoVideoService.Interfaces
{
    public interface ISalvarVideoService
    {
        Task<AnaliseVideo> SalvarVideoAsync(IFormFile video);
    }
}

using Microsoft.AspNetCore.Http;

namespace ArmazenamentoVideoService.Interfaces
{
    public interface ISalvarVideoService
    {
        Task<string> SalvarVideoAsync(IFormFile video);
    }
}

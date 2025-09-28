using Domain.Entities;

namespace ArmazenamentoVideoService.Interfaces;

public interface IExcluirVideoService
{
    bool ExcluirVideo(AnaliseVideo analiseVideo);
}

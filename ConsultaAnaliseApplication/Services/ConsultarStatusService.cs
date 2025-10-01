using AnaliseVideoRepository.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;

namespace ConsultaAnaliseApplication.Services;

public class ConsultarStatusService(IBuscarAnaliseVideoRepository buscarAnaliseVideoRepo) : IConsultarStatusService
{
    public async Task<ConsultaStatusDto> ConsultarAsync(string id)
    {
        AnaliseVideo? analiseVideo = await buscarAnaliseVideoRepo.BuscarPorIdAsync(id);

        if (analiseVideo == null)
            throw new Exception("Análise não encontrada");

        return new ConsultaStatusDto(analiseVideo.Status);
    }
}

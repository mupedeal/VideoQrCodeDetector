using AnaliseVideoRepository.Interfaces;
using Domain.Converters;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;

namespace ConsultaAnaliseApplication.Services;

public class ConsultarResultadoService(IBuscarAnaliseVideoRepository buscarAnaliseVideoRepo) : IConsultarResultadoService
{
    public async Task<ConsultaResultadoDto> ConsultarAsync(string id)
    {
        AnaliseVideo? analiseVideo = await buscarAnaliseVideoRepo.BuscarPorIdAsync(id);

        if (analiseVideo == null)
            throw new Exception("Análise não encontrada");

        return AnaliseVideoConverter.ParaConsultaResultadoDto(analiseVideo);
    }
}

using Domain.Dtos;
using Domain.Entities;

namespace Domain.Converters;

public static class AnaliseVideoConverter
{
    public static ConsultaResultadoDto ParaConsultaResultadoDto(AnaliseVideo analise)
    {
        var resultados = analise.Resultados
            .Select(r => new ResultadoAnaliseDto(
                qRCode: r.Conteudo,
                ocorrencias: r.Timestamps
                    .Select(t => new ResultadoTimestampDto(t.Inicio, t.Fim, t.Duracao))
                    .ToList()
            ))
            .ToList();

        return new ConsultaResultadoDto(resultados);
    }
}

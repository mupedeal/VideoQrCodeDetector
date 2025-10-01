using AnaliseVideoRepository.Interfaces;
using Domain.Interfaces;

namespace AnaliseVideoApplication.Services;

public class AnalisarVideoService(
    IAtualizarAnaliseVideoRepository atualizarRepo,
    IBuscarAnaliseVideoRepository buscarRepo,
    IQrCodeDetector detector) : IAnalisarVideoService
{
    public async Task AnalisarAsync(string id)
    {
        var analise = await buscarRepo.BuscarPorIdAsync(id)
            ?? throw new Exception($"Análise Vídeo não encontrada no repositório para o id: {id}");

        analise.IniciarProcessamento();

        var resultados = detector.Detectar(analise.CaminhoCompleto);

        analise.AdicionarResultados(resultados);

        await atualizarRepo.AtualizarAsync(analise);
    }
}

using Domain.Entities;

namespace Domain.Interfaces;

public interface IQrCodeDetector
{
    List<ResultadoAnalise> Detectar(string caminhoVideo);
}

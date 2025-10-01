using AnaliseVideoApplication.Utils;
using Domain.Entities;
using Domain.Interfaces;

namespace AnaliseVideoApplication.Services;

public class QrCodeDetectorService : IQrCodeDetector
{
    public List<ResultadoAnalise> Detectar(string caminhoVideo)
            => QrCodeDetector.DetectarQrCodesComTimestamp(caminhoVideo);
}

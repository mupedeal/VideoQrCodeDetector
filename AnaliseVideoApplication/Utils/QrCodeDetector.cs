using Domain.Entities;
using Emgu.CV;
using Emgu.CV.Structure;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using ZXing;

namespace AnaliseVideoApplication.Utils;

public static class QrCodeDetector
{
    public static List<ResultadoAnalise> DetectarQrCodesComTimestamp(string caminhoVideo)
    {
        var agrupados = new Dictionary<string, List<ResultadoTimestamp>>();
        var captura = new VideoCapture(caminhoVideo);
        double fps = captura.Get(Emgu.CV.CvEnum.CapProp.Fps);
        int frameIndex = 0;

        var reader = new BarcodeReaderGeneric
        {
            AutoRotate = true,
            Options = { PossibleFormats = [BarcodeFormat.QR_CODE], TryInverted = true }
        };

        Mat frame = new();
        string? qrCodeAtual = null;
        double? timestampInicio = null;

        while (captura.Read(frame))
        {
            if (frame.IsEmpty)
                break;

            using var imageSharpImage = ConvertMatToImageSharp(frame);
            var resultado = reader.Decode(imageSharpImage);
            string? textoDetectado = resultado?.Text;
            double timestampAtual = frameIndex / fps;

            if (!string.IsNullOrEmpty(textoDetectado))
            {
                if (qrCodeAtual == null)
                {
                    qrCodeAtual = textoDetectado;
                    timestampInicio = timestampAtual;
                }
                else if (textoDetectado != qrCodeAtual)
                {
                    AdicionarTimestamp(agrupados, qrCodeAtual, timestampInicio ?? 0, timestampAtual);
                    qrCodeAtual = textoDetectado;
                    timestampInicio = timestampAtual;
                }
            }
            else if (qrCodeAtual != null)
            {
                AdicionarTimestamp(agrupados, qrCodeAtual, timestampInicio ?? 0, timestampAtual);
                qrCodeAtual = null;
                timestampInicio = null;
            }

            frameIndex++;
        }

        if (qrCodeAtual != null && timestampInicio != null)
        {
            double timestampFinal = frameIndex / fps;
            AdicionarTimestamp(agrupados, qrCodeAtual, timestampInicio.Value, timestampFinal);
        }

        var resultados = agrupados
            .Select(kvp => new ResultadoAnalise(kvp.Key, kvp.Value.First().Inicio, kvp.Value.First().Fim)
            {
                Timestamps = kvp.Value
            })
            .ToList();

        return resultados;
    }

    private static void AdicionarTimestamp(Dictionary<string, List<ResultadoTimestamp>> agrupados, string conteudo, double inicio, double fim)
    {
        if (!agrupados.ContainsKey(conteudo))
            agrupados[conteudo] = new List<ResultadoTimestamp>();

        agrupados[conteudo].Add(new ResultadoTimestamp(inicio, fim));
    }


    private static Image<Rgba32> ConvertMatToImageSharp(Mat mat)
    {
        using var imageEmgu = mat.ToImage<Bgr, byte>();

        var imageSharp = new Image<Rgba32>(imageEmgu.Width, imageEmgu.Height);

        for (int y = 0; y < imageEmgu.Height; y++)
        {
            for (int x = 0; x < imageEmgu.Width; x++)
            {
                var bgr = imageEmgu[y, x];
                imageSharp[x, y] = new Rgba32(ToByte(bgr.Red), ToByte(bgr.Green), ToByte(bgr.Blue), 255);
            }
        }

        return imageSharp;
    }

    private static byte ToByte(double value)
    {
        return (byte)Math.Clamp(value, 0, 255);
    }
}

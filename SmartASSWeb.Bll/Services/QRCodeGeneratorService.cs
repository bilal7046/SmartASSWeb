using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using QRCoder;

namespace SmartASSWeb.Bll.Services
{
    public interface IQRCodeGeneratorService
    {
        Bitmap GetQRCodeImage(string url);
        string GetQRCodeBase64(string url);
    }
    public class QRCodeGeneratorService
        : IQRCodeGeneratorService
    {
        public Bitmap GetQRCodeImage(string url)
        {
            var generator = new PayloadGenerator.Url(url);
            string payload = generator.ToString();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }
        public string GetQRCodeBase64(string url)
        {
            var generator = new PayloadGenerator.Url(url);
            string payload = generator.ToString();

            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            using (var ms = new MemoryStream())
            {
                using (var qr = qrCode.GetGraphic(20))
                {
                    qr.Save(ms, ImageFormat.Png);
                    return "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }
}

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web;

namespace SmartASSWeb.Bll.Services
{
    public interface IImageReSizerService
    {
        Image ResizeImage(HttpPostedFileBase img, Size size);
    }

    public class ImageReSizerService
        : IImageReSizerService
    {
        public Image ResizeImage(HttpPostedFileBase img, Size size)
        {
            var oImage = Image.FromStream(img.InputStream);
            var sWidth = oImage.Width;
            var sHeight = oImage.Height;
            if (size.Width >= oImage.Width && size.Height >= oImage.Height)
                return oImage;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = size.Width / (float) sWidth;

            nPercentH = size.Height / (float) sHeight;

            if (nPercentH < nPercentW)

                nPercent = nPercentH;
            else

                nPercent = nPercentW;

            var dWidth = (int) (sWidth * nPercent);

            var dHeight = (int) (sHeight * nPercent);

            var b = new Bitmap(dWidth, dHeight);

            var g = Graphics.FromImage(b);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(oImage, 0, 0, dWidth, dHeight);
            g.Dispose();
            return b;
        }
    }
}
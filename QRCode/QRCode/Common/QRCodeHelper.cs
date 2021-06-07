using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.QrCode;

namespace QRCode.Common
{
    public static class QRCodeHelper
    {
        public static Bitmap ConvertToBitmap(string text, int width = 360, int height = 360)
        {
            var options = new QrCodeEncodingOptions
            {
                Height = width,
                Width = height
            };

            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;

            return writer.Write(text);
        }
    }
}

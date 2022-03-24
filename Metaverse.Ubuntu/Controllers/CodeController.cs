using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Metaverse.Ubuntu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QrCodeController : Controller
    {
        [HttpGet]
        [Route("generate/{qrText}")]
        public IActionResult GetQrCode(string qrText)
        {
            QRCodeGenerator qrCodeGenerate = new QRCodeGenerator();
            QRCodeData qrCodeData = qrCodeGenerate.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            return File(BitmapToBytes(qrCodeImage), "image/jpeg");

        }

        private byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                return ms.ToArray();
            }
        }
    }
}

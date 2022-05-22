using Microsoft.AspNetCore.Mvc;
using QRCoder;
using QRGenerator.Models;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace QRGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string inputText)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(inputText, QRCodeGenerator.ECCLevel.Q);
                QRCode qRCode = new QRCode(qRCodeData);

                using (Bitmap bitmap = qRCode.GetGraphic(20))
                {
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    ViewBag.QRCode = "data:image/png; base64," + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return View();
        }


    }
}
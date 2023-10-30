using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OCR.Contracts.DTO;
using OCR.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace OCR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PictureController : Controller
    {
        private static readonly ILogger<PictureController> _logger;
        private readonly ITesseractOCRService _tesseractOCRService;

        public PictureController(ITesseractOCRService tesseractOCRService)
        {
            _tesseractOCRService = tesseractOCRService;
        }

        //TEST ONLY
        [HttpGet]
        [Route("get")]
        public async Task<ActionResult> Get([FromBody] Base64ImageDTO imageDTO)
        {
            var response = new OCRResultDTO();
            try
            {
                
                response.ImageText = imageDTO.Image;
                response.ExecTime = 123;
                response.Status = "OK";
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PictureController.ReadImageBase64: " + ex);
            }
            return Json(response);
        }

        [HttpPost]
        [Route("readimagebase64")]
        public async Task<ActionResult> ReadImageBase64([FromBody]Base64ImageDTO imageDTO)
        {
            var response = new OCRResultDTO();
            try
            {
                var imageText = "";
                byte[] bytes = Convert.FromBase64String(imageDTO.Image);
                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }
                Stopwatch sw = Stopwatch.StartNew();
                imageText = await _tesseractOCRService.DoOCRAsync(image);
                sw.Stop();

                response.ImageText = imageText;
                response.ExecTime = sw.ElapsedMilliseconds;
                response.Status = "OK";
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PictureController.ReadImageBase64: " + ex);
            }
            return Json(response);
        }
    }
}

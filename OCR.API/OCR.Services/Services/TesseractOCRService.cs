using OCR.Services.Interfaces;
using System;
using System.Threading.Tasks;
using IronOcr;
using System.Drawing;
using Microsoft.Extensions.Logging;

namespace OCR.Services.Services
{
    public class TesseractOCRService : ITesseractOCRService
    {
        private static readonly ILogger<TesseractOCRService> _logger;
        public async Task<string> DoOCRAsync(Image image)
        {
            string result = "";
            IronTesseract ocr = new IronTesseract();
            try
            {
                using (OcrInput input = new OcrInput())
                {
                    
                    input.Add(image);
                    OcrResult ocrResult = await ocr.ReadAsync(image);
                    result = ocrResult.Text;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error doing IronOCR to image: " + ex);
            }
            return result;
        }
    }
}

using Microsoft.Extensions.Logging;
using OCR.Services.Interfaces;
using OCR.Contracts.DTO;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using OCR.Services.Util;
using System.Text;

namespace OCR.Services.Services
{
    public class OCRService : IOCRService
    {
        private readonly Uri baseAddress = new Uri("http://localhost:5000/api");
        private readonly HttpClient _client;
        private static readonly ILogger<OCRService> _logger;

        public OCRService()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public async Task<OCRResultDTO> ReadImageBase64(Stream imgSrteam)
        {
            var ocrResult = new OCRResultDTO();
            var base64DTO = new Base64ImageDTO();
            try
            {
                base64DTO.Image = FileUtil.ConvertStreamToBase64(imgSrteam);
                StringContent content = new StringContent(JsonConvert.SerializeObject(base64DTO), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/picture/readimagebase64", content);
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    ocrResult = JsonConvert.DeserializeObject<OCRResultDTO>(data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in OCRService.ReadImageBase64: " + ex.Message);
            }
            return ocrResult;
        }
    }
}

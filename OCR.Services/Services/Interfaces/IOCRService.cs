using System.IO;
using System.Threading.Tasks;
using OCR.Contracts.DTO;

namespace OCR.Services.Interfaces
{
    public interface IOCRService
    {
        Task<OCRResultDTO> ReadImageBase64(Stream imgSrteam);
    }
}

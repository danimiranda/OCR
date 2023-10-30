using System.Drawing;
using System.Threading.Tasks;

namespace OCR.Services.Interfaces
{
    public interface ITesseractOCRService
    {
        Task<string> DoOCRAsync(Image image);
    }
}

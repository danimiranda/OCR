using OCR.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OCR.Services.Interfaces
{
    public interface IPictureService
    {
        Task<bool> InsertMultipleAsync(List<PictureModel> pictures); 
    }
}

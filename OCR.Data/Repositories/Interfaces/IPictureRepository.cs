using OCR.Contracts.DTO;
using OCR.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OCR.Data.Repositories.Interfaces
{
    public interface IPictureRepository
    {
        Task<bool> InsertMultipleAsync(List<PictureModel> pictures);
    }
}

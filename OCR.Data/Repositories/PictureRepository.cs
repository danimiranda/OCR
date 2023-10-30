using Microsoft.Extensions.Logging;
using OCR.Contracts.Models;
using OCR.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OCR.Data.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private static readonly ILogger<PictureRepository> _logger;
        private readonly OCRContext _context;

        public PictureRepository(OCRContext context)
        {
            _context = context;
        }
        public async Task<bool> InsertMultipleAsync(List<PictureModel> pictures)
        {
            try
            {
                await _context.Pictures.AddRangeAsync(pictures);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PictureRepository.InsertMultipleAsync : " + ex.Message);
                return false;
            }
        }
    }
}

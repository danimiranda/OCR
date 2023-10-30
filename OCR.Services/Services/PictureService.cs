using Microsoft.Extensions.Logging;
using OCR.Contracts.Models;
using OCR.Data.Repositories.Interfaces;
using OCR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OCR.Services.Services
{
    public class PictureService : IPictureService
    {
        private static readonly ILogger<PictureService> _logger;
        private readonly IPictureRepository _pictureRepository;

        public PictureService(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }

        public async Task<bool> InsertMultipleAsync(List<PictureModel> pictures)
        {
            try
            {
                return await _pictureRepository.InsertMultipleAsync(pictures);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PictureService.InsertMultipleAsync: " + ex.Message);
                return false;
            }
        }
    }
}

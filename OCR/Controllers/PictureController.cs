using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OCR.Contracts.DTO;
using OCR.Contracts.Models;
using OCR.Services.Interfaces;
using System;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OCR.Controllers
{
    public class PictureController : Controller
    {

        private readonly ILogger<PictureController> _logger;
        private IWebHostEnvironment hostEnv;
        private readonly IOCRService _oCRService;
        private readonly IPictureService _pictureService;

        public PictureController(
            ILogger<PictureController> logger,
            IWebHostEnvironment env,
            IOCRService oCRService,
            IPictureService pictureService)
        {
            _logger = logger;
            hostEnv = env;
            _oCRService = oCRService;
            _pictureService = pictureService;
        }
        // GET: PictureController
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Upload(IFormFile file)
        {
            try
            {
                var ocrResults = new List<OCRResultDTO>();
                var picturesEntries = new List<PictureModel>();
                var stream = file.OpenReadStream();
                var zipFile = new ZipArchive(stream);
                //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files");
                foreach (ZipArchiveEntry image in zipFile.Entries)
                {
                    //image.ExtractToFile(Path.Combine(filePath, image.Name));
                    //await _imageService.SendImageRPC(image.Open(), image.Name);

                    var ocrResult =  _oCRService.ReadImageBase64(image.Open()).Result;
                    picturesEntries.Add(new PictureModel
                    {
                        Id = Guid.NewGuid(),
                        FileName = image.Name,
                        ImageText = ocrResult.ImageText,
                        ExecutionTime = ocrResult.ExecTime
                    });

                    //ocrResults.Add(await _oCRService.ReadImageBase64(image.Open()));

                }
                var result = await _pictureService.InsertMultipleAsync(picturesEntries);
                //return Json(picturesEntries);
                ViewBag.Pictures = picturesEntries;

                return View(picturesEntries);
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = "error uploading the zip file : " + ex.Message });
            }
        }
    }
}

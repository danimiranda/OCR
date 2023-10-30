using System;

namespace OCR.Contracts.DTO
{
    public class OCRResultDTO
    {
        public string ImageText { get; set; }
        public string Status { get; set; }
        public long ExecTime { get; set; }
    }
}

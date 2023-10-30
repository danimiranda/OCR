
using System;

namespace OCR.Contracts.Models
{
    public class PictureModel
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string ImageText { get; set; }

        public long ExecutionTime { get; set; }
    }
}

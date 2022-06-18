using SQLite;
using System;

namespace AndroidApp.Models
{
    [Table(nameof(CameraModel))]
    public class CameraModel
        : BaseModel
    {
        public string Source { get; set; }
        public string Category { get; set; }
        public int Count { get; set; }
        public string Title { get; set; }
        public string Provider { get; set; }
        public int Length { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }

        public string Subcategory { get; set; }
        public int Width { get; internal set; }
        public decimal Cost { get; internal set; }
        public decimal Price { get; internal set; }
        public string Phone { get; internal set; }
    }
}

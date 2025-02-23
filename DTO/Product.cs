using System;

namespace DTO
{
    public class Product : BaseDTO
    {
        public new int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public double Price { get; set; } = 0.0;
        public int Stock { get; set; } = 0;
        public string ProductCode { get; set; } = string.Empty;
    }
}

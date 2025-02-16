using System;

namespace DTO
{
    public class Product : BaseDTO
    {
        public required string Name { get; set; }
        public required string Category { get; set; }
        public required double Price { get; set; }
        public required int Stock { get; set; }
        public required string ProductCode { get; set; }
    }
}

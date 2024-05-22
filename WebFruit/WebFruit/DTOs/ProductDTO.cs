﻿namespace WebFruit.DTOs
{
    public class ProductDTO
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? ImageUrl { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryId { get; set; }
    }
}

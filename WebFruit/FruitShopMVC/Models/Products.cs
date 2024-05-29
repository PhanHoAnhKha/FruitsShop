using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FruitShopMVC.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? ImageUrl { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public bool? IsSaling { get; set; }
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Categories? Category { get; set; }
    }
}

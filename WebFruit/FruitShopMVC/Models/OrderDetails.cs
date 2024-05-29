using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FruitShopMVC.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Products? Product { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Orders? Order { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}

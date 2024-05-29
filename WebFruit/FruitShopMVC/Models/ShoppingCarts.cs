using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FruitShopMVC.Models
{
    public class ShoppingCarts
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Products? Product { get; set; }
        public int Qty { get; set; }
        public string? ShoppingCartId { get; set; }
    }
}

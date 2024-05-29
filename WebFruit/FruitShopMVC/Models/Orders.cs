using System.ComponentModel.DataAnnotations;

namespace FruitShopMVC.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int? Phone { get; set; }
        public int? OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public List<OrderDetails>? OrderDetails { get; set; }
    }
}

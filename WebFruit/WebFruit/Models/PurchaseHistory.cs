namespace WebFruit.Models
{
    public class PurchaseHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}

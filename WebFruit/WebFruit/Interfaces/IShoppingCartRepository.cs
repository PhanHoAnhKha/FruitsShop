using WebFruit.Models;

namespace WebFruit.Interfaces
{
    public interface IShoppingCartRepository
    {
        void AddToCart(Product product);
        int RemoveFromCart(Product product);
        List<ShoppingCart> GetAllShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
        public List<ShoppingCart> ShoppingCartItems { get; set; }
    }
}

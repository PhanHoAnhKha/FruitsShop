using WebFruit.Interfaces;
using WebFruit.Models;
using WebFruit.DTOs;
using WebFruit.Data;
using Microsoft.EntityFrameworkCore;

namespace WebFruit.Services
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private FruitDbContext dbContext;
        public ShoppingCartRepository(FruitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<ShoppingCart>? ShoppingCartItems { get; set; }
        public string? ShoppingCartId { set; get; }
        public static ShoppingCartRepository GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            FruitDbContext context = services.GetService<FruitDbContext>() ?? throw new Exception("Error initializing PetShopDbContext");
            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString(); session?.SetString("CartId", cartId);
            return new ShoppingCartRepository(context) { ShoppingCartId = cartId };
        }
        public void AddToCart(Product product)
        {
            var shoppingCartItem = dbContext.ShoppingCarts.FirstOrDefault(s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCart
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Qty = 1,
                };
                dbContext.ShoppingCarts.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Qty++;
            }
            dbContext.SaveChanges();
        }
        public void ClearCart()
        {
            var cartItems = dbContext.ShoppingCarts.Where(s => s.ShoppingCartId == ShoppingCartId);
            dbContext.ShoppingCarts.RemoveRange(cartItems);
            dbContext.SaveChanges();
        }
        public List<ShoppingCart> GetAllShoppingCartItems()
        {
            return ShoppingCartItems ??= dbContext.ShoppingCarts.Where(s => s.ShoppingCartId == ShoppingCartId).Include(p => p.Product).ToList();
        }
        public decimal GetShoppingCartTotal()
        {
            var totalCost = dbContext.ShoppingCarts.Where(s => s.ShoppingCartId == ShoppingCartId).Select(s => s.Product.Price * s.Qty).Sum();
            return (decimal)totalCost;
        }
        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem = dbContext.ShoppingCarts.FirstOrDefault(s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);
            var quantity = 0;
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Qty > 1)
                {
                    shoppingCartItem.Qty--;
                    quantity = shoppingCartItem.Qty;
                }
                else
                {
                    dbContext.ShoppingCarts.Remove(shoppingCartItem);
                }
            }
            dbContext.SaveChanges();
            return quantity;
        }
    }
}

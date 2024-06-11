using FruitShopMVC.Models;
using FruitShopMVC.DTOs;
namespace FruitShopMVC.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Products> Products { get; set; }
        public Products Product { get; set; }
        public IEnumerable<News> News { get; set; }
        public News New { get; set; }
        public RegisterRequestDTO Register { get; set; }
    }

    public class BlogVM
    {
        public IEnumerable<News> News { get; set; }
        public News New { set; get; }
    }
    public class ContactSubscriberViewModel
    {
        public int TotalRevenue { get; set; }
        public int TotalUsers { get; set; }
        public List<Contacts> Contacts { get; set; }
        public List<EmailSubscriptions> Subscribers { get; set; }
        public int TotalQuantityInOrderDetails { get; set; }
    }
}

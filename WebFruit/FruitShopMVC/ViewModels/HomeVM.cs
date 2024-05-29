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
}

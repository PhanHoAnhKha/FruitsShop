using System.ComponentModel.DataAnnotations;

namespace FruitShopMVC.Models
{
    public class News
    {
        [Key]
        public int NewId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? ContentDetail { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? Author { get; set; }
        public bool IsHot { get; set; }
        public List<Comments> Comments { get; set; }
    }
}

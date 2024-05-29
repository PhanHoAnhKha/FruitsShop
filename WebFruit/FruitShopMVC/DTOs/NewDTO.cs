namespace FruitShopMVC.DTOs
{
    public class NewDTO
    {
        public string? ImageUrl { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ContentDetail { get; set; }
        public string? Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsHot { get; set; }
    }
}

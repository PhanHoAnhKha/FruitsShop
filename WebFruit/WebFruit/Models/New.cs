using System.ComponentModel.DataAnnotations;

namespace WebFruit.Models
{
    public class New
    {
        [Key]
        public int NewId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set;}
        public string? Author { get; set; }
        public List<Comment> Comments { get; set; }
       
    }
}

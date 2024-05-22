using System.ComponentModel.DataAnnotations;

namespace WebFruit.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Email { get; set; }
        public DateTime? Created { get; set; }
        public string? Message { get; set; }
        public int NewId { get; set; }
        public New? New { get; set; }
    }
}

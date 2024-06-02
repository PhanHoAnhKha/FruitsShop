using System.ComponentModel.DataAnnotations;

namespace WebFruit.DTOs
{
    public class CommentDTO
    {
        [Required(ErrorMessage = "The Name field is required.")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "The Message field is required.")]
        public string? Message { get; set; }
        [Required(ErrorMessage = "The Email field is required.")]
        public string? Email { get; set; }
        public DateTime Created { get; set; }
    }
}

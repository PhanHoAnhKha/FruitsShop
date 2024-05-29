using System.ComponentModel.DataAnnotations;

namespace FruitShopMVC.DTOs
{
    public class ContactDTO
    {
        [Required(ErrorMessage = "The Name field is required.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "The Message field is required.")]
        public string? Message { get; set; }
    }
}

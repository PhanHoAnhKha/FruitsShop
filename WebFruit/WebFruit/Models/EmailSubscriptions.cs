using System.ComponentModel.DataAnnotations;

namespace WebFruit.Models
{
    public class EmailSubscriptions
    {
        [Key]
        public int Id { get; set; }
        public string? Email { get; set; }
    }
}

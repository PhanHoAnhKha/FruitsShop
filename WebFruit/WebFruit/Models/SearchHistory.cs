namespace WebFruit.Models
{
    public class SearchHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SearchTerm { get; set; }
        public DateTime SearchDate { get; set; }
    }
}

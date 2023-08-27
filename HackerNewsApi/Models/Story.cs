namespace HackerNewsApi.Models
{
    public class Story
    {
        public string? Title { get; set; }
        public string? PostedBy { get; set; }
        public int Score { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string? Url { get; set; }
    }
}

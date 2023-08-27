namespace HackerNewsApi.Dto
{
    public class StoryDto
    {
        public string? Title { get; set; }
        public string? PostedBy { get; set; }
        public int Score { get; set; }
        public int DateTime { get; set; }
        public string? Url { get; set; }
    }
}

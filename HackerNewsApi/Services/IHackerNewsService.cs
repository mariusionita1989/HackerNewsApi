using HackerNewsApi.Models;

namespace HackerNewsApi.Services
{
    public interface IHackerNewsService
    {
        Task<Story?> GetStoryAsync(int storyId);
        Task<List<int>?> GetBestStoryIdsAsync();
        Task<List<Story>> GetStoriesAsync(List<int> storyIds);
    }
}

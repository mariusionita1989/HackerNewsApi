using HackerNewsApi.Config;
using HackerNewsApi.Dto;
using HackerNewsApi.Mapper;
using HackerNewsApi.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HackerNewsApi.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _apiConfiguration;

        public HackerNewsService(IHttpClientFactory httpClientFactory, IOptions<List<ApiConfiguration>> options)
        {
            _httpClient = httpClientFactory.CreateClient();
            _apiConfiguration = options.Value.FirstOrDefault() ?? throw new ArgumentException("API configuration not provided.");
        }

        public async Task<Story?> GetStoryAsync(int storyId)
        {
            var url = _apiConfiguration.StoryBaseUrl?.Replace("{id}", storyId.ToString());
            if (string.IsNullOrEmpty(url))
                return null;

            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<Story?>(response);
        }

        public async Task<List<int>?> GetBestStoryIdsAsync()
        {
            var response = await _httpClient.GetStringAsync(_apiConfiguration.BestStoriesUrl);
            return JsonConvert.DeserializeObject<List<int>?>(response);
        }

        public async Task<List<StoryDto>> GetStoriesAsync(List<int> storyIds)
        {
            List<StoryDto> result = new List<StoryDto>();
            var tasks = storyIds.Select(GetStoryAsync);
            var stories = await Task.WhenAll(tasks);
            var list = stories.Where(story => story != null).ToList();
            if(list!= null)
                result = StoryToStoryDto.ConvertToStoryDtos(list);
            return result.OrderBy(t=>t.DateTime);
        }
    }
}

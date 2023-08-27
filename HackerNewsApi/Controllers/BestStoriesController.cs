using HackerNewsApi.Config;
using HackerNewsApi.Dto;
using HackerNewsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace HackerNewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BestStoriesController : ControllerBase
    {
        private readonly IOptions<List<ApiConfiguration>> _options;
        private readonly IHackerNewsService _hackerNewsService;
        private readonly IMemoryCache _memoryCache;

        public BestStoriesController(IHackerNewsService hackerNewsService, IOptions<List<ApiConfiguration>> options, IMemoryCache memoryCache)
        {
            _options = options;
            _hackerNewsService = hackerNewsService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetBestStories()   // gets a list of best stories
        {
            try
            {
                if (!_memoryCache.TryGetValue("BestStoriesList", out List<StoryDto>? storiesList))
                {
                    var bestStoryIds = await _hackerNewsService.GetBestStoryIdsAsync();
                    if (bestStoryIds != null)
                        storiesList = await _hackerNewsService.GetStoriesAsync(bestStoryIds);
                    var cacheOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) // add 1 minute time to live
                    };
                    _memoryCache.Set("BestStoriesList", storiesList, cacheOptions);
                }

                return Ok(storiesList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}

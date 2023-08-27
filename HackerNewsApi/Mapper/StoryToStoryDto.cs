using HackerNewsApi.Dto;
using HackerNewsApi.Models;

namespace HackerNewsApi.Mapper
{
    public static class StoryToStoryDto
    {
        public static List<StoryDto> ConvertToStoryDtos(List<Story?>? stories)
        {
            List<StoryDto> storyDtos = new List<StoryDto>();
            if (stories != null) 
            {
                foreach (var story in stories)
                {
                    if (story != null) 
                    {
                        StoryDto storyDto = new StoryDto
                        {
                            Title = story.Title,
                            PostedBy = story.By,
                            Score = story.Score,
                            DateTime = story.Time,
                            Url = story.Url
                        };

                        storyDtos.Add(storyDto);
                    }
                }
            }
            
            return storyDtos;
        }
    }
}

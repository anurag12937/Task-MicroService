using CodingChallenge.Core.Models;

namespace CodingChallenge.Core.ThirdPartyAPIService
{
    public interface IThirdPartyAPI
    {
        /// <summary>
        /// Get all stories ides
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<int>> GetAllStoriesIds();
    
        Task<StoryDetailDto> GetStorieDetail(int StoryId);
    }
}

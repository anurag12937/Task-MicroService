using CodingChallenge.Core.Models;

namespace CodingChallenge.Core.Services.StoriesServices.Interface
{
    /// <summary>
    /// IStoriesService Interface
    /// </summary>
    public interface IStoriesService
  {

        /// <summary>
        /// Get All Stories service
        /// </summary>
        /// <param name="pagingParameters">see cref="PagingParameters"</param>
        /// <returns> StoriesDetailsDto </returns>
        Task<APIResponse<StoriesDetailsDto>> GetTopAllStories(PagingParameters pagingParameters);
        /// <summary>
        /// Get stories list that are mached by search title
        /// </summary>
        /// <param name="searchtext">searchStoryTitle</param>
        /// <returns></returns>
        Task<APIResponse<StoriesDetailsDto>> GetSearchStories(String searchtext);
    }
}

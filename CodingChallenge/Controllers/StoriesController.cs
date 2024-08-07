using CodingChallenge.Core.Models;
using CodingChallenge.Core.Services.StoriesServices.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodingChallenge.Controllers
{
    /// <summary>
    /// Api controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly IStoriesService _storiesService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="storiesService"></param>
        public StoriesController(IStoriesService storiesService)
        {
            _storiesService = storiesService;
        }
        /// <summary>
        /// Get Paging size and page number stories
        /// </summary>
        /// <param name="pagingParameters"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllStories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllStories([FromQuery] PagingParameters pagingParameters)
        {
            return Ok(await _storiesService.GetTopAllStories(pagingParameters));
        }
        /// <summary>
        /// Get all searched stories 
        /// </summary>
        /// <param name="searchValue"> search value</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSearchStories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSearchStories([FromQuery] string searchValue)
        {
            return Ok(await _storiesService.GetSearchStories(searchValue));
        }

    }
}

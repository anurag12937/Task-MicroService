using AutoMapper;
using CodingChallenge.Core.Models;
using CodingChallenge.Core.ThirdPartyAPIService;
using Newtonsoft.Json;

namespace CodingChallenge.Core.Services.ThirdPartyAPIService
{
    public class ThirdPartyAPI : IThirdPartyAPI
    {
        /// <summary>
        /// http client
        /// </summary>
        private readonly HttpClient _httpClient;
        /// <summary>
        /// the mapper 
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThirdPartyAPI"/> class.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="mapper"></param>
        public ThirdPartyAPI(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all stories
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<int>> GetAllStoriesIds()
        {
            IEnumerable<int> storiesIDes=null;
            var urlFirstAPI = string.Format("topstories.json?print=pretty");
            var response = await _httpClient.GetAsync(urlFirstAPI);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = response.Content.ReadAsStringAsync().Result;
                storiesIDes = JsonConvert.DeserializeObject<IEnumerable<int>>(stringResponse);
            }
            return storiesIDes;

        }
        /// <summary>
        /// get story details
        /// </summary>
        /// <param name="StoryId">StoryId</param>
        /// <returns></returns>
        public async Task<StoryDetailDto> GetStorieDetail(int StoryId)
        {
            StoryDetailDto StoryDetailDto = null;
            var urlSecondAPI = string.Format("item/" + StoryId + ".json?print=pretty");
            var stringResponseSecondAPI = await _httpClient.GetAsync(urlSecondAPI);
            if (stringResponseSecondAPI.IsSuccessStatusCode)
            {
                StoryDetail storiesDetails = JsonConvert.DeserializeObject<StoryDetail>(stringResponseSecondAPI.Content.ReadAsStringAsync().Result);
                if (storiesDetails != null)
                {
                    StoryDetailDto = _mapper.Map<StoryDetailDto>(storiesDetails);
                }
            }
           return StoryDetailDto;
        }
    }
}

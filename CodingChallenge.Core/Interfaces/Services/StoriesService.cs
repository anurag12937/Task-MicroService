using AutoMapper;
using CodingChallenge.Core.Cache;
using CodingChallenge.Core.Builders;
using CodingChallenge.Core.Models;
using CodingChallenge.Core.Services.StoriesServices.Interface;
using CodingChallenge.Core.ThirdPartyAPIService;
using static CacheResultModel;
using System.Diagnostics;

namespace CodingChallenge.Core.Services.StoriesServices.Service
{
    /// <summary>
    /// add class contain get stories related data functions
    /// </summary>
    public class StoriesService : IStoriesService
    {
        /// <summary>
        /// The http client 
        /// </summary>
        private readonly HttpClient _httpClient;

        private readonly IMapper _mapper;
        private readonly IResponseBuilderFactory _responseBuilderFactory;
        /// <summary>
        /// The caching service
        /// </summary>
        private readonly ICacheService _cacheService;
        /// <summary>
        /// The third party api
        /// </summary>
        private readonly IThirdPartyAPI _thirdPartyAPI;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoriesService"/> class.
        /// </summary>
        /// <param name="responseBuilderFactory">responseBuilderFactory</param>
        /// <param name="httpclient">httpclient</param>
        /// <param name="mapper">mapper</param>
        /// <param name="cachingService">cachingService</param>
        /// <param name="thirdPartyAPI">thirdPartyAPI</param>
        public StoriesService(IResponseBuilderFactory responseBuilderFactory, HttpClient httpclient, IMapper mapper, ICacheService cachingService, IThirdPartyAPI thirdPartyAPI)
        {
            _httpClient = httpclient;
            _responseBuilderFactory = responseBuilderFactory;
            _mapper = mapper;
            _cacheService = cachingService;
            _thirdPartyAPI = thirdPartyAPI;
        }

        /// <summary>
        /// Get stories list that are mached by search title
        /// </summary>
        /// <param name="searchStoryTitle">searchStoryTitle</param>
        /// <returns></returns>
        public async Task<APIResponse<StoriesDetailsDto>> GetSearchStories(string searchStoryTitle)
        {
            StoriesDetailsDto storiesDetailsDto = new StoriesDetailsDto();
            var responseBuilder = _responseBuilderFactory.GetBuilder<StoriesDetailsDto>();
            try
            {
                var data = await GetDataForStoriesDetails();
                if (!string.IsNullOrEmpty(searchStoryTitle))
                {
                    storiesDetailsDto.Stories = data.Where(e => e.title.ToLower().Contains(searchStoryTitle.ToLower()));
                    storiesDetailsDto.TotalRecords = storiesDetailsDto.Stories.Count();
                }
                return responseBuilder.AddSuccessData(storiesDetailsDto).Build();
            }
            catch (Exception ex)
            {
               return responseBuilder.AddError(ex.Message).Build();
            }
        }

        /// <summary>
        /// Get All Stories service
        /// </summary>
        /// <param name="pagingParameters">see cref="PagingParameters"</param>
        /// <returns> StoriesDetailsDto </returns>
        public async Task<APIResponse<StoriesDetailsDto>> GetTopAllStories(PagingParameters pagingParameters)
        {
            var responseBuilder = _responseBuilderFactory.GetBuilder<StoriesDetailsDto>();
            StoriesDetailsDto storiesDetailsDto = new StoriesDetailsDto();
            try
            {
                Stopwatch stopw = new Stopwatch();
                stopw.Start();
                var data = await GetDataForStoriesDetails();
                storiesDetailsDto.Stories = data.OrderByDescending(on => on.id).Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize).Take(pagingParameters.PageSize).ToList();
                storiesDetailsDto.TotalRecords = data.Count();
                stopw.Stop();
                var x = stopw.ElapsedMilliseconds;
                return responseBuilder.AddSuccessData(storiesDetailsDto).Build();
            }
            catch (Exception ex)
            {
               return responseBuilder.AddError(ex.Message).Build();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryDetailDto>> GetDataForStoriesDetails()
        {
            List<StoryDetailDto> storiesDetailstList = new List<StoryDetailDto>();
            IEnumerable<StoryDetailDto> enumerableCollection = null;
            var responseBuilder = _responseBuilderFactory.GetBuilder<StoriesDetailsDto>();
            try
            {
                // Check caching
                var cacheRecords = await _cacheService.RetrieveFromCacheAsync("AllStoriesDataKey");
                if (cacheRecords.CacheStatus == CacheStatusOption.DoesNotExists)
                {
                    // Call First APi
                    var storiesIDes = (await _thirdPartyAPI.GetAllStoriesIds()).Take(220);
                    var tasks = storiesIDes.Select(async storyId =>
                    {
                        storiesDetailstList.Add(await _thirdPartyAPI.GetStorieDetail(storyId));
                    });
                    await Task.WhenAll(tasks);
                    enumerableCollection = storiesDetailstList
                        .Where(x => x.url != null && x.url != "" && x.type == "story")
                        .Take(200).OrderByDescending(x => x.id);
                    await _cacheService.SaveToCacheAsync("AllStoriesDataKey", enumerableCollection);
                    return enumerableCollection;
                }
                enumerableCollection = _mapper.Map<IEnumerable<StoryDetailDto>>(cacheRecords.CacheValue);
                return enumerableCollection;
            }
            catch (Exception ex)
            {
                return (IEnumerable<StoryDetailDto>)responseBuilder.AddError(ex.Message).Build();
            }
            
        }

    }
}

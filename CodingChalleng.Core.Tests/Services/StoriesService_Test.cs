using AutoMapper;
using CodingChallenge.Core.Cache;
using CodingChallenge.Core.Models;
using CodingChallenge.Core.ThirdPartyAPIService;
using FluentAssertions;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodingChalleng.Core.Tests.Services;
using CodingChallenge.Core.Services.StoriesServices.Service;

namespace CodingChallenge.Core.Tests.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class StoriesServiceTest
    {
        /// <summary>
        /// TheGetALLStoriesList
        /// </summary>
        public class xTheGetALLStoriesList
        {
            /// <summary>
            /// Should the return Get All Stories List as response from ThirdPartyAPI.
            /// </summary>
            [Fact]
            public async Task Should_Return_Get_All_Stories_Data_From_ThirdParty_API()
            {
                //Given
                var cachingService = Substitute.For<ICacheService>();
                cachingService.RetrieveFromCacheAsync(Arg.Any<string>()).Returns(new CacheResultModel("AllStoriesDataKey") { CacheStatus = CacheResultModel.CacheStatusOption.DoesNotExists, CacheValue = new List<StoryDetailDto>() { new StoryDetailDto() { id = 11, title = "The unique algorithm behind rewind in Braid", type = "story", url = "https://twitter.com/jonathan_blow/status/1770277363848552734" } } });

                var mapper = Substitute.For<IMapper>();
                mapper.Map<IEnumerable<StoryDetailDto>>(Arg.Any<IEnumerable<StoryDetailDto>>()).Returns(new List<StoryDetailDto>() { new StoryDetailDto() { id = 11, title = "The unique algorithm behind rewind in Braid", type = "story", url = "https://twitter.com/jonathan_blow/status/1770277363848552734" } });



                StoriesService sut = new StoriesServiceFixture().WithCachingService(cachingService).WithMapper(mapper);
                //When
                var result = (await sut.GetTopAllStories(new PagingParameters() { PageNumber = 1, PageSize = 1 })).Data.Stories;
                //Then
                result.Should().HaveCount(1);
            }


            /// <summary>
            /// Should the return Get All Stories List as response from Cache.
            /// </summary>
            [Fact]
            public async Task Should_Return_Get_All_Stories_Data_From_Cache()
            {
                //Given

                StoriesService sut = new StoriesServiceFixture();

                //When
                var result = (await sut.GetTopAllStories(new PagingParameters() { PageNumber = 1, PageSize = 1 })).Data.Stories;

                //Then
                result.Should().HaveCount(1);
            }


            /// <summary>
            /// Should the return null if story type is not as story When get data from Third Party API
            /// </summary>
            [Fact]
            public async Task Should_Not_Return_Stories_Data_If_StoryType_Not_Story()
            {
                //Given
                var cachingService = Substitute.For<ICacheService>();
                cachingService.RetrieveFromCacheAsync(Arg.Any<string>()).Returns(new CacheResultModel("AllStoriesDataKey") { CacheStatus = CacheResultModel.CacheStatusOption.DoesNotExists, CacheValue = new List<StoryDetailDto>() { new StoryDetailDto() { id = 11, title = "The unique algorithm behind rewind in Braid", type = "story", url = "https://twitter.com/jonathan_blow/status/1770277363848552734" } } });

                var thirdPartyAPI = Substitute.For<IThirdPartyAPI>();
                thirdPartyAPI.GetStorieDetail(Arg.Any<int>()).Returns(new StoryDetailDto() { id = 39773074, title = "Fujitsu spilled private client data,passwords into the open unnoticed for a year", type = "story1", url = "https://www.thestack.technology/fujitsu-breach-cloud-buckets/" });

                StoriesService sut = new StoriesServiceFixture().WithThirdPartyAPIService(thirdPartyAPI).WithCachingService(cachingService);
                //When
                var result = (await sut.GetTopAllStories(new PagingParameters() { PageNumber = 1, PageSize = 1 })).Data.Stories;
                //Then
                result.Should().HaveCount(0);
            }
        }

        public class ThrGetSearchStories
        {
            /// <summary>
            /// Should the return Get All searched Stories List  from cache If Parameter Provided & founded.
            /// </summary>
            [Theory]
            [InlineData("unique algorithm")]
            public async Task Should_Return_Get_searched_Stories_Data_If_Param_Provided(string searchStoryTitle)
            {
                //Given

                StoriesService sut = new StoriesServiceFixture();
                //When
                var result = (await sut.GetSearchStories(searchStoryTitle)).Data.Stories;
                //Then
                result.Should().HaveCount(1);
            }


            /// <summary>
            /// Should the return  searched Stories records 0 If Parameter not Provided & founded.
            /// </summary>
            [Theory]
            [InlineData("")]
            [InlineData(null)]
            public async Task Should_Return_Searched_Stories_Data_Count_If_Param_Not_Provided(string searchStoryTitle)
            {
                //Given

                StoriesService sut = new StoriesServiceFixture();
                //When
                var result = (await sut.GetSearchStories(searchStoryTitle)).Data.TotalRecords;
                //Then
                result.Should().Be(0);
            }

            /// <summary>
            /// Should the return  searched Stories records 0 If Parameter Provided but title not fount
            /// </summary>
            [Theory]
            [InlineData("wew23232")]
            public async Task Should_Return_Searched_Stories_Data_Count_If_SearchTitle_Not_Founded(string searchStoryTitle)
            {
                //Given

                StoriesService sut = new StoriesServiceFixture();
                //When
                var result = (await sut.GetSearchStories(searchStoryTitle)).Data.TotalRecords;
                //Then
                result.Should().Be(0);
            }

        }
    }

}

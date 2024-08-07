using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CodingChallenge.Core.Models;
using CodingChallenge.Core.Services.ThirdPartyAPIService;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace CodingChallenge.API.Tests.Controllers
{
    public class ThirdPartyAPITests
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ThirdPartyAPI _thirdPartyAPI;

        public ThirdPartyAPITests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new System.Uri("https://example.com/api/") // Set a base address for the HttpClient
            };
            _mapperMock = new Mock<IMapper>();

            _thirdPartyAPI = new ThirdPartyAPI(_httpClient, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllStoriesIds_ReturnsStoryIds_WhenResponseIsSuccessful()
        {
            // Arrange
            var expectedStoryIds = new List<int> { 1, 2, 3 };
            var responseContent = JsonConvert.SerializeObject(expectedStoryIds);

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent)
                });

            // Act
            var result = await _thirdPartyAPI.GetAllStoriesIds();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedStoryIds.Count, result.Count());
            Assert.Equal(expectedStoryIds, result);
        }

        [Fact]
        public async Task GetStorieDetail_ReturnsStoryDetailDto_WhenResponseIsSuccessful()
        {
            // Arrange
            var storyId = 1;
            var storyDetail = new StoryDetail();
            var storyDetailDto = new StoryDetailDto();

            var responseContent = JsonConvert.SerializeObject(storyDetail);

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent)
                });

            _mapperMock.Setup(m => m.Map<StoryDetailDto>(It.IsAny<StoryDetail>()))
                .Returns(storyDetailDto);

            // Act
            var result = await _thirdPartyAPI.GetStorieDetail(storyId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(storyDetailDto.id, result.id);
            Assert.Equal(storyDetailDto.title, result.title);
        }

        [Fact]
        public async Task GetStorieDetail_ReturnsNull_WhenResponseIsUnsuccessful()
        {
            // Arrange
            var storyId = 1;

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            // Act
            var result = await _thirdPartyAPI.GetStorieDetail(storyId);

            // Assert
            Assert.Null(result);
        }
    }
}

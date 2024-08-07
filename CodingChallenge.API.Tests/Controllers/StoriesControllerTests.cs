using CodingChallenge.Controllers;
using CodingChallenge.Core.Models;
using CodingChallenge.Core.Services.StoriesServices.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace CodingChallenge.API.Tests.Controllers
{
    public class StoriesControllerTests
    {
        private readonly Mock<IStoriesService> _mockStoriesService;
        private readonly StoriesController _controller;

        public StoriesControllerTests()
        {
            _mockStoriesService = new Mock<IStoriesService>();
            _controller = new StoriesController(_mockStoriesService.Object);
        }

        [Fact]
        public async Task GetAllStories_ShouldReturnOkResult_WithData_WhenServiceReturnsSuccess()
        {
            // Arrange
            var pagingParameters = new PagingParameters { PageNumber = 1, PageSize = 10 };
            var apiResponse = new APIResponse<StoriesDetailsDto>
            {
                Success = true,
                Data = new StoriesDetailsDto
                {
                    Stories = new List<StoryDetailDto>
                    {
                        new StoryDetailDto { id =  1, title =  "Test Story" }
                    },
                    TotalRecords = 1
                }
            };

            _mockStoriesService.Setup(service => service.GetTopAllStories(pagingParameters))
                .ReturnsAsync(apiResponse);

            // Act
            var result = await _controller.GetAllStories(pagingParameters) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(apiResponse);
        }

        [Fact]
        public async Task GetAllStories_ShouldReturnInternalServerError_WhenServiceReturnsError()
        {
            // Arrange
            var pagingParameters = new PagingParameters { PageNumber = 1, PageSize = 10 };
            var apiResponse = new APIResponse<StoriesDetailsDto>
            {
                Success = false,
                ErrorMessage = "An error occurred"
            };

            _mockStoriesService.Setup(service => service.GetTopAllStories(pagingParameters))
                .ReturnsAsync(apiResponse);

            // Act
            var result = await _controller.GetAllStories(pagingParameters) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().BeEquivalentTo(apiResponse);
        }

        [Fact]
        public async Task GetSearchStories_ShouldReturnOkResult_WithData_WhenServiceReturnsSuccess()
        {
            // Arrange
            var searchValue = "test";
            var apiResponse = new APIResponse<StoriesDetailsDto>
            {
                Success = true,
                Data = new StoriesDetailsDto
                {
                    Stories = new List<StoryDetailDto>
                    {
                        new StoryDetailDto { id = 1, title = "Test Story" }
                    },
                    TotalRecords = 1
                }
            };

            _mockStoriesService.Setup(service => service.GetSearchStories(searchValue))
                .ReturnsAsync(apiResponse);

            // Act
            var result = await _controller.GetSearchStories(searchValue) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(apiResponse);
        }

        [Fact]
        public async Task GetSearchStories_ShouldReturnInternalServerError_WhenServiceReturnsError()
        {
            // Arrange
            var searchValue = "test";
            var apiResponse = new APIResponse<StoriesDetailsDto>
            {
                Success = false,
                ErrorMessage = "An error occurred"
            };

            _mockStoriesService.Setup(service => service.GetSearchStories(searchValue))
                .ReturnsAsync(apiResponse);

            // Act
            var result = await _controller.GetSearchStories(searchValue) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().BeEquivalentTo(apiResponse);
        }
    }
}

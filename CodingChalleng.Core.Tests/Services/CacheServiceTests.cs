using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CodingChallenge.Core.Cache.Tests.Service
{
    public class CacheServiceTests
    {
        private readonly Mock<IMemoryCache> _mockMemoryCache;
        private readonly CacheService _cacheService;

        public CacheServiceTests()
        {
            _mockMemoryCache = new Mock<IMemoryCache>();
            _cacheService = new CacheService(_mockMemoryCache.Object);
        }

        [Fact]
        public async Task SaveToCacheAsync_ShouldHandleException_WhenErrorOccurs()
        {
            // Arrange
            var key = "testKey";
            var objectToCache = "testValue";
            _mockMemoryCache.Setup(cache => cache.TryGetValue(key, out It.Ref<object>.IsAny)).Throws(new Exception("Test exception"));

            // Act
            var result = await _cacheService.SaveToCacheAsync(key, objectToCache);

            // Assert
            Assert.Equal(CacheResultModel.CacheStatusOption.Error, result.CacheStatus);
            Assert.NotNull(result.Error);
            Assert.Equal("Test exception", result.Error.Message);
        }
    }
}

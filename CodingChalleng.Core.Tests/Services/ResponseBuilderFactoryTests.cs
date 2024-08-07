using CodingChallenge.Core.Builders;
using CodingChallenge.Core.Builders.Factory;
using Xunit;
using FluentAssertions;

namespace CodingChallenge.Core.Tests.Services
{
    public class ResponseBuilderFactoryTests
    {
        private readonly ResponseBuilderFactory _factory;

        public ResponseBuilderFactoryTests()
        {
            _factory = new ResponseBuilderFactory();
        }

        [Fact]
        public void GetBuilder_ShouldReturnResponseBuilder_WhenCalledWithGenericType()
        {
            // Act
            var builder = _factory.GetBuilder<string>();

            // Assert
            builder.Should().NotBeNull();
            builder.Should().BeOfType<ResponseBuilder<string>>();
        }

        [Fact]
        public void GetBuilder_ShouldReturnDifferentInstances_WhenCalledMultipleTimesWithSameType()
        {
            // Act
            var builder1 = _factory.GetBuilder<int>();
            var builder2 = _factory.GetBuilder<int>();

            // Assert
            builder1.Should().NotBeNull();
            builder2.Should().NotBeNull();
            builder1.Should().NotBeSameAs(builder2);
            builder1.Should().BeOfType<ResponseBuilder<int>>();
            builder2.Should().BeOfType<ResponseBuilder<int>>();
        }

        [Fact]
        public void GetBuilder_ShouldReturnTypeSpecificBuilder_WhenCalledWithDifferentTypes()
        {
            // Act
            var stringBuilder = _factory.GetBuilder<string>();
            var intBuilder = _factory.GetBuilder<int>();

            // Assert
            stringBuilder.Should().NotBeNull();
            intBuilder.Should().NotBeNull();
            stringBuilder.Should().BeOfType<ResponseBuilder<string>>();
            intBuilder.Should().BeOfType<ResponseBuilder<int>>();
        }
    }
}
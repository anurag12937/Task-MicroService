using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using CodingChallenge.Core.Models;

namespace CodingChallenge.Configurations
{
    [ExcludeFromCodeCoverage]
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            CreateMap<StoryDetail, StoryDetailDto>().ReverseMap();
        }
    }
}

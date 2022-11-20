using AutoMapper;
using Microsoft.Extensions.Options;

namespace NZWalk.API.Models.Profiles
{
    public class RegionProfile : Profile
    {
        public RegionProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>();
        }
    }
}

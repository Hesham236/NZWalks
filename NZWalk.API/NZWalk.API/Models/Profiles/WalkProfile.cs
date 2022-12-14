using AutoMapper;

namespace NZWalk.API.Models.Profiles
{
    public class WalkProfile : Profile
    {
        public WalkProfile() 
        {
            CreateMap<Models.Domain.Walk, Models.DTO.Walk>()
                .ReverseMap();

            CreateMap<Models.Domain.Walk, Models.DTO.AddWalkRequest>()
                .ReverseMap();
        }
    }
}

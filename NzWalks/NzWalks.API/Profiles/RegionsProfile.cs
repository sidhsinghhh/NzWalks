using AutoMapper;

namespace NzWalks.API.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>().ReverseMap();
            CreateMap<Models.Domain.Region, Models.DTO.RegionRequestBody>().ReverseMap();
        }
    }
}

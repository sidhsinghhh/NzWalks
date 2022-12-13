using AutoMapper;

namespace NzWalks.API.Profiles
{
    public class WalksProfile: Profile
    {
        public WalksProfile()
        {
            CreateMap<Models.Domain.Walk, Models.DTO.Walks>().ReverseMap();
            CreateMap<Models.Domain.Walk, Models.DTO.WalkRequestBody>().ReverseMap();
        }
    }
}

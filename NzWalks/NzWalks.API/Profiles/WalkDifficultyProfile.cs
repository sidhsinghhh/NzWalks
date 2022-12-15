using AutoMapper;

namespace NzWalks.API.Profiles
{
    public class WalkDifficultyProfile: Profile
    {
        public WalkDifficultyProfile()
        {
            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficultyDTO>().ReverseMap();
            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficultyRequestBody>().ReverseMap();
        }
    }
}

using NzWalks.API.Models.Domain;

namespace NzWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();
        Task<WalkDifficulty> GetWalkDifficultyAsync(Guid id);
        Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id);
        Task<WalkDifficulty> UpdateWalkDifficulty(Guid id, WalkDifficulty walkDifficulty);
    }
}

using NzWalks.API.Models.Domain;

namespace NzWalk.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk> GetWalkAsync(Guid id);
        Task<Walk> AddWalkAsync(Walk walk);
        Task<Walk> DeleteWalkAsync(Guid id);
        Task<Walk> UpdateWalkAsync(Guid id, Walk walk);
    }
}

using Microsoft.EntityFrameworkCore;
using NzWalks.API.Data;
using NzWalks.API.Models.Domain;

namespace NzWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NzWalksDbContext dbContext;

        public WalkDifficultyRepository(NzWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id= Guid.NewGuid();
            await dbContext.WalkDifficulty.AddAsync(walkDifficulty);
            dbContext.SaveChanges();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id)
        {
            var walkDifficulty = await dbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDifficulty == null)
            {
                return null;
            }
            dbContext.WalkDifficulty.Remove(walkDifficulty);
            dbContext.SaveChanges();
            return walkDifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await dbContext.WalkDifficulty.ToListAsync();   
        }

        public async Task<WalkDifficulty> GetWalkDifficultyAsync(Guid id)
        {
            var walkDifficulty = await dbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDifficulty == null)
            {
                return null;
            }
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> UpdateWalkDifficulty(Guid id, WalkDifficulty walkDifficulty)
        {
            var currentWalkDifficulty = await dbContext.WalkDifficulty.FirstOrDefaultAsync(a=>a.Id == id);
            if(currentWalkDifficulty == null)
            {
                return null;
            }
            currentWalkDifficulty.Code = walkDifficulty.Code;
            dbContext.SaveChanges();
            return currentWalkDifficulty;
        }
    }
}

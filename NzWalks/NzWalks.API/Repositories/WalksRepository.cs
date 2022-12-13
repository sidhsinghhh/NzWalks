using Microsoft.EntityFrameworkCore;
using NzWalk.API.Repositories;
using NzWalks.API.Data;
using NzWalks.API.Models.Domain;

namespace NzWalks.API.Repositories
{
    public class WalksRepository : IWalkRepository
    {
        private readonly NzWalksDbContext dbContext;

        public WalksRepository(NzWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> AddWalkAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await dbContext.Walks.AddAsync(walk);
            dbContext.SaveChanges();

            var walkDomain = await dbContext.Walks.Include(a => a.WalkDifficulty)
                .Include(a => a.Region).FirstOrDefaultAsync(x=>x.Id == walk.Id);

            if(walkDomain== null)
            {
                return null;
            }

            return walkDomain;
        }

        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            var walk = await dbContext.Walks
                .Include(a => a.WalkDifficulty)
                .Include(a => a.Region)
                .FirstOrDefaultAsync(a=>a.Id == id);
            if (walk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(walk);
            dbContext.SaveChanges();
            return (walk);
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await dbContext.Walks
                .Include(a=>a.WalkDifficulty)
                .Include(a=>a.Region)
                .ToListAsync();
        }

        public async Task<Walk> GetWalkAsync(Guid id)
        {
            var walk = await dbContext.Walks
                .Include(a=>a.WalkDifficulty)
                .Include(a=>a.Region)
                .FirstOrDefaultAsync(a=>a.Id == id);
            if(walk != null)
            {
                return walk;
            }
            return null;
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            var walkData = await dbContext.Walks
                .Include(a=>a.WalkDifficulty)
                .Include(a=>a.Region)
                .FirstOrDefaultAsync(a=>a.Id==id);
            if(walk == null)
            {
                return null;
            }
            walkData.Name = walk.Name;
            walkData.Length = walk.Length;
            walkData.WalkDifficultyId= walk.WalkDifficultyId;
            walkData.RegionId= walk.RegionId;
            await dbContext.SaveChangesAsync();
            return (walkData);
        }
    }
}

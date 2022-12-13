using Microsoft.EntityFrameworkCore;
using NzWalks.API.Data;
using NzWalks.API.Models.Domain;

namespace NzWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NzWalksDbContext dbContext;
        
        public RegionRepository(NzWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> AddRegionAsync(Region region)
        {
            region.Id= Guid.NewGuid();
            await dbContext.Regions.AddAsync(region);
            dbContext.SaveChanges();
            return region;
        }

        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(a=>a.Id==id);
            if(region == null)
            {
                return null;
            }
            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetRegionAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(a => a.Id == id);
            if(regionDomain == null)
            {
                return null;
            }
            regionDomain.Area = region.Area;
            regionDomain.Name = region.Name;
            regionDomain.Code = region.Code;
            regionDomain.Lat= region.Lat;
            regionDomain.Long= region.Long;
            regionDomain.Population= region.Population;

            dbContext.SaveChangesAsync();
            return regionDomain;
        }
    }
}

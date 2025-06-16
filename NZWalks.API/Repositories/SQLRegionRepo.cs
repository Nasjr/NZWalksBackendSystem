using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepo : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepo(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
           await dbContext.Regions.AddAsync(region);
           await dbContext.SaveChangesAsync();
           return region;
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }

            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;

        }
        

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            var regions = await dbContext.Regions.ToListAsync();
            if (regions == null || regions.Count() == 0 ) {
            return Enumerable.Empty<Region>();
            }
            return regions;
        }

        public async Task<Region?> GetById(Guid id)

        {   // var region = dbContext.Regions.Where(x=> x.Id == Id);
         
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null) 
            {
            return null;
            }
            existingRegion.Name = region.Name;
            existingRegion.Code = region.Code;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}

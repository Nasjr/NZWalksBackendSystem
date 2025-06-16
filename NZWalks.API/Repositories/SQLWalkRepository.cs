using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using System.Linq;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> createWalkAsync(Walk walk)
        {
       
            await dbContext.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;

        }

        public async Task<Walk?> deleteWalk(Guid id)
        {
            var walk = await GetWalkAsync(id);
            if (walk == null)
            {
                return null;
            }
            dbContext.Remove(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllWalkAsync(string? filterOn = null,
            string? filterQuery = null,  string? SortOn = null, 
            bool isAscending = true, int pageNumber = 1, int pageSize = 5)
        {

            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery)) {
                if (filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
                
                
            }
            
            if (walks.Count() == 0)
            {
                return Enumerable.Empty<Walk>();
            }
            // Sorting
            if (!string.IsNullOrWhiteSpace(SortOn)) {
                if (SortOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if(SortOn.Equals("lengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }

            
            }
            // pagination
            var skipResults = (pageNumber - 1) * pageSize;


            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk> GetWalkAsync(Guid id)
        {
            var walk = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x=> x.Id == id);
            if (walk == null)
            {
                return null;
            }
            return walk;
        }


        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            var walkDomainModel = await dbContext.Walks
                .Include(w => w.Difficulty)
                .Include(w => w.Region)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (walkDomainModel == null)
            {
                return null;
            }

            walkDomainModel.WalkImageUrl = walk.WalkImageUrl;
            walkDomainModel.Description = walk.Description;
            walkDomainModel.LengthInKm = walk.LengthInKm;
            walkDomainModel.RegionId = walk.RegionId;
            walkDomainModel.DifficultyId = walk.DifficultyId;
            walkDomainModel.Name = walk.Name;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
                
            }

            return walkDomainModel;
        }


    }
}

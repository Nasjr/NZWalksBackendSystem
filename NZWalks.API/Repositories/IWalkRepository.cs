using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> createWalkAsync(Walk walk);
        Task<Walk> UpdateWalkAsync(Guid id, Walk walk);
        Task<Walk> deleteWalk(Guid id);
        Task<Walk> GetWalkAsync(Guid id);
        Task<IEnumerable<Walk>> GetAllWalkAsync(string ? filterOn = null,string? filterQuery = null, string? SortOn = null, 
            bool isAscending = true,int pageNumber = 1 ,int pageSize = 5);
                                                                                                                                                    
    }
}

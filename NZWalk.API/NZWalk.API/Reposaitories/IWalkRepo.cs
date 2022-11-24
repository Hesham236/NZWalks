using NZWalk.API.Models.Domain;

namespace NZWalk.API.Reposaitories
{
    public interface IWalkRepo 
    {
        Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk> GetWalkAsync(Guid id);
        Task<Walk> AddWalkAsync(Walk walk);
        Task<Walk> DeleteWalkAsync(Guid id);
        Task<Walk> UpdateWalkAsync(Guid id,Walk walk);
    }
}

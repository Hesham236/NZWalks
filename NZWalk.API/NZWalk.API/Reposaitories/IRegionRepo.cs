using NZWalk.API.Models.Domain;

namespace NZWalk.API.Reposaitories
{
    public interface IRegionRepo
    {
        Task<IEnumerable<Region>> GetAllAsync();
        Task<Region> GetAsync(Guid id);
        Task<Region> AddRegionAsync(Region region);
        Task<Region> DeleteRegionAsync(Guid id);
        Task<Region> UpdateRegionAsync(Guid id,Region region);
    }
}

using NZWalk.API.Models.Domain;

namespace NZWalk.API.Reposaitories
{
    public interface IRegionRepo
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}

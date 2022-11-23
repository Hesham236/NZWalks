using NZWalk.API.Models.Domain;

namespace NZWalk.API.Reposaitories
{
    public interface IWalkRepo 
    {
        Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk> GetWalkAsync(Guid id);
    }
}

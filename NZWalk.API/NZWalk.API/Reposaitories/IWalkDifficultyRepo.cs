using NZWalk.API.Models.Domain;

namespace NZWalk.API.Reposaitories
{
    public interface IWalkDifficultyRepo
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();
        Task<WalkDifficulty> GetByIdAsync(Guid id);
        Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id,WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id);
    }
}

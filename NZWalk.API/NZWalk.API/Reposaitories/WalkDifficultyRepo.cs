using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Reposaitories
{
    public class WalkDifficultyRepo : IWalkDifficultyRepo
    {
        private readonly NZWalksDBContext nZWalksDBContext;

        public WalkDifficultyRepo(NZWalksDBContext nZWalksDBContext) 
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await nZWalksDBContext.WalkDifficultyud.ToListAsync(); 
        }

        public async Task<WalkDifficulty> GetByIdAsync(Guid id)
        {
            return await nZWalksDBContext.WalkDifficultyud.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();

            await nZWalksDBContext.AddAsync(walkDifficulty);
            await nZWalksDBContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id,WalkDifficulty walkDifficulty)
        {
            var exWd = await nZWalksDBContext.WalkDifficultyud.FirstOrDefaultAsync(x => x.Id == id);

            if (exWd == null) return null; 
            
            exWd.Code = walkDifficulty.Code;

            await nZWalksDBContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id)
        {
            var exWd = await nZWalksDBContext.WalkDifficultyud.FindAsync(id);
            if (exWd == null) return null;
            nZWalksDBContext.Remove(exWd);
            await nZWalksDBContext.SaveChangesAsync();
            return exWd;

        }

        public async Task<WalkDifficulty> GetByNameAsync(string code)
        {
            return await nZWalksDBContext.WalkDifficultyud.FirstOrDefaultAsync(x => x.Code == code);
        }
    }
}

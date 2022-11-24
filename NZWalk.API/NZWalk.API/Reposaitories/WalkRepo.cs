using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Models.DTO;

namespace NZWalk.API.Reposaitories
{
    public class WalkRepo : IWalkRepo
    {
        private readonly NZWalksDBContext nZWalksDBContext;

        public WalkRepo(NZWalksDBContext nZWalksDBContext) 
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }

        
        public async Task<IEnumerable<Models.Domain.Walk>> GetAllAsync()
        {
            return await nZWalksDBContext
                .Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .ToListAsync();
        }
        public async Task<Models.Domain.Walk> GetWalkAsync(Guid id)
        {
            return await nZWalksDBContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Models.Domain.Walk> AddWalkAsync(Models.Domain.Walk walk)
        {
            walk.Id = Guid.NewGuid();

            await nZWalksDBContext.Walks.AddAsync(walk);
            await nZWalksDBContext.SaveChangesAsync();
            return walk;
        }
        public async Task<Models.Domain.Walk> DeleteWalkAsync(Guid id)
        {
            var existingWalk = await nZWalksDBContext.Walks.FindAsync(id);
            if (existingWalk == null) return null;
            nZWalksDBContext.Walks.Remove(existingWalk);
            await nZWalksDBContext.SaveChangesAsync();
            return existingWalk;
        }
        public async Task<Models.Domain.Walk> UpdateWalkAsync(Guid id, Models.Domain.Walk walk)
        {
            var expwalk = await nZWalksDBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (expwalk != null)
            {
                expwalk.Name = walk.Name;
                expwalk.Length = walk.Length;
                expwalk.RegionId = walk.RegionId;
                expwalk.WalkDifficultyId = walk.WalkDifficultyId;
                await nZWalksDBContext.SaveChangesAsync();
                return expwalk;
            }
            return null;
        }
    }
}

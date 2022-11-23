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
                .Include(x=>x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Reposaitories
{
    public class RegionRepo : IRegionRepo
    {
        private readonly NZWalksDBContext nZWalksDBContext;
        public RegionRepo(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }
        public  async Task<Region> AddRegionAsync(Region region)
        {
           region.Id = Guid.NewGuid();
           await nZWalksDBContext.AddAsync(region);
           await nZWalksDBContext.SaveChangesAsync();
           return region;
        }
        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            var region = await nZWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null) return null;
            //delete the region from database
            nZWalksDBContext.Regions.Remove(region);
            await nZWalksDBContext.SaveChangesAsync();
            return region;

        }
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZWalksDBContext.Regions.ToListAsync();
        }
        public async Task<Region> GetAsync(Guid id)
        {
            return await nZWalksDBContext.Regions.FirstOrDefaultAsync(x=>x.Id==id);
        }
        public async Task<Region> UpdateRegionAsync(Guid id,Region region)
        {
            var exregion = await nZWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (exregion == null) return null;

            //update region data
            exregion.Code= region.Code;
            exregion.Name= region.Name;
            exregion.Area = region.Area;
            exregion.Lat= region.Lat;
            exregion.Long= region.Long;
            exregion.Population= region.Population;

            await nZWalksDBContext.SaveChangesAsync();
            return region;

        }
    }
}

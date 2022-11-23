using Microsoft.EntityFrameworkCore;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Data
{
    public class NZWalksDBContext : DbContext
    {
        public NZWalksDBContext(DbContextOptions<NZWalksDBContext> options) :base(options)
        {

        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficultyud { get; set; }

    }
}

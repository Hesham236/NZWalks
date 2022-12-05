using Microsoft.EntityFrameworkCore;
using NZWalk.API.Models.Domain;
using System.Runtime.CompilerServices;

namespace NZWalk.API.Data
{
    public class NZWalksDBContext : DbContext
    {
        public NZWalksDBContext(DbContextOptions<NZWalksDBContext> options) :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Role>()
                  .HasOne(x => x.role)
                  .WithMany(y => y.UserRoles)
                  .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<User_Role>()
                  .HasOne(x => x.user)
                  .WithMany(y => y.UserRoles)
                  .HasForeignKey(x => x.UserId);
        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficultyud { get; set; }

        public DbSet<User> Users { get; set; }  
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> Users_Roles { get; set; }

    }
}

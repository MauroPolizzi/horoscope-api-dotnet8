using Horoscope.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Horoscope.Infraestructure.Data
{
    public class HoroscopeDbContext : DbContext
    {
        public HoroscopeDbContext(DbContextOptions<HoroscopeDbContext> options) : base(options) { }

        public DbSet<HoroscopeQuery> HoroscopeQueries => Set<HoroscopeQuery>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HoroscopeQuery>(entity =>
            {
                entity.ToTable("HoroscopeQueries");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Sign).IsRequired().HasMaxLength(50);
            });
        }
    }
}

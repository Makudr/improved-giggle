using improved_giggle.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace improved_giggle.Data;

public class AppDbContext : DbContext
{
    public DbSet<CampaignEntity> Campaigns => Set<CampaignEntity>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "dmtool.db");

            optionsBuilder.UseSqlite($"Data Source={path}");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CampaignEntity>(entity =>
        {
            entity.ToTable("Campaign");

            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.System).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.LastModifiedAt).IsRequired();
            entity.Property(e => e.IsDefault).IsRequired();
            entity.Property(e => e.IsActive).IsRequired();
        });
    }
}

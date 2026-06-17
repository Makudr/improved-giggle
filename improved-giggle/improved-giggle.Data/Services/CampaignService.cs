using improved_giggle.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace improved_giggle.Data.Services;

public class CampaignService
{
    private readonly AppDbContext _db;

    public CampaignService(AppDbContext db)
    {
        _db = db;
    }

    public Task<List<CampaignEntity>> GetAllAsync()
        => _db.Campaigns.OrderBy(c => c.Name).ToListAsync();

    public async Task<CampaignEntity> CreateAsync(string name, string system, bool isDefault)
    {
        var entity = new CampaignEntity
        {
            Name = name,
            System = system,
            CreatedAt = DateTime.UtcNow,
            LastModifiedAt = DateTime.UtcNow,
            IsDefault = isDefault,
            IsActive = false
        };

        _db.Campaigns.Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(CampaignEntity entity)
    {
        entity.LastModifiedAt = DateTime.UtcNow;
        _db.Campaigns.Update(entity);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(CampaignEntity entity)
    {
        _db.Campaigns.Remove(entity);
        await _db.SaveChangesAsync();
    }
}

using improved_giggle.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace improved_giggle.Data.Services;

public class CampaignService(AppDbContext db)
{
    private readonly AppDbContext _db = db;

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

    public async Task DeleteAsync(int id)
    {
        var entity = await _db.Campaigns.FindAsync(id);
        if (entity != null)
        {
            _db.Campaigns.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<CampaignEntity?> GetByIdAsync(int id)
    {
        return await _db.Campaigns
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<CampaignEntity?> GetDefaultAsync()
    {
        return await _db.Campaigns
            .FirstOrDefaultAsync(c => c.IsDefault);
    }

    public async Task SetDefaultAsync(int id)
    {
        var all = await _db.Campaigns.ToListAsync();

        foreach (var c in all)
            c.IsDefault = c.Id == id;

        await _db.SaveChangesAsync();
    }

    public async Task MoveUpAsync(int id)
    {
        var list = await _db.Campaigns.OrderBy(c => c.Order).ToListAsync();
        var index = list.FindIndex(c => c.Id == id);

        if (index > 0)
        {
            (list[index].Order, list[index - 1].Order) =
                (list[index - 1].Order, list[index].Order);

            await _db.SaveChangesAsync();
        }
    }

    public async Task MoveDownAsync(int id)
    {
        var list = await _db.Campaigns.OrderBy(c => c.Order).ToListAsync();
        var index = list.FindIndex(c => c.Id == id);

        if (index < list.Count - 1)
        {
            (list[index].Order, list[index + 1].Order) =
                (list[index + 1].Order, list[index].Order);

            await _db.SaveChangesAsync();
        }
    }


}

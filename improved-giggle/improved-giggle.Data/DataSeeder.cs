using improved_giggle.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace improved_giggle.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext db)
        {
#if DEBUG
            // Usuń wszystko
            db.Campaigns.RemoveRange(db.Campaigns);
            await db.SaveChangesAsync();

            // Dodaj dane testowe
            db.Campaigns.Add(new CampaignEntity
            {
                Name = "Kampania testowa A",
                IsDefault = true
            });

            db.Campaigns.Add(new CampaignEntity
            {
                Name = "Kampania testowa B",
                IsDefault = false
            });

            await db.SaveChangesAsync();
#endif
        }
    }

}

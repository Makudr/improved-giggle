using improved_giggle.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace improved_giggle.Data.Services
{
    public class ActiveCampaignService
    {
        private readonly CampaignService _campaigns;

        public CampaignEntity? Current { get; private set; }

        public event Action<CampaignEntity?>? CampaignChanged;

        public ActiveCampaignService(CampaignService campaigns)
        {
            _campaigns = campaigns;
        }

        public async Task InitializeAsync()
        {
            var defaultCampaign = await _campaigns.GetDefaultAsync();

            if (defaultCampaign == null)
            {
                Current = null;
                CampaignChanged?.Invoke(null);
                return;
            }

            Current = defaultCampaign;
            CampaignChanged?.Invoke(Current);
        }

        public void SetActive(CampaignEntity campaign)
        {
            Current = campaign;
            CampaignChanged?.Invoke(Current);
        }
    }

}

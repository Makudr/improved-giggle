using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace improved_giggle.Data.Entities
{
    public class CampaignEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string System { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public bool IsDefault { get; set; }
    }
}

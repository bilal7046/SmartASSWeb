using System.ComponentModel.DataAnnotations;

namespace SmartASSWeb.Dal.Entities
{
    
    public class CampaignAffiliate : EntityBase
    {

        [Key]
        public string CampaignAffiliateId { get; set; }
        public string Name { get; set; }
    }
}
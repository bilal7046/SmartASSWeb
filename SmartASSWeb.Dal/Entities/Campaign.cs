using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SmartASSWeb.Dal.Entities
{
    /// <summary>
    /// Represents a commission structure affiliates sign up to and are paid out
    /// </summary>
    
    public class Campaign : EntityBase
    {

        [Key]
        public string CampaignId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("campaignDate")]
        public DateTime CampaignDate { get; set; }
        [JsonProperty("commissionAmount")]
        public string CommissionAmount { get; set; }
        [JsonProperty("salesAmount")]
        public string SalesAmount { get; set; }
        [JsonProperty("campaignStatus")]
        public string CampaignStatus { get; set; }
        [JsonProperty("notes")]
        public string Notes { get; set; }
    }
}
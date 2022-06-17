using System;
using System.Runtime.InteropServices;
using Google.Cloud.Firestore;

namespace SmartASSWeb.Bll
{
    /// <summary>
    /// Represents a commission structure affiliates sign up to and are paid out
    /// </summary>
    [FirestoreData]
    public class Campaign
    {
        [FirestoreDocumentId]
        public string CampaignId { get; set; }
        [FirestoreProperty("name")]
        public string Name { get; set; }
        [FirestoreProperty("campaignDate")]
        public Timestamp CampaignDate { get; set; }
        [FirestoreProperty("commissionAmount")]
        public string CommissionAmount { get; set; }
        [FirestoreProperty("salesAmount")]
        public string SalesAmount { get; set; }
        [FirestoreProperty("campaignStatus")]
        public string CampaignStatus { get; set; }
        [FirestoreProperty("notes")]
        public string Notes { get; set; }
    }
}
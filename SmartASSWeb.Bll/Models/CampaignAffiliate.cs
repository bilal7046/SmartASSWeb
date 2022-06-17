using Google.Cloud.Firestore;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class CampaignAffiliate
    {
        [FirestoreDocumentId]
        public string CampaignAffiliateId { get; set; }
        [FirestoreProperty("name")]
        public string Name { get; set; }
    }
}
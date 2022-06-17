using System.ComponentModel;
using Google.Cloud.Firestore;

namespace SmartASSWeb.Bll
{
    [FirestoreData]
    public class TeamMemberGoal
    {
        [FirestoreDocumentId]
        public string UserId { get; set; }
        [DefaultValue(100)]
        [FirestoreProperty("connectedLeads")]
        public int ConnectedLeads { get; set; }
        [DefaultValue(100)]
        [FirestoreProperty("qualifiedLeads")]
        public int QualifiedLeads { get; set; }
        [DefaultValue(100)]
        [FirestoreProperty("quotedLeads")]
        public int QuotedLeads { get; set; }
        [DefaultValue(100)]
        [FirestoreProperty("closedLeads")]
        public int ClosedLeads { get; set; }
        [DefaultValue(100)]
        [FirestoreProperty("lostLeads")]
        public int LostLeads { get; set; }
        [DefaultValue(100)]
        [FirestoreProperty("overcameObjectionLeads")]
        public int OvercameObjectionLeads { get; set; }
    }
}

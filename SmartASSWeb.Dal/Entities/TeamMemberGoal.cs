using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartASSWeb.Dal.Entities
{
    
    public class TeamMemberGoal : EntityBase
    {
        [Key]
        public int TeamMemberGoalId { get; set; }
        public string UserId { get; set; }
        [DefaultValue(100)]
        public int ConnectedLeads { get; set; }
        [DefaultValue(100)]
        public int QualifiedLeads { get; set; }
        [DefaultValue(100)]
        public int QuotedLeads { get; set; }
        [DefaultValue(100)]
        public int ClosedLeads { get; set; }
        [DefaultValue(100)]
        public int LostLeads { get; set; }
        [DefaultValue(100)]
        public int OvercameObjectionLeads { get; set; }
    }
}

using System.Collections.Generic;
using SmartASSWeb.Bll;

namespace SmartASSWeb.ViewModels.Charts.BusinessCards
{
    public class BusinessCardStatsViewModel
        : ChartBaseModel
    {
        #region Ctor


        /// <summary>
        /// Ctor
        /// </summary>
        public BusinessCardStatsViewModel()
        {
        }

        #endregion
        public List<ChartData> NumberOfCardsSent { get; set; }
        public List<ChartData> AcceptedCards { get; set; }
        public List<ChartData> PendingCards { get; set; }
        public List<ChartData> InvitesReceived { get; set; }
        public List<ChartData> PeopleSharedMyCards { get; set; }
        public List<DonutData> SocialMediaLinks { get; set; }
        public List<ChartData> EngagementRates { get; set; }
        public List<ChartData> AgeClicks { get; set; }
        public AgeData AgeData { get; set; }
        public List<ChartData3> GenderClicks { get; set; }
        public List<ChartData> JobLevelClicks { get; set; }
        public JobLevelData JobLevelData { get; set; }
    }

    public class JobLevelData
    {
        public string[] JobLevels { get; set; } = new string[]{};
        public int[] Data { get; set; } = new int[] { };
    }
    public class AgeData
    {
        public string[] AgeGroups { get; set; } = new string[]{};
        public int[] Data { get; set; } = new int[] { };
    }
}
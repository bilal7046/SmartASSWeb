using System.Collections.Generic;
using SmartASSWeb.Bll;

namespace SmartASSWeb.ViewModels
{
    public class ConversionGoalViewModel
    {
        public ConversionGoalViewModel()
        {
            ConversionGoals = new List<ConversionGoal>();
        }
        // Goal1: contact 10 people a day
        // Goal2: drive 4 quality leads a week
        // Goal3: have 5 meetings a week
        // Goal4: send out 7 proposals a week
        // Goal5: sign up 20 sales a month

        //Goal has name
        //Goal has start date and end date
        //Goal has a period (alert)
        //Goal has a measure factor value
        
        //Measures:
        //Contacts
        //Connected Leads
        //Meetings
        //Closed Leads
        public List<LookupDataItem> ConversionNameList { get; set; }
        public List<LookupDataItem> ConversionPeriodList { get; set; }

        public IEnumerable<ConversionGoal> ConversionGoals { get; set; }
    }
}
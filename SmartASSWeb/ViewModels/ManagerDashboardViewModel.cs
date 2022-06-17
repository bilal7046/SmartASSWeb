using System;
using System.Collections.Generic;
using SmartASSWeb.Bll;

namespace SmartASSWeb.ViewModels
{
    public class ManagerDashboardViewModel
        : ChartBaseModel
    {
        public ManagerDashboardViewModel()
        {
            LeadsPerIndustry = new LeadsPerIndustry();
            TotalRevenueForPeriod = new List<ChartData>();
            ClosedLeadsOverPeriod = new List<ChartData>();
        }

        public double TotalSales { get; set; }
        public int TotalLeads { get; set; }
        public LeadsPerIndustry LeadsPerIndustry { get; set; }
        public ChartLeadsPerMember LeadsPerTeamMember { get; set; }
        public ChartClosedLeadsPerMember ChartClosedLeadsPerMember { get; set; }
        public List<ChartData> TotalRevenueForPeriod { get; set; }
        public List<ChartData> ClosedLeadsOverPeriod { get; set; }
    }

    public class LeadsPerIndustry
    {
        public string[] Industries { get; set; }
        public int[] Data { get; set; }
    }

    public class LeadsForPeriod
    {
        public LeadsForPeriod()
        {
            data = new List<object[]>();
        }
        public string label { get; set; }
        public IEnumerable<object[]> data { get; set; }

    }
}
using System.Collections.Generic;
using SmartASSWeb.Bll;

namespace SmartASSWeb.ViewModels
{
    public class LeadDashboardViewModel
        : ChartBaseModel
    {
        public LeadDashboardViewModel()
        {
            LeadSourceData = new List<Pie>();
            LeadStatusData = new List<Funnel>();
            LeadsForMonth = new List<long[]>();
            LeadsForYear = new List<NightingGale>();
        }

        public IEnumerable<Funnel> LeadStatusData { get; set; }
        public IEnumerable<Pie> LeadSourceData { get; set; }
        public IEnumerable<long[]> LeadsForMonth { get; set; }
        public IEnumerable<NightingGale> LeadsForYear { get; set; }

        public static List<string> ColorBasket => new List<string> {
                "#4385F6",
                "#F53AA1",
                "#FABF00",
                "#30AA55",
                "#9D44FF",
                "#129DB0",
                "#F5A52A",
                "#23BFAA",
                "#FAA586",
                "#EB8CC6"
             };
    }

    public class Funnel
    {
        public string label { get; set; }
        public int y { get; set; }
    }

    public class Pie
    {
        public string label { get; set; }
        public string color { get; set; }
        public int data { get; set; }
    }

    public class FlotBar
    {
        public long Date { get; set; }
        public int Value { get; set; }
    }

    public class NightingGale
    {
        public string name { get; set; }
        public int value { get; set; }
    }
}
using System.Collections.Generic;
using System.Data;
// ReSharper disable MemberCanBePrivate.Global

namespace SmartASSWeb.Bll
{
    public class ChartData
    {
        public ChartData()
        {
            
        }
        public ChartData(string key, int value)
        {
            Key = key;
            Value = value;
        }
        public ChartData(string key, double sum)
        {
            Key = key;
            Sum = sum;
        }

        public string Key { get; set; }
        public int Value { get; set; }
        public double Sum { get; set; }
    }

    public class DonutData
    {
        public DonutData()
        {

        }
        public DonutData(string labelInput, int valueInput)
        {
            label = labelInput;
            value = valueInput;
        }

        public string label { get; set; }
        public int value { get; set; }
    }
    public class ChartData3
    {
        public ChartData3()
        {
            
        }
        public ChartData3(string date, int value, int value2, int value3)
        {
            Date = date;
            Value = value;
            Value2 = value2;
            Value3 = value3;
        }

        public string Date { get; set; }
        public int Value { get; set; }
        public int Value2 { get; set; }
        public int Value3 { get; set; }
    }

    public class ChartClosedLeadsPerMember
    {
        public ChartClosedLeadsPerMember()
        {
            TeamMembers = new string[] { };
            ClosedLeads = new List<int>();
        }
        public string[] TeamMembers { get; set; }
        public List<int> ClosedLeads { get; set; }
    }
    public class ChartLeadsPerMember
    {
        public ChartLeadsPerMember()
        {
            TeamMembers = new string[] { };
            ConnectedLeads = new List<int>();
            QualifiedLeads = new List<int>();
            QuotedLeads = new List<int>();
            OvercameLeads = new List<int>();
            ClosedLeads = new List<int>();
            LostLeads = new List<int>();
        }
        public string[] TeamMembers { get; set; }
        public List<int> ConnectedLeads { get; set; }
        public List<int> QualifiedLeads { get; set; }
        public List<int> QuotedLeads { get; set; }
        public List<int> OvercameLeads { get; set; }
        public List<int> ClosedLeads { get; set; }
        public List<int> LostLeads { get; set; }
    }
}

using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace SmartASSWeb.Bll
{
    public interface ILookupDictionary
    {
        IEnumerable<LookupDataItem> BusinessCardVersions { get; }
        IEnumerable<LookupDataItem> BusinessCardBackgroundImages { get; }
        IEnumerable<LookupDataItem> Industries { get; }
        IEnumerable<LookupDataItem> Tags { get; }
        IEnumerable<LookupDataItem> CompanyTypes { get; }
        IEnumerable<LookupDataItem> JobLevels { get; }
        IEnumerable<LookupDataItem> LeadSources { get; }
        IEnumerable<LookupDataItem> LeadStatuses { get; }
        IEnumerable<LookupDataItem> LogActions { get; }
        IEnumerable<LookupDataItem> Titles { get; }
        IEnumerable<LookupDataItem> ConversionNameList { get; }
        IEnumerable<LookupDataItem> ConversionPeriodList { get; }
        IEnumerable<LookupDataItem> FontSizes { get; }
    }
    public class LookupDictionary
        : ILookupDictionary
    {
        public IEnumerable<LookupDataItem> BusinessCardVersions => new List<LookupDataItem>
        {
            //new LookupDataItem(EnumBusinessCardVersion._MobileCard1.ToString(), "SmartASS 1 (Mobile)"),
            //new LookupDataItem(EnumBusinessCardVersion._MobileCard2.ToString(), "SmartASS 2 (Mobile)"),
            //new LookupDataItem(EnumBusinessCardVersion._MobileCard3.ToString(), "SmartASS 3 (Mobile)"),
            new LookupDataItem(EnumBusinessCardVersion._MobileCard4.ToString(), "SmartAss A (Mobile)"),
            new LookupDataItem(EnumBusinessCardVersion._MobileCard5.ToString(), "SmartAss B (Mobile)"),
            new LookupDataItem(EnumBusinessCardVersion._MobileCard6.ToString(), "SmartAss C (Mobile)"),
            new LookupDataItem(EnumBusinessCardVersion._BusinessCard1.ToString(), "SmartASS Prime"),
            new LookupDataItem(EnumBusinessCardVersion._BusinessCard2.ToString(), "SmartASS Concept"),
            //new LookupDataItem(EnumBusinessCardVersion._BusinessCard3.ToString(), "SmartASS Basic"),
        };
        public IEnumerable<LookupDataItem> BusinessCardBackgroundImages => new List<LookupDataItem>
        {
            new LookupDataItem(EnumBusinessCardBackgroundImage._Concrete.ToString(), "Concrete"),
            new LookupDataItem(EnumBusinessCardBackgroundImage._Stones.ToString(), "Stones"),
            new LookupDataItem(EnumBusinessCardBackgroundImage._Wall.ToString(), "Wall"),
            new LookupDataItem(EnumBusinessCardBackgroundImage._Waves.ToString(), "Waves"),
            new LookupDataItem(EnumBusinessCardBackgroundImage._Wood.ToString(), "Wood"),
        };
        public IEnumerable<LookupDataItem> ConversionNameList =>
            new List<LookupDataItem>
            {
                new LookupDataItem("Contacts", "Contacts"),
                new LookupDataItem("Connected Leads", "Connected Leads"),
                new LookupDataItem("Meetings", "Meetings"),
                new LookupDataItem("Closed Leads", "Closed Leads"),
            };
        public IEnumerable<LookupDataItem> ConversionPeriodList =>
            new List<LookupDataItem>
            {
                new LookupDataItem("Daily", "Daily"),
                new LookupDataItem("Weekly", "Weekly"),
                new LookupDataItem("Monthly", "Monthly"),
            };

        public IEnumerable<LookupDataItem> Industries
        {
            get
            {
                var data = new List<LookupDataItem>
                {
                    new LookupDataItem("Accounting & Auditing"),
                    new LookupDataItem("Agriculture"),
                    new LookupDataItem("Architecture & Property"),
                    new LookupDataItem("Banking & Insurance"),
                    new LookupDataItem("Construction and Engineering"),
                    new LookupDataItem("Home Services"),
                    new LookupDataItem("Education & Training"),
                    new LookupDataItem("Finance and Business Services"),
                    new LookupDataItem("Government & NGO"),
                    new LookupDataItem("Health and Personal Services"),
                    new LookupDataItem("Human Resources & Recruitment"),
                    new LookupDataItem("Legal & Medical"),
                    new LookupDataItem("IT, Communication & Media"),
                    new LookupDataItem("Manufacturing and Chemicals"),
                    new LookupDataItem("Marketing, Advertising & PR"),
                    new LookupDataItem("Mining and Minerals"),
                    new LookupDataItem("Other"),
                    new LookupDataItem("R&D, Science & Scientific Research"),
                    new LookupDataItem("Real Estate & Property"),
                    new LookupDataItem("FMCG, Retail and Wholesale"),
                    new LookupDataItem("Telecommunications"),
                    new LookupDataItem("Textile / Clothing"),
                    new LookupDataItem("Tourism, Leisure, Hospitality"),
                    new LookupDataItem("Transport & Automotive")
                };
                return data;
            }
        }
        public IEnumerable<LookupDataItem> CompanyTypes
        {
            get
            {
                var data = new List<LookupDataItem>
                {
                    new LookupDataItem("Sole Proprietorship", "Sole Proprietorship"),
                    new LookupDataItem("Pty Ltd", "Pty Ltd"),
                    new LookupDataItem("Partnership", "Partnership"),
                    new LookupDataItem("Public Company", "Public Company"),
                    new LookupDataItem("Franchise", "Franchise")
                };
                return data;
            }
        }
        public IEnumerable<LookupDataItem> JobLevels
        {
            get
            {
                var data = new List<LookupDataItem>
                {
                    new LookupDataItem("EntryLevel", "EntryLevel"),
                    new LookupDataItem("MidLevel", "MidLevel"),
                    new LookupDataItem("Intermediate", "Intermediate"),
                    new LookupDataItem("Senior", "Senior"),
                    new LookupDataItem("Executive", "Executive")
                };
                return data;
            }
        }
        public IEnumerable<LookupDataItem> FontSizes
        {
            get
            {
                var data = new List<LookupDataItem>
                {
                    new LookupDataItem("14px", "1x"),
                    new LookupDataItem("16px", "2x"),
                    new LookupDataItem("18px", "3x"),
                    new LookupDataItem("20px", "4x"),
                    new LookupDataItem("24px", "5x"),
                    new LookupDataItem("28px", "6x"),
                    new LookupDataItem("30px", "7x"),
                    new LookupDataItem("32px", "8x"),
                    new LookupDataItem("34px", "9x"),
                    new LookupDataItem("38px", "10x"),
                };
                return data;
            }
        }
        public IEnumerable<LookupDataItem> LeadSources
        {
            get
            {
                var data = new List<LookupDataItem>
                {
                    new LookupDataItem("Advertisement", "Advertisement"),
                    new LookupDataItem("Employee Referral", "Employee Referral"),
                    new LookupDataItem("External Referral", "External Referral"),
                    new LookupDataItem("In Store", "In Store"),
                    new LookupDataItem("On Site", "On Site"),
                    new LookupDataItem("Other", "Other"),
                    new LookupDataItem("Social", "Social"),
                    new LookupDataItem("Trade Show", "Trade Show"),
                    new LookupDataItem("Web", "Web"),
                    new LookupDataItem("Word of Mouth", "Word of Mouth"),
                };
                return data;
            }
        }
        public IEnumerable<LookupDataItem> LeadStatuses
        {
            get
            {
                var data = new List<LookupDataItem>
                {
                    new LookupDataItem("Connected", "Connected"),
                    new LookupDataItem("Closed", "Closed"),
                    new LookupDataItem("Quoted", "Quoted"),
                    new LookupDataItem("Overcame Objections", "Overcame Objections"),
                    new LookupDataItem("Closed", "Closed"),
                    new LookupDataItem("Lost", "Lost")
                };
                return data;
            }
        }
        public IEnumerable<LookupDataItem> LogActions
        {
            get
            {
                var data = new List<LookupDataItem>
                {
                    new LookupDataItem("Call", "Call"),
                    new LookupDataItem("Email", "Email"),
                    new LookupDataItem("Task", "Task"),
                    new LookupDataItem("Event", "Event"),
                    new LookupDataItem("Other", "Other")
                };
                return data;
            }
        }
        public IEnumerable<LookupDataItem> Titles
        {
            get
            {
                var data = new List<LookupDataItem>
                {
                    new LookupDataItem("Mr", "Mr."),
                    new LookupDataItem("Ms", "Ms."),
                    new LookupDataItem("Mrs", "Mrs."),
                    new LookupDataItem("Dr", "Dr."),
                    new LookupDataItem("Prof", "Prof.")
                };
                return data;
            }
        }
        public IEnumerable<LookupDataItem> Tags
        {
            get
            {
                var data = new List<LookupDataItem>
                {
                    new LookupDataItem("Information Technology", "Information Technology"),
                    new LookupDataItem("Web Design", "Web Design"),
                    new LookupDataItem("Tutorials", "Tutorials"),
                    new LookupDataItem("Cookery", "Cookery"),
                    new LookupDataItem("Electrical Repairs", "Electrical Repairs")
                };
                return data;
            }
        }
    }

    public class LookupDataItem
    {
        public string Key { get; set; }
        public string Value { get; }

        public LookupDataItem(string key)
        {
            Key = key;
            Value = key;
        }
        public LookupDataItem(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}

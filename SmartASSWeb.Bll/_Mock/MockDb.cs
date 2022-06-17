using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SmartASSWeb.Core.Service;
// ReSharper disable AssignNullToNotNullAttribute

namespace SmartASSWeb.Bll._Mock
{
    public class MockDb
    {
        #region Ctor
        
        private readonly ILookupDictionary _dictionary;
        private static readonly MockDataGenerator Generator = new MockDataGenerator();
        private const string UserId = "Id7QxCuXIAMIVgBPLw8kjqbMWoJ3";
        private const string BerthaDoe = "PavwfXwA5VWjQU0ghXIw9cS4bms1";
        private const string SimonDoe = "Mpk1RBDOmhP1Z8NQOe52Tva9gTt1";
        private const string PeterDoe = "2tXUL5zdLmY0r4kzUqUzik4p2ma2";
        private const string JuliaDoe = "OMpMVPDK4LP3LusHAInAd6szg6u1";

        public MockDb()
        {
            _dictionary = new LookupDictionary();
            UserProfiles = new List<UserProfile>();
            UserActions = new List<UserAction>();
            Activities = new List<Activity>();
            Leads = new List<Lead>();
            Contacts = new List<Contact>();
            BusinessCards = new List<BusinessCard>();
        }

        #endregion

        #region Database

        public List<T> GetTable<T>(string tableName)
        {
            switch (tableName)
            {
                case FirestoreTableStore.UserProfiles:
                    return UserProfiles as List<T>;
                case FirestoreTableStore.UserActions:
                    return UserActions as List<T>;
                case FirestoreTableStore.Activities:
                    return Activities as List<T>;
                case FirestoreTableStore.Leads:
                    return Leads as List<T>;
                case FirestoreTableStore.Contacts:
                    return Contacts as List<T>;
                case FirestoreTableStore.BusinessCards:
                    return BusinessCards as List<T>;
                default:
                    throw new Exception("Table not found!");
            }
        }
        public List<T> GetTable<T>(string tableName, Func<T, bool> func)
        {
            switch (tableName)
            {
                case FirestoreTableStore.UserProfiles:
                    return (UserProfiles as List<T>).Where(func).ToList();
                case FirestoreTableStore.UserActions:
                    return (UserActions as List<T>).Where(func).ToList();
                case FirestoreTableStore.Activities:
                    return (Activities as List<T>).Where(func).ToList();
                case FirestoreTableStore.Leads:
                    return (Leads as List<T>).Where(func).ToList();
                case FirestoreTableStore.Contacts:
                    return (Contacts as List<T>).Where(func).ToList();
                case FirestoreTableStore.BusinessCards:
                    return (BusinessCards as List<T>).Where(func).ToList();
                default:
                    throw new Exception("Table not found!");
            }
        }
        public IList GetTable(string tableName)
        {
            switch (tableName)
            {
                case FirestoreTableStore.UserProfiles:
                    return UserProfiles;
                case FirestoreTableStore.UserActions:
                    return UserActions;
                case FirestoreTableStore.Activities:
                    return Activities;
                case FirestoreTableStore.Leads:
                    return Leads;
                case FirestoreTableStore.Contacts:
                    return Contacts;
                case FirestoreTableStore.BusinessCards:
                    return BusinessCards;
                default:
                    throw new Exception("Table not found!");
            }
        }
        public IList GetTable(string tableName, Func<object, bool> func)
        {
            switch (tableName)
            {
                case FirestoreTableStore.UserProfiles:
                    return UserProfiles.Where(func).ToList();
                case FirestoreTableStore.UserActions:
                    return UserActions.Where(func).ToList();
                case FirestoreTableStore.Activities:
                    return Activities.Where(func).ToList();
                case FirestoreTableStore.Leads:
                    return Leads.Where(func).ToList();
                case FirestoreTableStore.Contacts:
                    return Contacts.Where(func).ToList();
                case FirestoreTableStore.BusinessCards:
                    return BusinessCards.Where(func).ToList();
                default:
                    throw new Exception("Table not found!");
            }
        }

        #endregion

        #region Tables

        private List<UserProfile> _userProfiles;
        private List<UserProfile> UserProfiles
        {
            get
            {
                var userProfiles = new List<UserProfile>
                {
                    Generator.GetUserProfile(UserId, "donald@wald.co.za", "Donald", "Mafa", "Male", "Intermediate", "Technician", new []{ BerthaDoe, SimonDoe}),
                    Generator.GetUserProfile(BerthaDoe, "Bertha", "Doe", "Female", "Executive", "CEO", new []{ SimonDoe, PeterDoe, JuliaDoe}),
                    Generator.GetUserProfile(SimonDoe, "Simon", "Doe", "Male", "Senior", "Senior Director", new []{PeterDoe, BerthaDoe}),
                    Generator.GetUserProfile(PeterDoe, "Peter", "Doe", "Male", "MidLevel", "Floor Manager", new []{SimonDoe, BerthaDoe}),
                    Generator.GetUserProfile(JuliaDoe, "Julia", "Doe", "Female", "EntryLevel", "Administrator", new []{ PeterDoe}),
                };
                
                return userProfiles;
            }
            set => _userProfiles = value;
        }

        private List<Activity> _activities;
        private List<Activity> Activities
        {
            get
            {
                var activities = new List<Activity>();

                foreach (var activityType in System.Enum.GetValues(typeof(EnumActivity)).Cast<EnumActivity>())
                {
                    var acts = Generator.GetActivities(10, JuliaDoe, activityType).Result;
                    activities.AddRange(acts);
                }
                foreach (var activityType in System.Enum.GetValues(typeof(EnumActivity)).Cast<EnumActivity>())
                {
                    var acts = Generator.GetActivities(25, SimonDoe, activityType).Result;
                    activities.AddRange(acts);
                }
                foreach (var activityType in System.Enum.GetValues(typeof(EnumActivity)).Cast<EnumActivity>())
                {
                    var acts = Generator.GetActivities(50, UserId, activityType).Result;
                    activities.AddRange(acts);
                }

                return activities;
            }
            set => _activities = value;
        }

        private List<UserAction> UserActions { get; set; }
        private List<Lead> Leads { get; set; }
        private List<Contact> Contacts { get; set; }

        private List<BusinessCard> _businessCards;
        private List<BusinessCard> BusinessCards
        {
            get
            {
                var businessCards = new List<BusinessCard>
                {
                    Generator.GetBusinessCard(JuliaDoe, new string[]{}, "JULIA1"),
                    Generator.GetBusinessCard(JuliaDoe, new string[]{}, "JULIA2"),
                    Generator.GetBusinessCard(JuliaDoe, new string[]{}, "JULIA3"),
                    Generator.GetBusinessCard(SimonDoe, new string[]{}, "SIMON1"),
                    Generator.GetBusinessCard(SimonDoe, new string[]{}, "SIMON2"),
                    Generator.GetBusinessCard(UserId, new string[]{ JuliaDoe, SimonDoe }, "1"),
                    Generator.GetBusinessCard(UserId, new string[]{}, "2"),
                    Generator.GetBusinessCard(UserId, new string[]{ JuliaDoe }, "3"),
                };

                return businessCards;
            }
            set => _businessCards = value;
        }

        #endregion

        #region FactoryMethods

        

        #endregion

    }
}

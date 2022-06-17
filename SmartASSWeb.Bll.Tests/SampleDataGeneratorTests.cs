using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartASSWeb.Bll._Mock;
using SmartASSWeb.Bll.Core;
using SmartASSWeb.Bll.Models;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Bll.Tests
{
    [TestClass]
    public class SampleDataGeneratorTests
    {
        private IFirebaseService _service;
        private MockDataGenerator _generator;
        
        #region Initialize & CleanUp

        [TestInitialize]
        public void TestInitialize()
        {
            _generator = new MockDataGenerator();
            _service = new FirebaseService(new TestKeyFileResolver());
        }
        [TestCleanup]
        public void TestClean()
        {
            _service = null;
        }

        #endregion

        [Ignore]
        [TestMethod]
        public void Generate__GivenSomeValues_ShouldAddUsers()
        {
            //--- Arrange ----------------------------------------------------
            //var users = GetUsers(10);
            //--- Execute ----------------------------------------------------
            //foreach (var user in users)
            //{
            //    await _service.Add()
            //}
            //--- Assert ----------------------------------------------------
            
        }

        //[Ignore]
        [TestMethod]
        public async Task Generate_GivenLeadData_ShouldAddLeads()
        {
            //--- Arrange ----------------------------------------------------
            var leads = _generator.GetLeads(50, MockDataGenerator.DemoUser);
            //--- Execute ----------------------------------------------------
            foreach (var lead in leads)
            {
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Leads, lead);
            }
            //--- Assert ----------------------------------------------------
        }
        [TestMethod]
        public async Task Generate_GivenUserLeadData_ShouldAddLeads()
        {
            //--- Arrange ----------------------------------------------------
            var leads = _generator.GetLeads(5, MockDataGenerator.PeterDoe, EnumLeadStatus.Closed.ToString(), new DateTime(2020,10,1),new DateTime(2020,12,31));
            //--- Execute ----------------------------------------------------
            foreach (var lead in leads)
            {
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Leads, lead);
            }
            //--- Assert ----------------------------------------------------
        }

        //[Ignore]
        [TestMethod]
        public async Task UpdateLeads_GivenSomeData_ShouldChangeLeadStatus()
        {
            //--- Arrange ----------------------------------------------------
            var leads = await _service.GetCollection<Lead>(FirestoreTableStore.Leads, "leadStatus", "Contacted");
            //--- Execute ----------------------------------------------------
            foreach (var lead in leads)
            {
                await _service.Update(lead.LeadId, FirestoreTableStore.Leads, "leadStatus",
                    EnumLeadStatus.Qualified.ToString());
            }
            //--- Assert ----------------------------------------------------
        }

        [TestMethod]
        public async Task Generate_BusinessCards_ShouldAddBusinessCards()
        {
            //--- Arrange ----------------------------------------------------
            var businessCards = _generator.GetBusinessCards(MockDataGenerator.DemoUser, 3);
            //--- Execute ----------------------------------------------------
            foreach (var businessCard in businessCards)
            {
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.BusinessCards, businessCard);
            }
            //--- Assert ----------------------------------------------------

        }


        [TestMethod]
        public async Task Generate_Activities_ShouldAddActivities()
        {
            //--- Arrange ----------------------------------------------------
            var activities = await _generator.GetActivities(11, MockDataGenerator.JuliaDoe, EnumActivity.EmailClick);
            //--- Execute ----------------------------------------------------
            //await _service.AddCollection(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activities);
            foreach (var activity in activities)
            {
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
            }
            //--- Assert ----------------------------------------------------
        }

        [TestMethod]
        public async Task ModifyActivityCreators_GivenRandom_ShouldSetRandomCreators()
        {
            //--- Arrange ----------------------------------------------------
            var activitiesForCard = await _service.GetCollection<Activity>(FirestoreTableStore.Activities, "activityBusiness", "2bd09cda-7536-4fc6-beea-23ff1ef3f8a4");
            //--- Execute ----------------------------------------------------
            foreach (var activity in activitiesForCard)
            {
                await _service.Update(activity.ActivityId, FirestoreTableStore.Activities, "activityCreator", MockDataGenerator.Users.RandomItem<string>());
            }
            //--- Assert ----------------------------------------------------

        }
        [TestMethod]
        public async Task ModifyActivityCreators_GivenRandom_ShouldSetDemo()
        {
            //--- Arrange ----------------------------------------------------
            var activitiesForCard = await _service.GetCollection<Activity>(FirestoreTableStore.Activities, "activityBusiness", "2bd09cda-7536-4fc6-beea-23ff1ef3f8a4");
            //--- Execute ----------------------------------------------------
            foreach (var activity in activitiesForCard.Where(p => p.ActivityType == EnumActivity.CardSharedInternal.ToString() || p.ActivityType == EnumActivity.CardSharedExternal.ToString()))
            {
                await _service.Update(activity.ActivityId, FirestoreTableStore.Activities, "activityCreator", MockDataGenerator.DemoUser);
            }
            //--- Assert ----------------------------------------------------

        }

        [TestMethod]
        public async Task Generate_Notifications_ShouldAddNotifications()
        {
            //--- Arrange ----------------------------------------------------
            //var notifications = await _generator.GetNotifications(1, MockDataGenerator.DemoUser, MockDataGenerator.BerthaDoe, EnumNotificationType.REQUEST, "Bertha Doe has request to follow you?");
            //var notifications = await _generator.GetNotifications(2, MockDataGenerator.DemoUser, MockDataGenerator.JuliaDoe, EnumNotificationType.MESSAGE, string.Empty);
            var notifications = await _generator.GetNotifications(1, MockDataGenerator.DemoUser, MockDataGenerator.DemoUser, EnumNotificationType.ALERT, "New features to be added soon!");
            //--- Execute ----------------------------------------------------
            foreach (var notification in notifications)
            {
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Notifications, notification);
            }
            //--- Assert ----------------------------------------------------
        }



    }
}

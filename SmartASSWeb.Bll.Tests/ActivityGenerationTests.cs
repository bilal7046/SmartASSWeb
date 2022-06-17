using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartASSWeb.Bll._Mock;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Bll.Tests
{
    [TestClass]
    public class ActivityGenerationTests
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

        [TestMethod]
        public async Task Generate_GivenActivityType_CardViewed_ShouldGenerate()
        {
            //--- Arrange ----------------------------------------------------
            var activityType = EnumActivity.CardViewed;
            var userActivities = await _generator.GetActivities(20, MockDataGenerator.UserId, activityType);
            var juliaActivities = await _generator.GetActivities(20, MockDataGenerator.JuliaDoe, activityType);
            var simonActivities = await _generator.GetActivities(20, MockDataGenerator.SimonDoe, activityType);
            var berthaActivities = await _generator.GetActivities(20, MockDataGenerator.BerthaDoe, activityType);
            //--- Execute ----------------------------------------------------
            foreach (var activity in userActivities.Union(juliaActivities).Union(simonActivities).Union(berthaActivities))
            {
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
            }
            //--- Assert ----------------------------------------------------
        }
        [TestMethod]
        public async Task Generate_GivenActivityType_RequestAccepted_ShouldGenerate()
        {
            //--- Arrange ----------------------------------------------------
            var activityType = EnumActivity.RequestAccepted;
            var userActivities = await _generator.GetActivities(20, MockDataGenerator.UserId, activityType);
            var juliaActivities = await _generator.GetActivities(20, MockDataGenerator.JuliaDoe, activityType);
            var simonActivities = await _generator.GetActivities(20, MockDataGenerator.SimonDoe, activityType);
            var berthaActivities = await _generator.GetActivities(20, MockDataGenerator.BerthaDoe, activityType);
            //--- Execute ----------------------------------------------------
            foreach (var activity in userActivities.Union(juliaActivities).Union(simonActivities).Union(berthaActivities))
            {
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
            }
            //--- Assert ----------------------------------------------------
        }
        [TestMethod]
        public async Task Generate_GivenActivityType_CardSharedExternal_ShouldGenerate()
        {
            //--- Arrange ----------------------------------------------------
            var activityType = EnumActivity.CardSharedExternal;
            var userActivities = await _generator.GetActivities(20, MockDataGenerator.UserId, activityType);
            var juliaActivities = await _generator.GetActivities(20, MockDataGenerator.JuliaDoe, activityType);
            var simonActivities = await _generator.GetActivities(20, MockDataGenerator.SimonDoe, activityType);
            var berthaActivities = await _generator.GetActivities(20, MockDataGenerator.BerthaDoe, activityType);
            //--- Execute ----------------------------------------------------
            foreach (var activity in userActivities.Union(juliaActivities).Union(simonActivities).Union(berthaActivities))
            {
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
            }
            //--- Assert ----------------------------------------------------
        }
        [TestMethod]
        public async Task Generate_GivenActivityType_CardSharedInternal_ShouldGenerate()
        {
            //--- Arrange ----------------------------------------------------
            var activityType = EnumActivity.CardSharedInternal;
            var userActivities = await _generator.GetActivities(20, MockDataGenerator.UserId, activityType);
            var juliaActivities = await _generator.GetActivities(20, MockDataGenerator.JuliaDoe, activityType);
            var simonActivities = await _generator.GetActivities(20, MockDataGenerator.SimonDoe, activityType);
            var berthaActivities = await _generator.GetActivities(20, MockDataGenerator.BerthaDoe, activityType);
            //--- Execute ----------------------------------------------------
            foreach (var activity in userActivities.Union(juliaActivities).Union(simonActivities).Union(berthaActivities))
            {
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
            }
            //--- Assert ----------------------------------------------------
        }
        [TestMethod]
        public async Task Generate_GivenActivityType_CardSharedOnBehalf_ShouldGenerate()
        {
            //--- Arrange ----------------------------------------------------
            var activityType = EnumActivity.CardSharedOnBehalf;
            var userActivities = await _generator.GetActivities(20, MockDataGenerator.UserId, activityType);
            var juliaActivities = await _generator.GetActivities(20, MockDataGenerator.JuliaDoe, activityType);
            var simonActivities = await _generator.GetActivities(20, MockDataGenerator.SimonDoe, activityType);
            var berthaActivities = await _generator.GetActivities(20, MockDataGenerator.BerthaDoe, activityType);
            //--- Execute ----------------------------------------------------
            foreach (var activity in userActivities.Union(juliaActivities).Union(simonActivities).Union(berthaActivities))
            {
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
            }
            //--- Assert ----------------------------------------------------
        }
        [TestMethod]
        public async Task Generate_GivenActivityType_RequestToConnect_ShouldGenerate()
        {
            //--- Arrange ----------------------------------------------------
            var activityType = EnumActivity.RequestToConnect;
            var userActivities = await _generator.GetActivities(20, MockDataGenerator.UserId, activityType);
            var juliaActivities = await _generator.GetActivities(20, MockDataGenerator.JuliaDoe, activityType);
            var simonActivities = await _generator.GetActivities(20, MockDataGenerator.SimonDoe, activityType);
            var berthaActivities = await _generator.GetActivities(20, MockDataGenerator.BerthaDoe, activityType);
            //--- Execute ----------------------------------------------------
            foreach (var activity in userActivities.Union(juliaActivities).Union(simonActivities).Union(berthaActivities))
            {
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
            }
            //--- Assert ----------------------------------------------------
        }
        [TestMethod]
        public async Task Generate_GivenActivityType_EmailClick_ShouldGenerate()
        {
            //--- Arrange ----------------------------------------------------
            var activityType = EnumActivity.EmailClick;
            var userActivities = await _generator.GetActivities(20, MockDataGenerator.UserId, activityType);
            var juliaActivities = await _generator.GetActivities(20, MockDataGenerator.JuliaDoe, activityType);
            var simonActivities = await _generator.GetActivities(20, MockDataGenerator.SimonDoe, activityType);
            var berthaActivities = await _generator.GetActivities(20, MockDataGenerator.BerthaDoe, activityType);
            //--- Execute ----------------------------------------------------
            foreach (var activity in userActivities.Union(juliaActivities).Union(simonActivities).Union(berthaActivities))
            {
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
            }
            //--- Assert ----------------------------------------------------
        }
        [TestMethod]
        public async Task Generate_GivenActivityType_WebsiteClick_ShouldGenerate()
        {
            //--- Arrange ----------------------------------------------------
            var activityType = EnumActivity.WebsiteClick;
            var userActivities = await _generator.GetActivities(20, MockDataGenerator.UserId, activityType);
            var juliaActivities = await _generator.GetActivities(20, MockDataGenerator.JuliaDoe, activityType);
            var simonActivities = await _generator.GetActivities(20, MockDataGenerator.SimonDoe, activityType);
            var berthaActivities = await _generator.GetActivities(20, MockDataGenerator.BerthaDoe, activityType);
            //--- Execute ----------------------------------------------------
            foreach (var activity in userActivities.Union(juliaActivities).Union(simonActivities).Union(berthaActivities))
            {
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
            }
            //--- Assert ----------------------------------------------------
        }
        [TestMethod]
        public async Task Generate_GivenActivityType_FacebookClick_ShouldGenerate()
        {
            //--- Arrange ----------------------------------------------------
            var activityType = EnumActivity.FacebookClick;
            var userActivities = await _generator.GetActivities(26, MockDataGenerator.UserId, activityType);
            var juliaActivities = await _generator.GetActivities(26, MockDataGenerator.JuliaDoe, activityType);
            var simonActivities = await _generator.GetActivities(26, MockDataGenerator.SimonDoe, activityType);
            var berthaActivities = await _generator.GetActivities(26, MockDataGenerator.BerthaDoe, activityType);
            //--- Execute ----------------------------------------------------
            foreach (var activity in userActivities.Union(juliaActivities).Union(simonActivities).Union(berthaActivities))
            {
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
            }
            //--- Assert ----------------------------------------------------
        }
        [TestMethod]
        public async Task Generate_GivenActivityType_TwitterClick_ShouldGenerate()
        {
            //--- Arrange ----------------------------------------------------
            var activityType = EnumActivity.FacebookClick;
            var userActivities = await _generator.GetActivities(12, MockDataGenerator.UserId, activityType);
            var juliaActivities = await _generator.GetActivities(12, MockDataGenerator.JuliaDoe, activityType);
            var simonActivities = await _generator.GetActivities(12, MockDataGenerator.SimonDoe, activityType);
            var berthaActivities = await _generator.GetActivities(12, MockDataGenerator.BerthaDoe, activityType);
            //--- Execute ----------------------------------------------------
            foreach (var activity in userActivities.Union(juliaActivities).Union(simonActivities).Union(berthaActivities))
            {
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
            }
            //--- Assert ----------------------------------------------------
        }
        [TestMethod]
        public async Task Generate_GivenActivityType_InstagramClick_ShouldGenerate()
        {
            //--- Arrange ----------------------------------------------------
            var activityType = EnumActivity.InstagramClick;
            var userActivities = await _generator.GetActivities(12, MockDataGenerator.UserId, activityType);
            var juliaActivities = await _generator.GetActivities(12, MockDataGenerator.JuliaDoe, activityType);
            var simonActivities = await _generator.GetActivities(12, MockDataGenerator.SimonDoe, activityType);
            var berthaActivities = await _generator.GetActivities(12, MockDataGenerator.BerthaDoe, activityType);
            //--- Execute ----------------------------------------------------
            foreach (var activity in userActivities.Union(juliaActivities).Union(simonActivities).Union(berthaActivities))
            {
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
            }
            //--- Assert ----------------------------------------------------
        }
        [TestMethod]
        public async Task Generate_GivenActivityType_YouTubeClick_ShouldGenerate()
        {
            //--- Arrange ----------------------------------------------------
            var activityType = EnumActivity.YouTubeClick;
            var userActivities = await _generator.GetActivities(13, MockDataGenerator.UserId, activityType);
            var juliaActivities = await _generator.GetActivities(13, MockDataGenerator.JuliaDoe, activityType);
            var simonActivities = await _generator.GetActivities(15, MockDataGenerator.SimonDoe, activityType);
            var berthaActivities = await _generator.GetActivities(15, MockDataGenerator.BerthaDoe, activityType);
            //--- Execute ----------------------------------------------------
            foreach (var activity in userActivities.Union(juliaActivities).Union(simonActivities).Union(berthaActivities))
            {
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
            }
            //--- Assert ----------------------------------------------------
        }
        [TestMethod]
        public async Task Generate_GivenActivityType_LinkedInClick_ShouldGenerate()
        {
            //--- Arrange ----------------------------------------------------
            var activityType = EnumActivity.LinkedInClick;
            var userActivities = await _generator.GetActivities(34, MockDataGenerator.UserId, activityType);
            var juliaActivities = await _generator.GetActivities(35, MockDataGenerator.JuliaDoe, activityType);
            var simonActivities = await _generator.GetActivities(35, MockDataGenerator.SimonDoe, activityType);
            var berthaActivities = await _generator.GetActivities(34, MockDataGenerator.BerthaDoe, activityType);
            //--- Execute ----------------------------------------------------
            foreach (var activity in userActivities.Union(juliaActivities).Union(simonActivities).Union(berthaActivities))
            {
                await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.Activities, activity);
            }
            //--- Assert ----------------------------------------------------
        }

        // EmailClick,
        // WebsiteClick,
        // ShareClick,
        // ProfileClick,
        // FacebookClick,
        // TwitterClick,
        // InstagramClick,
        // LinkedInClick,
        // YouTubeClick,
        // //
        // //Card is shared with external people via email
        // CardSharedExternal,
        // //Card is shared with other members of the application
        // CardSharedInternal,
        // //User shares card that belongs to someone else with a member of the application
        // CardSharedOnBehalf,
        // //Request accepted by user
        // RequestAccepted,
        // //Request to connect with user
        // RequestToConnect,
        // //Number of times card is viewed
        // CardViewed
    }
}

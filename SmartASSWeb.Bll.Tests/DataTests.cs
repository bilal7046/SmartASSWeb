using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Faker;
using Google.Cloud.Firestore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartASSWeb.Bll._Mock;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Bll.Tests
{
    [TestClass]
    public class DataTests
    {
        private IFirebaseService _service;

        #region Initialize & CleanUp

        [TestInitialize]
        public void TestInitialize()
        {
            _service = new FirebaseService(new TestKeyFileResolver());
        }
        [TestCleanup]
        public void TestClean()
        {
            _service = null;
        }

        #endregion
        [TestMethod]
        public async Task Leads_GivenAUser_ShouldReturnCorrectNumber()
        {
            //--- Arrange ----------------------------------------------------
            var teamMemberLeads = await _service.GetCollection<Lead>(FirestoreTableStore.Leads, "userId", MockDataGenerator.JuliaDoe);
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;
            var filteredLeads = teamMemberLeads.Where(c => c.DateCreated >= Timestamp.FromDateTime(startDate.ToUniversalTime()) && c.DateCreated <= Timestamp.FromDateTime(endDate.ToUniversalTime()))
                                                    .GroupBy(p => p.LeadStatus)
                                                    .Select(p => new ChartData
                                                    {
                                                        Key = p.Key,
                                                        Value = p.Count()
                                                    }).ToList();
            //--- Execute ----------------------------------------------------

            //--- Assert ----------------------------------------------------

        }
        [TestMethod]
        public async Task Leads_GivenPhoneNumber_ShouldUpdate()
        {
            //--- Arrange ----------------------------------------------------
            var leads = await _service.GetCollection<Lead>(FirestoreTableStore.Leads);
            foreach (var lead in leads)
            {
                await _service.Update(lead.LeadId, FirestoreTableStore.Leads, "phone", Faker.PhoneFaker.Phone());
                await _service.Update(lead.LeadId, FirestoreTableStore.Leads, "cellphone", Faker.PhoneFaker.Phone());
            }
            //--- Execute ----------------------------------------------------

            //--- Assert ----------------------------------------------------

        }

        [TestMethod]
        public void GeneratePhoneNumber_Given_ShouldGenerate()
        {
            //--- Arrange ----------------------------------------------------
            for (int i = 0; i < 1000; i++)
            {
                Debug.WriteLine(Faker.PhoneFaker.Phone());
            }
            //--- Execute ----------------------------------------------------

            //--- Assert ----------------------------------------------------

        }
    }
}

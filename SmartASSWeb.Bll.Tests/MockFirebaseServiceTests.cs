using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartASSWeb.Bll._Mock;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Bll.Tests
{
    [TestClass]
    public class MockFirebaseServiceTests
    {
        [TestMethod]
        public async Task GetTable_GivenTableName_ShouldReturnData()
        {
            //--- Arrange ----------------------------------------------------
            IFirebaseService service = new MockFirebaseService();
            //--- Execute ----------------------------------------------------
            var userProfiles = await service.GetCollection<UserProfile>(FirestoreTableStore.UserProfiles);
            //--- Assert ----------------------------------------------------
            Assert.IsNotNull(userProfiles);
            Assert.IsTrue(userProfiles.Count() == 5);
        }
    }
}

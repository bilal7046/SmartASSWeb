using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartASSWeb.Bll;

namespace SmartASSWeb.Core.Service.Tests
{
    [TestClass]
    public class FirebaseTests
    {
        [TestMethod]
        public async Task GetCollection_GivenValidName_ShouldReturnData()
        {
            //--- Arrange ----------------------------------------------------
            IFirebaseService service = GetService();
            //--- Execute ----------------------------------------------------
            var data = await service.GetCollection<UserProfile>("user-profile");
            //--- Assert ----------------------------------------------------
            Debug.WriteLine("Count: " + data.Count());
            Debug.WriteLine("First Person Name: " + data.First().Email);
            Assert.IsNotNull(data);
        }

        #region Factory Methods

        private static FirebaseService GetService()
        {
            var keyFilePath = @"C:\Users\mafad\source\repos\SmartASSWeb\SmartASSWeb.Core.Service.Tests\smartass.json";
            var mockFileResolver = new Mock<IKeyFileResolver>();
            mockFileResolver.Setup(c => c.GetKeyFilePath()).Returns(keyFilePath);
            return new FirebaseService(mockFileResolver.Object);
        }

        #endregion
    }
}

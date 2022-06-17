using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using SmartASSWeb.Bll.Services;

namespace SmartASSWeb.Bll.Tests
{
    [TestClass]
    public class AddressServiceTests
    {
        [TestMethod]
        public async Task GetCountries_ShouldReturnAllCountries()
        {
            //--- Arrange ----------------------------------------------------
            IAddressService service = new AddressService();
            //--- Execute ----------------------------------------------------
            var countries = await service.GetCountries();
            //--- Assert ----------------------------------------------------
            Assert.IsNotNull(countries);
        }
    }
}

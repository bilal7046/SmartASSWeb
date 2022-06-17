using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Bll.Tests
{
    public class TestKeyFileResolver
        : IKeyFileResolver
    {
        public string GetKeyFilePath()
        {
            return @"C:\Users\mafad\source\repos\SmartASSWeb\SmartASSWeb.Bll.Tests\smartass.json";
        }
    }
}
using System.Web;
using Google.Api;

namespace SmartASSWeb.Core.Service
{
    public interface IKeyFileResolver
    {
        string GetKeyFilePath();
    }
    public class KeyFileResolver
        : IKeyFileResolver
    {
        public string GetKeyFilePath()
        {
            return HttpContext.Current == null 
                        ? "https://smartass.global/smartass.json" 
                        : HttpContext.Current.Server.MapPath("~/smartass.json");
        }
    }
}

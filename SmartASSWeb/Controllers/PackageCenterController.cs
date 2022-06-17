using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using RestSharp;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Core;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Controllers
{
    public class PackageCenterController 
        : ControllerBase
    {
        
        private readonly IFirebaseService _service;

        public PackageCenterController(IFirebaseService service)
        {
            _service = service;
        }

        // GET: PackageCenter
        public ActionResult Index()
        {
           var ipAddress = GetIp();
            string url = string.Format("http://www.geoplugin.net/json.gp?ip={0}", ipAddress);
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                Location location = new JavaScriptSerializer().Deserialize<Location>(json);

                ViewBag.Country = location.Geoplugin_CountryName;
            }
            return View();
        }
        
        public async Task<ActionResult> GetStarted(decimal amount)
        {

            var user = await _service.Get<UserProfile>(FirestoreTableStore.UserProfiles, "userId", User.UserId);

            var client = new RestClient("https://secure.paygate.co.za/payweb3/initiate.trans") { Timeout = -1 };
            var request = new RestRequest(Method.POST) { AlwaysMultipartFormData = true };
            request.AddParameter("PAYGATE_ID", WebConfigurationManager.AppSettings["PayGateID"]);
            request.AddParameter("REFERENCE", user.UserId);
            request.AddParameter("AMOUNT", amount);
            request.AddParameter("CURRENCY", "ZAR");
            request.AddParameter("RETURN_URL", "https://my.return.url/page");// $"{Request.Url.Scheme}://{Request.Url.Authority}/packagecenter/completepayment"
            request.AddParameter("TRANSACTION_DATE", DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss"));
            request.AddParameter("LOCALE", "en-za");
            request.AddParameter("COUNTRY", "ZAF");
            request.AddParameter("EMAIL", "customer@paygate.co.za");
            request.AddParameter("CHECKSUM", HashExtensions.GetMd5Hash(request.Parameters, WebConfigurationManager.AppSettings["PayGateSecret"]));
            IRestResponse response = await client.ExecuteAsync(request);
            
            return Json(response.Content);
        }

        [HttpGet]
        public ActionResult CompletePayment()
        {
            return View();
        }

        [NonAction]
        public string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
    }
    public class Location
    {
        public string Geoplugin_Request { get; set; }
        public string Geoplugin_CountryName { get; set; }
        public string Geoplugin_CountryCode { get; set; }
        public string Geoplugin_CurrencyCode { get; set; }

    }
}
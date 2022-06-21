using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using RestSharp;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Core;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Controllers
{
    public class PackageCenterController
        : ControllerBase
    {
        private string Country;
        private string Currency;
        private string CurrencyCode;

        private double SmallBusiness = 3.49;
        private double BusinessPack = 6.00;
        private readonly IFirebaseService _service;

        public PackageCenterController(IFirebaseService service)
        {
            _service = service;
        }

        // GET: PackageCenter
        public ActionResult Index()
        {
            ViewBag.SmallBusiness = 3.49;
            ViewBag.BusinessPack = 6.00;
            ViewBag.CurrencySymbol = "$";

            var ipAddress = GetIp();
            string url = string.Format("http://www.geoplugin.net/json.gp?ip={0}", ipAddress);
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                Location location = new JavaScriptSerializer().Deserialize<Location>(json);
               
                Currency = location.Geoplugin_CurrencyCode;
                if (Currency!=null)
                {
                    if (Currency.ToUpper() == "EUR")
                    {
                        ViewBag.SmallBusiness = CurrencyConverter(Currency, "USD", SmallBusiness);
                        ViewBag.BusinessPack = CurrencyConverter(Currency, "USD", BusinessPack);
                    
                        ViewBag.CurrencySymbol = "&#x20AC;";
                    }
                    if (Currency.ToUpper() == "GBP")
                    {
                        ViewBag.CurrencySymbol = "&#163;";
                        ViewBag.SmallBusiness = CurrencyConverter(Currency, "USD", SmallBusiness);
                        ViewBag.BusinessPack = CurrencyConverter(Currency, "USD", BusinessPack);
                    }
                }
                

               
            }
            return View();
        }

        public ActionResult UpdatePackageValue(double val, int type)
        {
            ViewBag.SmallBusiness = 3.49;
            ViewBag.BusinessPack = 6.00;

           
            if (val == 12 && type == 1)
            {
                ViewBag.SmallBusiness = ViewBag.SmallBusiness * 12;
                return Json(ViewBag.SmallBusiness,JsonRequestBehavior.AllowGet);
            }
            else if (val == 12 && type == 2)
            {
                ViewBag.BusinessPack = ViewBag.BusinessPack * 12;
                return Json(ViewBag.BusinessPack, JsonRequestBehavior.AllowGet);
            }
            return Json("",JsonRequestBehavior.AllowGet);
            
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
        [NonAction]
        public double CurrencyConverter(string from, string to, double amount)
        {
            var client = new RestClient("https://api.apilayer.com/exchangerates_data/convert?to=" + to + "from=" + from + "&amount=" + amount + "");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("apikey", "6yGMGAc2anjphNsPDaxrzwYNKOVlf7xC");
            IRestResponse response = client.Execute(request);
            var a = JsonConvert.DeserializeObject<Root>(response.Content);
            return a.result;
        }
    }
    public class Location
    {
        public string Geoplugin_Request { get; set; }
        public string Geoplugin_CountryName { get; set; }
        public string Geoplugin_CountryCode { get; set; }
        public string Geoplugin_CurrencyCode { get; set; }

    }

    public class Info
    {
        public int timestamp { get; set; }
        public double rate { get; set; }
    }

    public class Query
    {
        public string from { get; set; }
        public string to { get; set; }
        public double amount { get; set; }
    }

    public class Root
    {
        public bool success { get; set; }
        public Query query { get; set; }
        public Info info { get; set; }
        public string date { get; set; }
        public double result { get; set; }
    }



}
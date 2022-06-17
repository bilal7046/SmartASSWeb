using System.Web.Mvc;

namespace SmartASSWeb.Controllers
{
    public class StatisticsController 
        : ControllerBase
    {
        // GET: Statistics
        public ActionResult Index()
        {
            return View();
        }
    }
}
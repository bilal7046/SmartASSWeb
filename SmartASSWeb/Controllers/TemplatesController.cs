using System.Web.Mvc;

namespace SmartASSWeb.Controllers
{
    public class TemplatesController 
        : ControllerBase
    {
        // GET: Templates
        public ActionResult Index()
        {
            return View();
        }
    }
}
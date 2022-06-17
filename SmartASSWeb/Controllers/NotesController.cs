using System.Web.Mvc;

namespace SmartASSWeb.Controllers
{
    public class NotesController 
        : ControllerBase
    {
        // GET: Notes
        public ActionResult Index()
        {
            return View();
        }
    }
}
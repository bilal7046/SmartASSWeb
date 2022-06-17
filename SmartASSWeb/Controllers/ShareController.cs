using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartASSWeb.Controllers
{
    public class ShareController 
        : ControllerBase
    {
        // GET: Share
        public ActionResult Index()
        {
            return View();
        }
    }
}
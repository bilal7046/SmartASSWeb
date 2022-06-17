using System.Web.Mvc;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Core;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Controllers
{
    public class AffiliatesController 
        : ControllerBase
    {
        private readonly IFirebaseService _service;
        private readonly IUniqueCodeGenerator _codeGenerator;

        public AffiliatesController(IFirebaseService service, IUniqueCodeGenerator codeGenerator)
        {
            _service = service;
            _codeGenerator = codeGenerator;
        }

        // GET: Affiliates
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            var profile = new AffiliateProfile
            {
                AffiliateCode = _codeGenerator.Generate(6),
                AffiliateAccountStatus = "Pending"
            };
            return View(profile);
        }

        [HttpPost]
        public ActionResult SignUp(AffiliateProfile profile)
        {
            return RedirectToAction("Index");
        }
    }
}
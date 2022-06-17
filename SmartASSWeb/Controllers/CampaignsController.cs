using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Core;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Controllers
{
    public class CampaignsController 
        : ControllerBase
    {
        private readonly IFirebaseService _service;

        #region Ctor

        public CampaignsController(IFirebaseService service)
        {
            _service = service;
        }

        #endregion

        // GET: Leads
        public async Task<ActionResult> Index()
        {
            var campaigns = await _service.GetCollection<Campaign>("campaigns");
            return View(campaigns);
        }

        public async Task<ActionResult> Editor(string campaignId)
        {
            if (string.IsNullOrEmpty(campaignId))
            {
                var newCampaign = new Campaign();
                return View(newCampaign);
            }
            else
            {
                var data = await _service.GetCollection<Campaign>("campaigns", "campaignId", campaignId);
                return View(data.FirstOrDefault());
            }
        }

        [HttpPost]
        public ActionResult Save(Campaign campaign)
        {
            if (string.IsNullOrEmpty(campaign.CampaignId))
            {
                _service.Add(Guid.NewGuid().ToString(), "campaigns", campaign);
            }
            else
            {
                _service.Update(campaign.CampaignId, "campaigns", campaign.ToDictionary());
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            _service.Delete(id, "campaigns");

            return View("Index");
        }
    }
}
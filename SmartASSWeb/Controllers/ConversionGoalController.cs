using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Google.Cloud.Firestore;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Core;
using SmartASSWeb.Core.Service;
using SmartASSWeb.ViewModels;

namespace SmartASSWeb.Controllers
{
    [SmartAssAuthorize(Roles = Common.SMALLBUSINESS)]
    public class ConversionGoalController 
        : ControllerBase
    {
        private readonly IFirebaseService _service;
        private readonly ILookupDictionary _lookupDictionary;

        public ConversionGoalController(IFirebaseService service, ILookupDictionary lookupDictionary)
        {
            _service = service;
            _lookupDictionary = lookupDictionary;
        }

        #region MyConvervsion

        // GET: ConversionGoal
        public async Task<ActionResult> MyConversionGoals()
        {
            var goals = await _service.GetCollection<ConversionGoal>(FirestoreTableStore.ConversionGoals, "userId", User.UserId);
            var model = new ConversionGoalViewModel
            {
                ConversionGoals = goals,
                ConversionNameList = _lookupDictionary.ConversionNameList.Where(p=> !goals.All(c=> c.Name != p.Key)).ToList(),
                ConversionPeriodList = _lookupDictionary.ConversionPeriodList.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> SaveConversionGoals(ConversionGoal conversionGoal)
        {
            var goal = new ConversionGoal
            {
                UserId = User.UserId,
                Name = conversionGoal.Name,
                ConversionValue = conversionGoal.ConversionValue,
                Period = conversionGoal.Period,
                Start = Timestamp.FromDateTime(conversionGoal.StartDate.ToUniversalTime()),
                End = Timestamp.FromDateTime(conversionGoal.EndDate.ToUniversalTime())
            };
            await _service.Add(Guid.NewGuid().ToString(), FirestoreTableStore.ConversionGoals, goal);
            //
            var goals = await _service.GetCollection<ConversionGoal>(FirestoreTableStore.ConversionGoals, "userId", User.UserId);
            var model = new ConversionGoalViewModel
            {
                ConversionGoals = goals,
                ConversionNameList = _lookupDictionary.ConversionNameList.Where(p => !goals.All(c => c.Name != p.Key)).ToList(),
                ConversionPeriodList = _lookupDictionary.ConversionPeriodList.ToList()
            };
            return PartialView("MyConversionsGoalPanel", model);
        }

        [HttpPost]
        public async Task<JsonResult> RemoveConversionGoal(string id)
        {
            await _service.Delete(id, FirestoreTableStore.ConversionGoals);

            var goals = await _service.GetCollection<ConversionGoal>(FirestoreTableStore.ConversionGoals, "userId", User.UserId);
            var model = new ConversionGoalViewModel
            {
                ConversionGoals = goals,
                ConversionNameList = _lookupDictionary.ConversionNameList.Where(p => !goals.All(c => c.Name != p.Key)).ToList(),
                ConversionPeriodList = _lookupDictionary.ConversionPeriodList.ToList()
            };
            return Json(model);
        }

        [HttpGet]
        public async Task<JsonResult> GetNames()
        {
            var goals = await _service.GetCollection<ConversionGoal>(FirestoreTableStore.ConversionGoals, "userId", User.UserId);
            var list = _lookupDictionary.ConversionNameList.Where(p => goals.All(c => c.Name != p.Key)).Select(p=>p.Key).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region TeamConvervsion

        // GET: ConversionGoal
        public async Task<ActionResult> TeamConversionGoals()
        {
            var teamMemberGoal = await _service.Get<TeamMemberGoal>(FirestoreTableStore.TeamMemberGoals, User.UserId);
            return View(teamMemberGoal);
        }

        [HttpPost]
        public async Task<PartialViewResult> SaveTeamConversionGoals(TeamMemberGoal teamMemberGoal)
        {
            var goal = await _service.Get<TeamMemberGoal>(FirestoreTableStore.TeamMemberGoals, User.UserId);
            if (goal == null)
            {
                await _service.Add(User.UserId, FirestoreTableStore.TeamMemberGoals, teamMemberGoal);
            }
            else
            {
                await _service.Update(User.UserId, FirestoreTableStore.TeamMemberGoals, teamMemberGoal.ToDictionary());
            }
            //
            var model = await _service.Get<TeamMemberGoal>(FirestoreTableStore.TeamMemberGoals, User.UserId);
            return PartialView("TeamConversionsGoalPanel", model);
        }

        #endregion
    }
}
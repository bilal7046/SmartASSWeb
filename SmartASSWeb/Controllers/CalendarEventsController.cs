using System.Threading.Tasks;
using System.Web.Mvc;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Core;
using SmartASSWeb.Core.Service;
using SmartASSWeb.ViewModels;

namespace SmartASSWeb.Controllers
{
    [SmartAssAuthorize(Roles = Common.INDIVIDUAL)]
    public class CalendarEventsController 
        : ControllerBase
    {
        #region Ctor

        private readonly IFirebaseService _service;

        public CalendarEventsController(IFirebaseService service)
        {
            _service = service;
        }

        #endregion

        // GET: Events
        public async Task<ActionResult> Index()
        {
            var events = await _service.GetCollection<CalendarEvent>(FirestoreTableStore.CalendarEvents, "userId", "UserId");
            var model = new CalendarEventViewModel
            {
                CalendarEvents = events
            };
            return View(model);
        }
        public async Task<ActionResult> Meetings()
        {
            var events = await _service.GetCollection<CalendarEvent>(FirestoreTableStore.CalendarEvents, "userId", "UserId");
            var model = new CalendarEventViewModel
            {
                CalendarEvents = events
            };
            return View(model);
        }

        public async Task<ActionResult> AddEvent(CalendarEvent calendarEvent)
        {
            await _service.Add(FirestoreTableStore.CalendarEvents, calendarEvent);
            return Json("Meeting Request created");
        }
        public async Task<ActionResult> UpdateEvent(CalendarEvent calendarEvent)
        {
            await _service.Update(calendarEvent.CalendarEventId, FirestoreTableStore.CalendarEvents, calendarEvent.ToDictionary());
            return Json("Meeting Request saved");
        }

        // public virtual async Task<JsonResult> Basic_Usage_Read([DataSourceRequest] DataSourceRequest request)
        // {
        //     var events = await _service.GetCollection<CalendarEvent>(FirestoreTableStore.CalendarEvents, "userId", User.UserId);
        //     return Json(await events.ToDataSourceResultAsync(request));
        // }
        //
        // public virtual async Task<JsonResult> Basic_Usage_Destroy([DataSourceRequest] DataSourceRequest request, CalendarEvent calendarEvent)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         await _service.Delete(calendarEvent.CalendarEventId, FirestoreTableStore.CalendarEvents);
        //     }
        //
        //     return Json(await new[] { calendarEvent }.ToDataSourceResultAsync(request, ModelState));
        // }
        //
        // public virtual async Task<JsonResult> Basic_Usage_Create([DataSourceRequest] DataSourceRequest request, CalendarEvent calendarEvent)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         calendarEvent.UserId = User.UserId;
        //         await _service.Add(FirestoreTableStore.CalendarEvents, calendarEvent);
        //     }
        //
        //     return Json(await new[] { calendarEvent }.ToDataSourceResultAsync(request, ModelState));
        // }
        //
        // public virtual async Task<JsonResult> Basic_Usage_Update([DataSourceRequest] DataSourceRequest request, CalendarEvent calendarEvent)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         await _service.Update(calendarEvent.CalendarEventId, FirestoreTableStore.CalendarEvents, calendarEvent.ToDictionary());
        //     }
        //
        //     return Json(await new[] { calendarEvent }.ToDataSourceResultAsync(request, ModelState));
        // }
    }
}
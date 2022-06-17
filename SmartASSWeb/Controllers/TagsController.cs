using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Core;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Controllers
{
    public class TagsController 
        : Controller
    {
        private readonly IFirebaseService _service;
        private string _tableStore;

        #region Ctor

        public TagsController(IFirebaseService service)
        {
            _service = service;
        }

        #endregion

        public async Task<ActionResult> Index()
        {
            _tableStore = FirestoreTableStore.Tags;
            var tags = await _service.GetCollection<Tag>(_tableStore);
            return View(tags);
        }

        public async Task<ActionResult> Editor(string tagId)
        {
            if (string.IsNullOrEmpty(tagId))
            {
                var obj = new Tag();
                return View(obj);
            }
            else
            {
                var col = await _service.GetCollection<Tag>(_tableStore, "tagId", tagId);
                return View(col.FirstOrDefault());
            }
        }

        [HttpPost]
        public ActionResult Save(Tag obj)
        {
            if (string.IsNullOrEmpty(obj.TagId))
            {
                _service.Add(Guid.NewGuid().ToString(), _tableStore, obj);
            }
            else
            {
                _service.Update(obj.TagId, _tableStore, obj.ToDictionary());
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(string tagId)
        {
            _service.Delete(tagId, _tableStore);

            return View("Index");
        }
    }
}
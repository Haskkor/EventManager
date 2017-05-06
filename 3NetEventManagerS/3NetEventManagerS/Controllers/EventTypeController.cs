using System.Web.Mvc;
using _3NetEventManagerS.Models;

namespace _3NetEventManagerS.Controllers
{   
    public class EventTypeController : Controller
    {
		private readonly IEventTypeRepository _eventtypeRepository;

        public EventTypeController() : this(new EventTypeRepository())
        {
        }

        public EventTypeController(IEventTypeRepository eventtypeRepository)
        {
			_eventtypeRepository = eventtypeRepository;
        }

        public ViewResult Index()
        {
            return View(_eventtypeRepository.All);
        }

        public ViewResult Details(int id)
        {
            return View(_eventtypeRepository.Find(id));
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(EventType eventtype)
        {
            if (!ModelState.IsValid) return View();
            _eventtypeRepository.InsertOrUpdate(eventtype);
            _eventtypeRepository.Save();
            if (Session != null)
            {
                return Session["LoggedUserRole"].Equals("Admin") ? RedirectToAction("Index") : RedirectToAction("UserManager", "User", new { id = Session["LoggedUserId"] });
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
             return View(_eventtypeRepository.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(EventType eventtype)
        {
            if (ModelState.IsValid) {
                _eventtypeRepository.InsertOrUpdate(eventtype);
                _eventtypeRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View(_eventtypeRepository.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _eventtypeRepository.Delete(id);
            _eventtypeRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _eventtypeRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


using System.Web.Mvc;
using _3NetEventManagerS.Models;

namespace _3NetEventManagerS.Controllers
{   
    public class EventStatusController : Controller
    {
		private readonly IEventStatusRepository _eventstatusRepository;

        public EventStatusController() : this(new EventStatusRepository())
        {
        }

        public EventStatusController(IEventStatusRepository eventstatusRepository)
        {
			_eventstatusRepository = eventstatusRepository;
        }

        public ViewResult Index()
        {
            return View(_eventstatusRepository.All);
        }

        public ViewResult Details(int id)
        {
            return View(_eventstatusRepository.Find(id));
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(EventStatus eventstatus)
        {
            if (ModelState.IsValid) {
                _eventstatusRepository.InsertOrUpdate(eventstatus);
                _eventstatusRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
             return View(_eventstatusRepository.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(EventStatus eventstatus)
        {
            if (ModelState.IsValid) {
                _eventstatusRepository.InsertOrUpdate(eventstatus);
                _eventstatusRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View(_eventstatusRepository.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _eventstatusRepository.Delete(id);
            _eventstatusRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _eventstatusRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


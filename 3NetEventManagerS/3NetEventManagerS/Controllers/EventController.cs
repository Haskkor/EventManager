using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using _3NetEventManagerS.Models;

namespace _3NetEventManagerS.Controllers
{   
    public class EventController : Controller
    {
		private readonly IEventStatusRepository _eventstatusRepository;
		private readonly IEventTypeRepository _eventtypeRepository;
		private readonly IEventRepository _eventRepository;

        public EventController() : this(new EventStatusRepository(), new EventTypeRepository(), new EventRepository())
        {
        }

        public EventController(IEventStatusRepository eventstatusRepository, IEventTypeRepository eventtypeRepository, IEventRepository eventRepository)
        {
			_eventstatusRepository = eventstatusRepository;
			_eventtypeRepository = eventtypeRepository;
			_eventRepository = eventRepository;
        }

        public ViewResult Index()
        {
            return View(_eventRepository.All);
        }

        public ViewResult UserEvents(int id)
        {
            IEnumerable<Event> iEnumerable = _eventRepository.All;
            var eventList = iEnumerable.Where(eventE => eventE.CreatorId == id).ToList();
            return View(eventList);
        }

        public ViewResult Details(int id)
        {
            return View(_eventRepository.Find(id));
        }

        public ActionResult Create()
        {
			ViewBag.PossibleEventStatus = _eventstatusRepository.All;
			ViewBag.PossibleEventType = _eventtypeRepository.All;
            return View();
        } 

        [HttpPost]
        public ActionResult Create(Event event1)
        {
            if (Session != null &&(!(Session["LoggedUserRole"].Equals("Admin"))))
            {
                event1.EventStatusId = 3;
                event1.CreatorId = (int)Session["LoggedUserId"];
            }
            if (ModelState.IsValid) {
                _eventRepository.InsertOrUpdate(event1);
                _eventRepository.Save();
                if (Session == null) return RedirectToAction("Index");
                return Session["LoggedUserRole"].Equals("Admin") ? RedirectToAction("Index") : RedirectToAction("UserEvents", "Event", new {id = Session["LoggedUserId"]});
            }
            ViewBag.PossibleEventStatus = _eventstatusRepository.All;
            ViewBag.PossibleEventType = _eventtypeRepository.All;
            return View();
        }
        
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleEventStatus = _eventstatusRepository.All;
			ViewBag.PossibleEventType = _eventtypeRepository.All;
             return View(_eventRepository.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Event event1)
        {
            if (ModelState.IsValid) {
                _eventRepository.InsertOrUpdate(event1);
                _eventRepository.Save();
                if (Session["LoggedUserRole"].Equals("Admin"))
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("UserEvents", "Event", new { id = Session["LoggedUserId"] });
            }
            ViewBag.PossibleEventStatus = _eventstatusRepository.All;
            ViewBag.PossibleEventType = _eventtypeRepository.All;
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View(_eventRepository.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _eventRepository.Delete(id);
            _eventRepository.Save();

            if (Session["LoggedUserRole"].Equals("Admin"))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("UserEvents", "Event", new { id = Session["LoggedUserId"] });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _eventstatusRepository.Dispose();
                _eventtypeRepository.Dispose();
                _eventRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


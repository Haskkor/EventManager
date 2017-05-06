using System.Web.Mvc;
using _3NetEventManagerS.Models;

namespace _3NetEventManagerS.Controllers
{   
    public class ContributionController : Controller
    {
		private readonly IContributionTypeRepository _contributiontypeRepository;
		private readonly IEventRepository _eventRepository;
		private readonly IContributionRepository _contributionRepository;

        public ContributionController() : this(new ContributionTypeRepository(), new EventRepository(), new ContributionRepository())
        {
        }

        public ContributionController(IContributionTypeRepository contributiontypeRepository, IEventRepository eventRepository, IContributionRepository contributionRepository)
        {
			_contributiontypeRepository = contributiontypeRepository;
			_eventRepository = eventRepository;
			_contributionRepository = contributionRepository;
        }

        public ViewResult Index()
        {
            return View(_contributionRepository.All);
        }

        public ViewResult Details(int id)
        {
            return View(_contributionRepository.Find(id));
        }

        public ActionResult Create()
        {
			ViewBag.PossibleContributionType = _contributiontypeRepository.All;
			ViewBag.PossibleEvent = _eventRepository.All;
            return View();
        } 

        [HttpPost]
        public ActionResult Create(Contribution contribution)
        {
            if (ModelState.IsValid) {
                _contributionRepository.InsertOrUpdate(contribution);
                _contributionRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleContributionType = _contributiontypeRepository.All;
            ViewBag.PossibleEvent = _eventRepository.All;
            return View();
        }
        
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleContributionType = _contributiontypeRepository.All;
			ViewBag.PossibleEvent = _eventRepository.All;
             return View(_contributionRepository.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Contribution contribution)
        {
            if (ModelState.IsValid) {
                _contributionRepository.InsertOrUpdate(contribution);
                _contributionRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleContributionType = _contributiontypeRepository.All;
            ViewBag.PossibleEvent = _eventRepository.All;
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View(_contributionRepository.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _contributionRepository.Delete(id);
            _contributionRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _contributiontypeRepository.Dispose();
                _eventRepository.Dispose();
                _contributionRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


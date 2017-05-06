using System.Web.Mvc;
using _3NetEventManagerS.Models;

namespace _3NetEventManagerS.Controllers
{   
    public class ContributionTypeController : Controller
    {
		private readonly IContributionTypeRepository _contributiontypeRepository;

        public ContributionTypeController() : this(new ContributionTypeRepository())
        {
        }

        public ContributionTypeController(IContributionTypeRepository contributiontypeRepository)
        {
			_contributiontypeRepository = contributiontypeRepository;
        }

        public ViewResult Index()
        {
            return View(_contributiontypeRepository.All);
        }

        public ViewResult Details(int id)
        {
            return View(_contributiontypeRepository.Find(id));
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(ContributionType contributiontype)
        {
            if (ModelState.IsValid) {
                _contributiontypeRepository.InsertOrUpdate(contributiontype);
                _contributiontypeRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
             return View(_contributiontypeRepository.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(ContributionType contributiontype)
        {
            if (ModelState.IsValid) {
                _contributiontypeRepository.InsertOrUpdate(contributiontype);
                _contributiontypeRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View(_contributiontypeRepository.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _contributiontypeRepository.Delete(id);
            _contributiontypeRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _contributiontypeRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


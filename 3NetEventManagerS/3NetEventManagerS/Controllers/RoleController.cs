using System.Web.Mvc;
using _3NetEventManagerS.Models;

namespace _3NetEventManagerS.Controllers
{   
    public class RoleController : Controller
    {
		private readonly IRoleRepository _roleRepository;

        public RoleController() : this(new RoleRepository())
        {
        }

        public RoleController(IRoleRepository roleRepository)
        {
			_roleRepository = roleRepository;
        }

        public ViewResult Index()
        {
            return View(_roleRepository.All);
        }

        public ViewResult Details(int id)
        {
            return View(_roleRepository.Find(id));
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid) {
                _roleRepository.InsertOrUpdate(role);
                _roleRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
             return View(_roleRepository.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid) {
                _roleRepository.InsertOrUpdate(role);
                _roleRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View(_roleRepository.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _roleRepository.Delete(id);
            _roleRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _roleRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


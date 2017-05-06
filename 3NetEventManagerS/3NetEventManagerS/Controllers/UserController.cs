using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using _3NetEventManagerS.Models;
using User = _3NetEventManagerS.Models.User;

namespace _3NetEventManagerS.Controllers
{   
    public class UserController : Controller
    {
		private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private User _currentUser = new User();
        static List<User> _friendList; 

        public UserController() : this(new RoleRepository(), new UserRepository())
        {
        }

        public UserController(IRoleRepository roleRepository, IUserRepository userRepository)
        {
			_roleRepository = roleRepository;
			_userRepository = userRepository;
        }

        public ViewResult Index()
        {
            return View(_userRepository.All);
        }

        public ViewResult Details(int id)
        {
            return View(_userRepository.Find(id));
        }

        public bool FindUser(string nickname, string password)
        {
            IEnumerable<User> iEnumerable = _userRepository.All;
            foreach (var user in iEnumerable.Where(user => user.NickName == nickname && user.Password == password))
            {
                _currentUser = user;
                return true;
            }
            return false;
        }

        public User GetUser()
        {
            return _currentUser;
        }

        public ActionResult Create()
        {
			ViewBag.PossibleRole = _roleRepository.All;
            return View();
        } 

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid) {
                _userRepository.InsertOrUpdate(user);
                _userRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleRole = _roleRepository.All;
            return View();
        }
        
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleRole = _roleRepository.All;
             return View(_userRepository.Find(id));
        }

        public ActionResult UserManager(int id)
        {
            ViewBag.PossibleRole = _roleRepository.All;
            return View(_userRepository.Find(id));
        }

        [HttpPost]
        public ActionResult UserManager(User user)
        {
            user.UserId = (int) Session["LoggedUserId"];
            user.RoleId = (int) Session["LoggedUserRole"];
            _userRepository.InsertOrUpdate(user);
            _userRepository.Save();
            return RedirectToAction("UserManager", "User", new { id = Session["LoggedUserId"] });           
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid) {
                _userRepository.InsertOrUpdate(user);
                _userRepository.Save();
                return Session["LoggedUserRole"].Equals("Admin") ? RedirectToAction("Index") : RedirectToAction("UserManager", "User", new { id = Session["LoggedUserId"] });
            }
            ViewBag.PossibleRole = _roleRepository.All;
            return View();
        }

        public ActionResult FindFriend()
        {
            var userList = new List<User>();
            if (_friendList == null)
            {
                IEnumerable<User> iEnumerable = _userRepository.All.OrderBy(user => user.NickName);
                userList.AddRange(iEnumerable);
            }
            else
            {
                userList = _friendList;
                _friendList = null;
            }
            return View(userList);
        }

        public ActionResult FindOneFriend()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FindOneFriend(User name)
        {
            IEnumerable<User> iEnumerable = _userRepository.All;
            _friendList = new List<User>();
            foreach (var user in iEnumerable.Where(user => user.NickName.Equals(name.NickName) || user.FirstName.Equals(name.NickName) || user.LastName.Equals(name.NickName)))
            {
                _friendList.Add(user);
            }
            return RedirectToAction("FindFriend", "User");
        }

        public ActionResult Delete(int id)
        {
            return View(_userRepository.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _userRepository.Delete(id);
            _userRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _roleRepository.Dispose();
                _userRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
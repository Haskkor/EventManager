using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using _3NetEventManagerS.Models;

namespace _3NetEventManagerS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            var userController = new UserController();
            if (!userController.FindUser(user.NickName, user.Password)) return RedirectToAction("Index", "Home");
            var rolerep = new RoleRepository();
            var currentUser = userController.GetUser();
            Session["LoggedUserId"] = currentUser.UserId;
            Session["LoggedUserRole"] = rolerep.Find(currentUser.RoleId).Name;
            Session["LoggedUserFirstName"] = currentUser.FirstName;
            Session["LoggedUserLastName"] = currentUser.LastName;
            return RedirectToAction("UserManager", "User", new { id = Session["LoggedUserId"] });
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            var rolerep = new RoleRepository();
            var userRep = new UserRepository();
            IEnumerable<User> iEnumerable = userRep.All;
            var userController = new UserController();
            user.RoleId = 3;
            Session["LoggedUserId"] = iEnumerable.Count() + 1;
            Session["LoggedUserRole"] = rolerep.Find(user.RoleId).Name;
            Session["LoggedUserFirstName"] = user.FirstName;
            Session["LoggedUserLastName"] = user.LastName;
            userController.Create(user);
            return RedirectToAction("UserManager", "User", new { id = Session["LoggedUserId"] });
        }
    }
}
using Drive.DAL;
using Drive.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drive.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(String Login, String Password)
        {
            UserDAO dao = new UserDAO();

            var user = (from u in dao.GetAll()
                       where u.Login == Login && u.Password == Password
                       select u).FirstOrDefault();
         
            if (user != null)
            {
                SessionManager.User = user;
            }

            if(SessionManager.IsValidUser)
                return Redirect("~/Home");

            ViewBag.errorMessage = "Incorrect Login or Password";
            return View();
        }

        public ActionResult Logout()
        {
            SessionManager.ClearSession();
            return View("Login");
        }
    }
}
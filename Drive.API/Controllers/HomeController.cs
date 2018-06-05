using Drive.DAL;
using Drive.Entities;
using Drive.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drive.Controllers
{
    public class HomeController : Controller
    {
        [AuthorizedOnly]
        public ActionResult Index()
        {
            return View();
        }
    }
}
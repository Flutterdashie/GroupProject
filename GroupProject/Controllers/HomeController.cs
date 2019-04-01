using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupProject.Models;

namespace GroupProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            User model = new User();
            return View(model);
        }

        public ActionResult Login(User model)
        {
            throw new NotImplementedException();
        }
    }
}
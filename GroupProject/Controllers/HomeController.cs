using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.Owin;
using GroupProject.Models;
using Microsoft.Owin.Security;

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

        [HttpPost]
        public ActionResult Login(User model)
        {
            //TODO: Add further validation/specific model errors
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userMgr = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            var authMgr = HttpContext.GetOwinContext().Authentication;

            IdentityUser user = userMgr.Find(model.UserName, model.Password);

            if(user == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }

            var identity = userMgr.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            authMgr.SignIn(new AuthenticationProperties{ IsPersistent = true}, identity);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Logout()
        {
            var authMgr = HttpContext.GetOwinContext().Authentication;
            authMgr.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index");
        }
    }
}
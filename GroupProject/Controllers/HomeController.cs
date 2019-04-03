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
using GroupProject.Repos;
using Microsoft.Owin.Security;

namespace GroupProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var repo = new BlogRepository();
            return View(repo.GetAllPosts());
        }

        public ActionResult Login()
        {
            User model = new User();
            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model,string returnUrl)
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
            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Logout()
        {
            var authMgr = HttpContext.GetOwinContext().Authentication;
            authMgr.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index");
        }

        //add route
        [Route()]
        [AcceptVerbs("GET")]
        public ActionResult guestSearchByTitle(string term)
        {
            var repo = new BlogRepository();

            IEnumerable<BlogPost> found = repo.SearchByTitle(term);

            if (found == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(found);
        }

        //add route
        [Route()]
        [AcceptVerbs("GET")]
        public ActionResult guestSearchById(int Id)
        {
            var repo = new BlogRepository();

            IEnumerable<BlogPost> found = repo.SearchById(Id);

            if (found == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(found);
        }
    }
}
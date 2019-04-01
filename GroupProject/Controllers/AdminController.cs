using GroupProject.Models;
using GroupProject.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BlogPost blogPost)
        {
            var repo = new BlogRepository();
            repo.Insert(blogPost);
            return View("Index");
        }
    }
}
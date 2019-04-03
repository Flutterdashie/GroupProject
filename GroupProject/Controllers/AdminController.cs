using GroupProject.Models;
using GroupProject.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    [Authorize(Roles = "Admin")]
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
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("/Admin/Update/{id}")]
        public ActionResult Update(int id)
        {
            var repo = new BlogRepository();

            BlogPost model = repo.BlogPostSelectById(id);

            if (model == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpPost]
        [Route("Admin/Update/{id}")]
        public ActionResult Update(int id, BlogPost updatedPost)
        {

            var repo = new BlogRepository();

            if (string.IsNullOrEmpty(updatedPost.Title))
            {
                ModelState.AddModelError("Title", "Please enter a title for the post.");
            }

            if (string.IsNullOrEmpty(updatedPost.Message))
            {
                ModelState.AddModelError("Message", "Please enter a message for the post.");
            }

            if (ModelState.IsValid)
            {
                repo.BlogUpdatePost(updatedPost);
                return RedirectToAction("Index");
            }

            else
            {
                return View(updatedPost);
            }
        }

        [HttpGet]
        public ActionResult DeletePost(int id)
        {
            var repo = new BlogRepository();

            BlogPost model = repo.BlogPostSelectById(id);

            if (model == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(BlogPost post)
        {
            var repo = new BlogRepository();

            repo.BlogDeletePost(post.BlogPostId);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
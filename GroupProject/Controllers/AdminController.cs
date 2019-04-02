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
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        [Route("/Admin/Update/{id}")]
        public ActionResult Update(int id)
        {
            BlogPost model = new BlogPost(); // TODO: Replace this with repo.GetByID or equivalent
            if (model == null)
            {
                //TODO: Some sort of error handling if the GetByID doesn't find a post there
            }

            return View(model);
        }

        [HttpPost]
        [Route("Admin/Update/{id}")]
        public ActionResult Update(int id, BlogPost updatedPost)
        {
            //TODO: Validate title/message/whatever else
            //TODO: Actually apply changes to the repo
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult DeletePost(int id)
        {
            var repo = new BlogRepository();

            BlogPost post = repo.BlogPostSelectById(id);

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(BlogPost post)
        {
            var repo = new BlogRepository();

            repo.BlogDeletePost(post.BlogPostId);

            return RedirectToAction("Index", "Home");
        }

    }
}
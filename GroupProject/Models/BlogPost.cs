using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Models
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }
        public string Title { get; set; }
        //TODO: Make sure we're protected from bad html input.
        [AllowHtml]
        public string Message { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateEdited { get; set; }
    }
}
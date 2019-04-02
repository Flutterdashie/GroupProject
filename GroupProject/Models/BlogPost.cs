using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateEdited { get; set; }
    }
}